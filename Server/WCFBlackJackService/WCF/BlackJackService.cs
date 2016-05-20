using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WCFBlackJackService.GameEngine;
using WCFBlackJackService.WCF;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using WCFBlackJackService.WCF.WCFAuxiliary;
using System.Collections.ObjectModel;
using WCFBlackJackService.GameEngine.CardGameFramework;

namespace WCFBlackJackService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "BlackJackService" in both code and config file together.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class BlackJackService : IBlackJackService
    {
        public GameSession _session;
        static PlayerCollection players = new PlayerCollection();
        public Player _player;

        static ReplaySubject<Player> WantToPlaySubject = new ReplaySubject<Player>();
        static ObservableCollection<GameSession> sessions = new ObservableCollection<GameSession>();
        static IDisposable sessionDispatcher = WantToPlaySubject
            .AsObservable()
            .Synchronize()
            .Buffer(3, 3)
            .Subscribe(sessionPlayers =>
                {
                    
                    GameSession session = new GameSession(sessions.Count,sessionPlayers);
                    var currentPlayers = players.AsObservableCollection();
                    sessions.Add(session);

                    sessionPlayers.ToObservable()
                    .ForEachAsync(player => 
                    {
                        var plr = currentPlayers.Where(current => current.Name == player.Name).Last();
                        plr.SessionID = session.ID;
                    });                
                });

        #region IBlackJackService implementation
        public void Deal(Player player, decimal sum)
        {
            if (_session == null)
                _session = sessions.Where(session => session.ID == player.SessionID).First();
            _session.Deal(_player, sum);
        }

        public void Hit(Player player)
        {
            Console.WriteLine(_player.Name + " has id "+ _player.SessionID);
            _session.Hit(_player);
        }

        public void Join(Player player)
        {
            if(player is Player)
            {
                Console.WriteLine(player.Name + " connected");
                _player = player;
                _player.Bet = 0;
                _player.Callback = OperationContext.Current.GetCallbackChannel<IBlackJackServiceCallback>();
                players.OnNext(_player);
            }
        }

        public void StartGame(Player player)
        {
            WantToPlaySubject.OnNext(player);
        }


        public void Leave(Player player)
        {
            _session.Leave(_player);
        }

        public void Stand(Player player)
        {
            _session.Stand(_player);
        }

        public static void FinalizeSession(decimal ID)
        {
            var session = sessions.Where(sess => sess.ID == ID).First();
            sessions.Remove(session);
            players.AsObservableCollection()
                .Where(player => player.SessionID == ID)
                .ForEach(player => 
                {
                    player.SessionID = -1;
                    player.Bet = 0;
                    player.Hand = null;
                });
        }

        public void WantsToPlay(Player player)
        {
            Console.WriteLine(player.Name + " wants to play");
            StartGame(_player);
        }
        #endregion
    }
}

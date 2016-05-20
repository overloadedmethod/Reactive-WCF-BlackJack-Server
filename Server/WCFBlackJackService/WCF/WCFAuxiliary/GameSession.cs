using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Subjects;
using WCFBlackJackService.WCF.WCFAuxiliary;
using System.Collections.ObjectModel;
using WCFBlackJackService.GameEngine.CardGameFramework;
using WCFBlackJackService.GameEngine;

namespace WCFBlackJackService.WCF.WCFAuxiliary
{
    public class GameSession : IBlackJackService
    {
        public readonly decimal ID;
        private Deck _deck;
        private Player _dealer;
        private ObservableCollection<Player> _players;
        private ReplaySubject<Player> _standSubject;
        private IDisposable _finalize;
        public GameSession(decimal id,IList<Player> players)
        {
            ID = id;
            _deck = new Deck();
            _deck.Shuffle();
            _players = new ObservableCollection<Player>(players);
            _dealer = new Player { Name = "Dealer",Hand=new BlackJackHand()};
            _standSubject = new ReplaySubject<Player>();
            _finalize = _standSubject.Buffer(_players.Count, _players.Count)
                .Subscribe(count => FinalizeGame());
            //_players.ForEach(player => player.SessionID = id);

            _players.ForEach(player => player.Callback.PlayerList(_players.ToArray()));
        }

        private void DistibuteCards()
        {
            //var players = _players.AsObservableCollection();

            _players.ForEach(player => 
            {
                player.Hand = new BlackJackHand();
                player.Hand.Cards.Add(_deck.Draw());
                player.Hand.Cards.Add(_deck.Draw());
                player.Callback.PlayerRecievedCards(player, player.Hand.Cards);
                _players.Where(pl => pl.Name != player.Name)
                    .ForEach(pl => pl.Callback.PlayerRecievedCards(player, player.Hand.toUnknown()));
            });

            _dealer.Hand.Cards.Add(_deck.Draw());
            _dealer.Hand.Cards.Add(_deck.Draw());

            var dealerCards = new List<Card>
            {
                new Card {
                    CardFaceValue = FaceValue.Unknown
                    , CardSuit = Suit.Unknown
                }
                , _dealer.Hand.Cards.Last()
            };

            _players.Select(player => player.Callback)
                .ForEach(callback =>callback.PlayerRecievedCards(_dealer, dealerCards));
        }

        public void Deal(Player player, decimal sum)
        {
            Console.WriteLine(player.Name + " deals " + sum);
           var currentPlayer = _players
                .Where(pl => pl.Name == player.Name && pl.Bet == 0).Last();
            currentPlayer.Bet = sum;

            _players.Where(pl => pl.Name != player.Name)
                .Select(pl => pl.Callback)
                .ForEach(callback => callback.PlayerDeal(player, sum));

            _players.ToObservable().All(pl => pl.Bet > 0)
                .Where(result => result)
                .Subscribe(result => DistibuteCards());
        }

        public void Hit(Player player)
        {
            var HitCallbackSubj = new ReplaySubject<Player>();
            var HitCallBackObservable = HitCallbackSubj
                .SelectMany(sender => _players.Select(pl => new { Sender=sender,Reciever=pl}));

            HitCallBackObservable.Where(pl => pl.Sender.Name == pl.Reciever.Name)
                .Select(pl => pl.Reciever)
                .Subscribe<Player>(pl => pl.Callback.PlayerRecievedCards(pl, pl.Hand.Cards));

            HitCallBackObservable.Where(pl=>pl.Sender.Name!=pl.Reciever.Name)
                .Select(pl=>new { Reciever = pl.Reciever.Callback
                ,Sender=pl.Sender
                ,Cards = pl.Sender.Hand.toUnknown()})
                .Subscribe(message=>message.Reciever.PlayerRecievedCards(message.Sender, message.Cards));

            var currentPlayer = _players
                .Where(pl => pl.Name == player.Name && pl.Bet > 0 && pl.Hand.GetSumOfHand() < 21).Last();



            currentPlayer.Hand.Cards.Add(_deck.Draw());

            if (currentPlayer.Hand.GetSumOfHand() > 21)
                _players.ToObservable().Select(pl => pl.Callback)
                    .Subscribe(ServAux.Broadcast<IBlackJackServiceCallback>
                    (callback => callback.PlayerStatus(currentPlayer, "Busted!")));

            HitCallbackSubj.OnNext(currentPlayer);
        }

        public void AllBusted()
        {

        }

        public void Join(Player player)
        {
            Console.WriteLine(player.Name + " enering to the game");
            if (!_players.Any(pl => pl.Name == player.Name))
                _players.Add(player);
        }

        public void JoinSession(int sessionID)
        {
            throw new NotImplementedException();
        }

        public void Leave(Player player)
        {
            //player.Token = PlayerGameStateToken.LEAVE;
            //_players.OnNext(player);
        }

        public void Stand(Player player)
        {
            Console.WriteLine(player.Name + " stands");
            _standSubject.OnNext(player);
        }

        private void FinalizeGame()
        {
            if(_dealer.Hand.GetSumOfHand() < 17)
            {
                while (_dealer.Hand.GetSumOfHand() < 17)
                    _dealer.Hand.Cards.Add(_deck.Draw());
            }
            var prize = _players.Select(player => player.Bet)
                            .Sum();

            var players = _players.ToObservable().StartWith(_dealer);

            var winner = players.Where(player => player.Hand.GetSumOfHand() < 22)
                .MaxBy(player => player.Hand.GetSumOfHand())
                .SelectMany(winners => winners.ToObservable());

            winner.Where(plr=>plr.Name == _dealer.Name)
                .Subscribe(wner => 
                {
                    _players.ToObservable().Select(pl => pl.Callback)
                        .Subscribe(ServAux.Broadcast<IBlackJackServiceCallback>
                        (callback => callback.PlayerStatus(wner, "Wins!Casino takes the price\n\n\n")));
                });
            
            winner.Any(wnr=>wnr.Name==_dealer.Name)
                .Where(result=>!result)
                .SelectMany(result=>winner)
                .Take(1)
                .Subscribe(wner => {
                    _players.ToObservable().Select(pl => pl.Callback)
                       .Subscribe(ServAux.Broadcast<IBlackJackServiceCallback>
                       (callback => callback.PlayerStatus(wner, "Wins!\n\n\n")));
                });
            Console.WriteLine("Finilizing");
            BlackJackService.FinalizeSession(ID);
            
        }

        public void WantsToPlay(Player player)
        {
            throw new NotImplementedException();
        }
    }
}

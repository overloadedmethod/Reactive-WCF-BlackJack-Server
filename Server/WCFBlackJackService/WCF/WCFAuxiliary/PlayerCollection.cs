using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Subjects;
using System.Reactive.Linq;
using System.Collections.ObjectModel;
using WCFBlackJackService.GameEngine;

namespace WCFBlackJackService.WCF.WCFAuxiliary
{
    class PlayerCollection : ISubject<Player>
    {
        private ObservableCollection<Player> _players;
        private ReplaySubject<Player> _player;

        public PlayerCollection(ObservableCollection<Player> players,ReplaySubject<Player> player)
        {
            _players = players;
            _player = player;
        }

        public PlayerCollection() :
            this(new ObservableCollection<Player>(), new ReplaySubject<Player>()) { }

        public void OnCompleted()
        {
            _players.Clear();
        }

        public ObservableCollection<Player> AsObservableCollection()
        {
            return _players;
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(Player value)
        {
            var isPlayerExists = _players.AsParallel()
                .Any(currPlayers => currPlayers.Name.Equals
                (value.Name, StringComparison.OrdinalIgnoreCase));

            Observable.Return(value)
                .Synchronize()
                .TakeWhile(_ => !isPlayerExists)
                .Subscribe(pl => _players.Add(pl));

            _players.ToObservable()
                .TakeWhile(_ => !isPlayerExists)
                .Where(pl => !pl.Name.Equals(value.Name, StringComparison.OrdinalIgnoreCase))
                .Select(pl => pl.Callback)
                .Subscribe(ServAux.Broadcast<IBlackJackServiceCallback>(cl => cl.PlayerJoin(value)));
        }

        public IDisposable Subscribe(IObserver<Player> observer)
        {
            return _players.ToObservable().Subscribe(observer);
        }

        public IObservable<Player> AsObservable()
        { 
            return _players.ToObservable<Player>();
        }
    }
}

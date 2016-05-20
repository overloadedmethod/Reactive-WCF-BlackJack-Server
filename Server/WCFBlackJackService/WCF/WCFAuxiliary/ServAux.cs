using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace WCFBlackJackService.WCF.WCFAuxiliary
{
    public static class ServAux
    {
        #region Auxiliary
        public static void Broadcast<T>(IObservable<T> recievers, Action<T> lambda)
        {
            var broadcast = new ReplaySubject<T>();
            broadcast.ForEachAsync(lambda);
            recievers.Subscribe(cl => broadcast.OnNext(cl), () => broadcast.OnCompleted());
            broadcast.Dispose();
        }

        public static ReplaySubject<T> Broadcast<T> (Action<T> lambda)
        {
            var broadcast = new ReplaySubject<T>();
            broadcast.ForEachAsync(lambda);
            return broadcast;
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var cur in enumerable)
            {
                action(cur);
            }
        }

        //public static ReplaySubject<Action> ReplaySubject<Action> Broadcast()
        //{
        //    var broadcast = new ReplaySubject<Action>();
        //    broadcast.ForEachAsync(lambda => lambda.Invoke());
        //    return broadcast;
        //}

        #endregion
    }
}

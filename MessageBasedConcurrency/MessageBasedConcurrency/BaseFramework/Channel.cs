using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace MessageBasedConcurrency.BaseFramework
{
    public class Channel<T> : IObserver<T>, IObservable<T>, IActivateableProcess
    {
        private readonly Queue _Queue;
        private readonly List<IObserver<T>> _Observers;
        private bool _Finished;
        private readonly ManualResetEvent _NewItemEntered;

        public Channel()
        {
            _Queue = Queue.Synchronized(new Queue());
            _Observers = new List<IObserver<T>>();
            _NewItemEntered = new ManualResetEvent(false);
        }

        public void OnNext(T message)
        {
            lock (_Queue)
            {
                _Queue.Enqueue(message);
                _NewItemEntered.Set();
            }
        }

        public void OnComplete()
        {
            _Finished = true;
        }

        public void BroadcastTo(IObserver<T> subscriber)
        {
            _Observers.Add(subscriber);
        }

        public void Activate()
        {
            while(!_Finished)
            {
                _NewItemEntered.WaitOne();

                while (_Queue.Count > 0)
                {
                    var message = (T) _Queue.Dequeue();
                    _Observers.ForEach(x => x.OnNext(message));
                }
            }
        }
    }
}
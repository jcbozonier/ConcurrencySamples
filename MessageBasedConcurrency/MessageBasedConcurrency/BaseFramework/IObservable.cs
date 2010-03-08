namespace MessageBasedConcurrency.BaseFramework
{
    public interface IObservable<T>
    {
        void BroadcastTo(IObserver<T> observer);
    }
}
namespace MessageBasedConcurrency.BaseFramework
{
    public interface IObserver<T>
    {
        void OnNext(T message);
        void OnComplete();
    }
}
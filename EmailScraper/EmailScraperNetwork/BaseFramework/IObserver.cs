namespace EmailScraperNetwork.BaseFramework
{
    public interface IObserver<T>
    {
        void OnNext(T lineOfText);
    }
}

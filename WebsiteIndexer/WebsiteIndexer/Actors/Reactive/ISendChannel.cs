namespace WebsiteIndexer.Actors.Reactive
{
    public interface ISendChannel<T>
    {
        void Send(T message);
    }
}
using System.Collections.Generic;
using EmailScraperNetwork.BaseFramework;

namespace EmailScraperAgentBehaviours.Agents.Fakes
{
    public class MessageCollectionChannel<T> : IObserver<T>
    {
        public MessageCollectionChannel()
        {
            ReceivedMessages = new List<T>();
        }

        public int ReceivedMessagesCount;
        public readonly List<T> ReceivedMessages;

        public void OnNext(T lineOfText)
        {
            ReceivedMessagesCount++;
            ReceivedMessages.Add(lineOfText);
        }
    }
}
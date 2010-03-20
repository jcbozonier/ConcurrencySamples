using System;
using System.Linq;
using EmailScraperAgentBehaviours.Agents.Fakes;
using EmailScraperNetwork.Actors;
using NUnit.Framework;

namespace Given_a_line_of_text.with_no_email_address
{
    [TestFixture]
    public class When_the_agent_receives_the_message
    {
        [Test]
        public void It_should_send_the_text_to_the_bad_email_address_channel()
        {
            Assert.That(BadEmailChannel.ReceivedMessages.SingleOrDefault(), Is.EqualTo(ProvidedLineOfText));
        }

        [Test]
        public void It_should_NOT_send_anything_to_the_good_email_address_channel()
        {
            Assert.That(GoodEmailChannel.ReceivedMessages, Is.Empty);
        }

        [TestFixtureSetUp]
        public void Setup()
        {
            BadEmailChannel = new TransperantBadEmailChannel();
            GoodEmailChannel = new TransperantGoodEmailChannel();

            It = new ObviousEmailExtractionAgent(GoodEmailChannel, BadEmailChannel);

            Context();
            Because();
        }

        private void Because()
        {
            It.OnNext(ProvidedLineOfText);
        }

        private void Context()
        {
            ProvidedLineOfText = "abcd sfsd dfsdfsdfsd \tHI!";
        }

        private ObviousEmailExtractionAgent It;
        private string ProvidedLineOfText;
        private TransperantBadEmailChannel BadEmailChannel;
        private string ProvidedEmailAddress;
        private TransperantGoodEmailChannel GoodEmailChannel;
    }
}


namespace Given_a_line_of_text.with_an_email_address.and_other_text
{
    [TestFixture]
    public class When_the_agent_receives_the_message
    {
        [Test]
        public void It_should_send_the_email_address_to_the_good_email_address_channel()
        {
            Assert.That(MessageChannel.ReceivedMessages.SingleOrDefault(), Is.EqualTo(ProvidedEmailAddress));
        }

        [TestFixtureSetUp]
        public void Setup()
        {
            MessageChannel = new TransperantGoodEmailChannel();

            It = new ObviousEmailExtractionAgent(MessageChannel, null);

            Context();
            Because();
        }

        private void Because()
        {
            It.OnNext(ProvidedLineOfText);
        }

        private void Context()
        {
            ProvidedEmailAddress = "darkxanthos@gmail.com";
            ProvidedLineOfText = "abcd " + ProvidedEmailAddress + "\tHI!";
        }

        private ObviousEmailExtractionAgent It;
        private string ProvidedLineOfText;
        private TransperantGoodEmailChannel MessageChannel;
        private string ProvidedEmailAddress;
    }
}

namespace Given_a_line_of_text.with_an_email_address
{
    [TestFixture]
    public class When_the_agent_receives_the_message
    {
        [Test]
        public void It_should_send_the_email_address_out_on_the_good_email_address_channel()
        {
            Assert.That(MessageChannel.ReceivedMessages.SingleOrDefault(), Is.EqualTo(ProvidedEmailAddress));
        }

        private ObviousEmailExtractionAgent It;
        private TransperantGoodEmailChannel MessageChannel;
        private string ProvidedEmailAddress;

        [TestFixtureSetUp]
        public void Setup()
        {
            MessageChannel = new TransperantGoodEmailChannel();
            It = new ObviousEmailExtractionAgent(MessageChannel, null);

            Context();
            Because();
        }

        private void Context()
        {
            ProvidedEmailAddress = "darkxanthos@gmail.com";
        }

        private void Because()
        {
            It.OnNext(ProvidedEmailAddress);
        }
    }
}

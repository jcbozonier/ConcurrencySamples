using System.Linq;
using EmailScraperAgentBehaviours.Agents.Fakes;
using EmailScraperNetwork.Actors;
using NUnit.Framework;

namespace Given_a_file.with_lines_of_text.and_some_lines_with_whitespace_characters
{
    [TestFixture]
    public class When_the_agent_receives_the_file_path
    {
        [Test]
        public void It_should_send_a_message_for_only_the_line_with_text()
        {
            Assert.That(MessageChannel.ReceivedMessagesCount, Is.EqualTo(1));
        }

        [Test]
        public void It_should_NOT_send_out_a_message_for_any_non_blank_line()
        {
            Assert.That(MessageChannel.ReceivedMessages.Any(x => x.Trim() == ""), Is.False);
        }

        [Test]
        public void It_should_use_the_provided_file_path()
        {
            Assert.That(FileReader.ProvidedFilePath, Is.EqualTo(ProvidedFilePath));
        }

        [TestFixtureSetUp]
        public void Setup()
        {
            ProvidedFilePath = "filePath";
            MessageChannel = new MessageCollectionChannel<string>();
            FileReader = new FileReaderWithOneNonBlankLineAndMultipleWhitespaceLines();
            It = new LineByLineFileReadingAgent(FileReader);
            It.ShouldSendLinesOfTextTo(MessageChannel);

            It.OnNext(ProvidedFilePath);
        }

        private FileReaderWithOneNonBlankLineAndMultipleWhitespaceLines FileReader;
        private LineByLineFileReadingAgent It;
        private MessageCollectionChannel<string> MessageChannel;
        private string ProvidedFilePath;

    }
}

namespace Given_a_file.with_lines_of_text.and_some_blank_lines
{
    [TestFixture]
    public class When_the_agent_receives_the_file_path
    {
        [Test]
        public void It_should_NOT_send_out_a_message_for_any_non_blank_line()
        {
            Assert.That(MessageChannel.ReceivedMessages.Any(x => x == ""), Is.False);
        }

        [Test]
        public void It_should_use_the_provided_file_path()
        {
            Assert.That(FileReader.ProvidedFilePath, Is.EqualTo(ProvidedFilePath));
        }

        [TestFixtureSetUp]
        public void Setup()
        {
            ProvidedFilePath = "filePath";
            MessageChannel = new MessageCollectionChannel<string>();
            FileReader = new FileReaderWithOneNonBlankLineAndMultipleBlankLines();
            It = new LineByLineFileReadingAgent(FileReader);
            It.ShouldSendLinesOfTextTo(MessageChannel);

            It.OnNext(ProvidedFilePath);
        }

        private FileReaderWithOneNonBlankLineAndMultipleBlankLines FileReader;
        private LineByLineFileReadingAgent It;
        private MessageCollectionChannel<string> MessageChannel;
        private string ProvidedFilePath;
    }
}

namespace Given_a_file.with_lines_of_text
{
    [TestFixture]
    public class When_the_agent_receives_the_file_path
    {
        [Test]
        public void It_should_send_out_a_message_for_each_line()
        {
            Assert.That(LinesOfTextChannel.ReceivedMessagesCount, Is.EqualTo(FileReaderWithOneNonBlankLineOfText.NonblankLineCount));
        }

        [Test]
        public void It_should_read_from_the_correct_file()
        {
            Assert.That(FileReader.FilePath, Is.EqualTo(ProvidedFilePath));
        }

        private void Context()
        {
            FileReader = new FileReaderWithOneNonBlankLineOfText();
        }

        private void Because()
        {
            It.OnNext(ProvidedFilePath);
        }

        [TestFixtureSetUp]
        public void Setup()
        {
            LinesOfTextChannel = new MessageCollectionChannel<string>();

            Context();

            It = new LineByLineFileReadingAgent(FileReader);
            It.ShouldSendLinesOfTextTo(LinesOfTextChannel);

            ProvidedFilePath = "c:/file_path";

            Because();
        }

        private LineByLineFileReadingAgent It;
        private MessageCollectionChannel<string> LinesOfTextChannel;
        private FileReaderWithOneNonBlankLineOfText FileReader;
        private string ProvidedFilePath;
    }
}

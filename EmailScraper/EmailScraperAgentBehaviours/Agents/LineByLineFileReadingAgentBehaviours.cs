using System;
using System.Linq;
using EmailScraperAgentBehaviours.Agents.Fakes;
using EmailScraperNetwork.Actors;
using EmailScraperNetwork.ChannelContracts;
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
            Assert.That(_fileReadingServiceReader.ProvidedFilePath, Is.EqualTo(ProvidedFilePath));
        }

        [TestFixtureSetUp]
        public void Setup()
        {
            ProvidedFilePath = "filePath";
            MessageChannel = new ChannelForNonBlankTextLines();
            _fileReadingServiceReader = new FileReadingServiceReaderWithOneNonBlankLineAndMultipleWhitespaceLines();
            It = new LineByLineFileReadingAgentChannel(_fileReadingServiceReader, MessageChannel);

            It.SendBeginReadingFromFilePath(ProvidedFilePath);
        }

        private FileReadingServiceReaderWithOneNonBlankLineAndMultipleWhitespaceLines _fileReadingServiceReader;
        private LineByLineFileReadingAgentChannel It;
        private ChannelForNonBlankTextLines MessageChannel;
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
            Assert.That(_fileReadingServiceReader.ProvidedFilePath, Is.EqualTo(ProvidedFilePath));
        }

        [TestFixtureSetUp]
        public void Setup()
        {
            ProvidedFilePath = "filePath";
            MessageChannel = new ChannelForNonBlankTextLines();
            _fileReadingServiceReader = new FileReadingServiceReaderWithOneNonBlankLineAndMultipleBlankLines();
            It = new LineByLineFileReadingAgentChannel(_fileReadingServiceReader, MessageChannel);
            
            It.SendBeginReadingFromFilePath(ProvidedFilePath);
        }

        private FileReadingServiceReaderWithOneNonBlankLineAndMultipleBlankLines _fileReadingServiceReader;
        private LineByLineFileReadingAgentChannel It;
        private ChannelForNonBlankTextLines MessageChannel;
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
            Assert.That(LinesOfTextChannel.ReceivedMessagesCount, Is.EqualTo(FileReadingServiceReaderWithOneNonBlankLineOfText.NonblankLineCount));
        }

        [Test]
        public void It_should_read_from_the_correct_file()
        {
            Assert.That(_fileReadingServiceReader.FilePath, Is.EqualTo(ProvidedFilePath));
        }

        private void Context()
        {
            _fileReadingServiceReader = new FileReadingServiceReaderWithOneNonBlankLineOfText();
        }

        private void Because()
        {
            It.SendBeginReadingFromFilePath(ProvidedFilePath);
        }

        [TestFixtureSetUp]
        public void Setup()
        {
            LinesOfTextChannel = new ChannelForNonBlankTextLines();

            Context();

            It = new LineByLineFileReadingAgentChannel(_fileReadingServiceReader, LinesOfTextChannel);

            ProvidedFilePath = "c:/file_path";

            Because();
        }

        private LineByLineFileReadingAgentChannel It;
        private ChannelForNonBlankTextLines LinesOfTextChannel;
        private FileReadingServiceReaderWithOneNonBlankLineOfText _fileReadingServiceReader;
        private string ProvidedFilePath;
    }
}

using NUnit.Framework;

namespace Take.Be.Domain.Test
{
    public class TcpServerTest
    {
        [Test]
        public void ShouldCreateNewServer()
        {
            var server = new TcpServer();
            
            Assert.NotNull(server);
        }

        [Test]
        public void TryStart_ShouldReturnTrue()
        {
            var server = new TcpServer();

            bool response = server.TryStart();

            Assert.True(response);
        }

        [Test]
        public void TryStop_ShouldReturnTrue()
        {
            var server = new TcpServer();

            bool response = server.TryStop();

            Assert.True(response);
        }

        [Test]
        public void ValidateNickName_ShouldReturnEmptyString()
        {
            var server = new TcpServer();

            string response = server.ValidateNickName("myNickName");

            Assert.IsEmpty(response);
        }
    }
}

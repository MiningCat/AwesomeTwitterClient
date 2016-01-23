using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Ploeh.AutoFixture.Xunit2;
using TwitterClient.Core.Facade;
using TwitterClient.Streaming.Facade;
using TwitterClient.Streaming.Tests.Attributtes;
using Xunit;

namespace TwitterClient.Streaming.Tests
{
    public class StreamListnerTests
    {

        [Theory, AutoWebData]
        public async Task Listen_AnonymousData_Ok(
            [Frozen]HttpRequestMessage request,
            Func<HttpRequestMessage> requestProvider,
            [Frozen]Mock<IStreamingUtils> streamingUtils, 
            [Frozen]Mock<IHttpUtils> httpUtils,
            Listener sut)
        {
            //Arrange
            var twits = new[] {"Твиттер это ниочень", "Вот мой мир -- другое дело", "круче только одноклассники"};
            var concatenatedTwits = string.Join('\n'.ToString(), twits);
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(concatenatedTwits));
            var reader = new StreamReader(stream, Encoding.GetEncoding("utf-8"));

            httpUtils.Setup(a => a.UnescapeUnicode(It.IsAny<string>())).Returns((string r) => r);
            streamingUtils.Setup(a => a.GetReader(It.IsAny<HttpRequestMessage>())).ReturnsAsync(reader);
            var results = new List<string>();
            Action<string> processRequest = a => results.Add(a);

            //Act
            await sut.Listen(requestProvider, processRequest);

            //Assert
            streamingUtils.Verify(a => a.GetReader(request), Times.Once);
            results.ShouldAllBeEquivalentTo(twits);
        }


    }
}

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Ploeh.AutoFixture.Xunit2;
using TwitterClient.Core.Facade;
using TwitterClient.Streaming.Facade;
using TwitterClient.Streaming.Tests.Attributtes;
using Xunit;

namespace TwitterClient.Streaming.Tests.Filter
{
    public class FilterStreamTests
    {

        [Theory, AutoWebData]
        public void GetStream_AnonymousData_CorrectObservable(
            [Frozen] Mock<IListener> listner,
            IFilterQuery query,
            Streaming sut,
            HttpRequestMessage request,
            List<string> data)
        {
            listner.Setup(a => a.Listen(It.IsAny<Func<HttpRequestMessage>>(), It.IsAny<Action<string>>()))
                .Returns((Func<HttpRequestMessage> requestProvider, Action<string> processRequest) =>
                {
                    data.ForEach(processRequest);
                    return new Task(() => { });
                });

            var observable = sut.GetStream(query);

            var resultData = new List<string>();
            observable.Subscribe(a => resultData.Add(a));
            resultData.ShouldAllBeEquivalentTo(data);
        }

        [Theory, AutoWebData]
        public void GetStream_AnonymousData_CorrectRequestProvider(
            [Frozen] Mock<IListener> listner,
            [Frozen] Mock<IRequestAuthorizer> authorizer,
            Mock<IFilterQuery> query,
            Streaming sut,
            HttpRequestMessage request,
            List<string> data)
        {
            //Arrange
            Func<HttpRequestMessage> requestProviderArgument = null;
            authorizer.Setup(a => a.AuthorizeRequest(It.IsAny<HttpRequestMessage>())).Returns(request);
            query.Setup(a => a.GetRequest()).Returns(request);
            listner.Setup(a => a.Listen(It.IsAny<Func<HttpRequestMessage>>(), It.IsAny<Action<string>>()))
                .Callback((Func<HttpRequestMessage> requestProvider, Action<string> processRequest) =>
                {
                    requestProviderArgument = requestProvider;
                });

            //Act
            var observable = sut.GetStream(query.Object);
            observable.Subscribe(a => {});
            var result = requestProviderArgument();

            //Assert
            result.Should().Be(request);
            query.Verify(a => a.GetRequest(), Times.Once);
            authorizer.Verify(a => a.AuthorizeRequest(request), Times.Once);
        }

        [Theory, AutoWebData]
        public void GetStream_AnonymousData_ExceptionPropagated(
            [Frozen] Mock<IListener> listner,
            IFilterQuery query,
            Streaming sut)
        {
            //Arrange
            listner.Setup(a => a.Listen(It.IsAny<Func<HttpRequestMessage>>(), It.IsAny<Action<string>>()))
                .Throws<WebException>();
            Exception propagatedException = null;

            //Act
            var observable = sut.GetStream(query);
            observable.Subscribe(a => { }, ex => propagatedException = ex);

            //Assert
            propagatedException.Should().NotBeNull();
            propagatedException.Should().BeOfType<WebException>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using FluentAssertions;
using Moq;
using Ploeh.AutoFixture.Idioms;
using Ploeh.AutoFixture.Xunit2;
using TwitterClient.Core.Facade;
using TwitterClient.Core.Tests.Attributtes;
using Xunit;

namespace TwitterClient.Core.Tests
{
    public class RequestAuthorizerTests
    {
        [Theory, AutoWebData]
        public void Constructor_Null_GuardClause(GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(RequestAuthorizer));
        }

        [Theory, AutoWebData]
        public void GetOauthParamerters_AnonymousData_CorrectParameters(
            [Frozen]ITwitterCredentials credentials,
            [Frozen]Mock<IHttpUtils> oAuthUtils,
            RequestAuthorizer sut)
        {
            const string nonce = "BogdanPidr";
            const string timeStamp = "12345678";
            oAuthUtils.Setup(a => a.GenerateNonce()).Returns(nonce);
            oAuthUtils.Setup(a => a.GenerateTimeStamp()).Returns(timeStamp);

            var oauthParameters = sut.CreateOAuthParameters();

            oauthParameters.Should().Contain(a => a.Key == "oauth_consumer_key" && a.Value == credentials.ConsumerKey);
            oauthParameters.Should().Contain(a => a.Key == "oauth_nonce" && a.Value == nonce);
            oauthParameters.Should().Contain(a => a.Key == "oauth_signature_method" && a.Value == "HMAC-SHA1");
            oauthParameters.Should().Contain(a => a.Key == "oauth_timestamp" && a.Value == timeStamp);
            oauthParameters.Should().Contain(a => a.Key == "oauth_token" && a.Value == credentials.AccessToken);
            oauthParameters.Should().Contain(a => a.Key == "oauth_version" && a.Value == "1.0");
        }

        [Theory, AutoWebData]
        public void GetParametes_AnonymousBodyEmptyQuery_CorrectParameters(
            string parametersString,
            [Frozen]Mock<IHttpUtils> oAuthUtils,
            HttpRequestMessage request,  
            RequestAuthorizer sut)
        {
            var parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("All", "Right")
            };
            oAuthUtils.Setup(a => a.ParseParameterString(parametersString))
                .Returns(parameters);
            request.Content = new StringContent(parametersString, Encoding.UTF8);

            var result = sut.GetParameters(request);

             result.ShouldBeEquivalentTo(parameters);
        }

        [Theory, AutoWebData]
        public void GetParametes_AnonymousQueryEmptyBody_CorrectParameters(
            string parametersString,
            [Frozen]Mock<IHttpUtils> oAuthUtils,
            HttpRequestMessage request,
            RequestAuthorizer sut)
        {
            parametersString = "?" + parametersString; //Иначе не распарсится как query
            var parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("All", "Right")
            };
            oAuthUtils.Setup(a => a.ParseParameterString(parametersString))
                .Returns(parameters);
            
            request.RequestUri = new Uri(request.RequestUri + parametersString);

            var result = sut.GetParameters(request);

            result.ShouldBeEquivalentTo(parameters);
        }

        [Theory, AutoWebData]
        public void GetParametes_AnonymousQueryAndBody_CorrectParameters(
            string bodyParametersString,
            string queryParametersString,
            [Frozen]Mock<IHttpUtils> oAuthUtils,
            HttpRequestMessage request,
            RequestAuthorizer sut)
        {
            //Arrange
            //Query parameters
            queryParametersString = "?" + queryParametersString; //Иначе не распарсится как query
            var queryParameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Query", "Parameter")
            };
            oAuthUtils.Setup(a => a.ParseParameterString(queryParametersString))
                .Returns(queryParameters);
            request.RequestUri = new Uri(request.RequestUri + queryParametersString);
            //Body parameters
            var bodyParameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Body", "Parameter")
            };
            oAuthUtils.Setup(a => a.ParseParameterString(bodyParametersString))
                .Returns(bodyParameters);
            request.Content = new StringContent(bodyParametersString, Encoding.UTF8);

            //Act
            var result = sut.GetParameters(request);

            //Assert
            result.ShouldBeEquivalentTo(bodyParameters.Union(queryParameters));
        }

        [Theory, AutoWebData]
        public void SignRequest_AnonymousRequest_RequestAuthorized(
            [Frozen] ITwitterCredentials credentials,
            [Frozen] Mock<IHttpUtils> oAuthUtils,
            HttpRequestMessage request,
            RequestAuthorizer sut)
        {
            //Arrange
            const string parametersString = "ParametersString";
            const string key = "10001110101";
            const string body = "POST&URL&bogdan=pidr";
            const string signature = "IvanovIvan";

            oAuthUtils.Setup(a => a.GetParametersString(It.IsAny<IEnumerable<KeyValuePair<string, string>>>(), 
                It.IsAny<string>())).Returns(parametersString);
            oAuthUtils.Setup(a => a.CreateSigningKey(It.IsAny<string>(), It.IsAny<string>())).Returns(key);
            oAuthUtils.Setup(a => a.CreateSignatureBaseString(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(body);
            oAuthUtils.Setup(a => a.CreateSignature(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(signature);

            //Act
            var result = sut.AuthorizeRequest(request);

            //Assert
            result.Headers.Authorization.Should().NotBeNull();
            result.Headers.Authorization.Scheme.Should().Be("OAuth");
            result.Headers.Authorization.Parameter.Should().Be(parametersString);
            oAuthUtils.Verify(a => a.GetParametersString(It.IsAny<IEnumerable<KeyValuePair<string, string>>>(), "&"), Times.Once());
            oAuthUtils.Verify(a => a.CreateSigningKey(credentials.ConsumerSecret, credentials.AccessTokenSecret), Times.Once());
            oAuthUtils.Verify(a => a.CreateSignatureBaseString(request.Method.Method, request.RequestUri.ToString(), parametersString), Times.Once());
            oAuthUtils.Verify(a => a.CreateSignature(body, key), Times.Once());
            oAuthUtils.Verify(a => a.GetParametersString(It.Is<IEnumerable<KeyValuePair<string, string>>>(
                x => x.Any(s => s.Key == "oauth_signature" && s.Value == signature)), ","), Times.Once());
        }
    }
}

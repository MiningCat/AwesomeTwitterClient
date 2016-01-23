using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Ploeh.AutoFixture.Idioms;
using Ploeh.AutoFixture.Xunit;
using Xunit.Extensions;

namespace TwitterClient.Core.Tests
{
    public class HttpUtilsTests
    {
        [Theory, AutoData]
        public void Constructor_Null_GuardClause(GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof (HttpUtils));
        }

        [Theory, AutoData]
        public void GenerateNonce_ValidNonce(HttpUtils sut)
        {
            var previous = string.Empty;

            for (var i = 0; i < 5; i++)
            {
                var current = sut.GenerateNonce();
                current.Should().NotBe(previous);
            }
        }

        [Theory, AutoData]
        public void GenerateTimeStamp_ValidTimeStamp(HttpUtils sut)
        {
            var currentTime = DateTime.UtcNow - new DateTime(1970, 1, 1);

            var timestamp = sut.GenerateTimeStamp();

            var value = long.Parse(timestamp);
            value.Should().BeInRange(Convert.ToInt64(currentTime.TotalSeconds), Convert.ToInt64(currentTime.TotalSeconds) + 1);
        }

        [Theory, AutoData]
        public void CreateSignatureBaseString_InvalidHttpMethod_ArgumentOutOfRangeException(HttpUtils sut,
            string method, string url, string parametersString)
        {
            sut.Invoking(a => a.CreateSignatureBaseString(method, url, parametersString))
                .ShouldThrow<ArgumentOutOfRangeException>();
        }

        [Theory]
        [InlineAutoData("POST")]
        [InlineAutoData("GET")]
        public void CreateSignatureBaseString_AnonymousData_CorrectString(string method, string url, string parametersString, HttpUtils sut)
        {
            var result = sut.CreateSignatureBaseString(method, url, parametersString);

            result.Should().Be(method + "&" + sut.Encode(url) + "&" + sut.Encode(parametersString));
        }

        [Theory, AutoData]
        public void CreateSigningKey_AnonymousData_CorrectString(string consumerSecret, string accessTokenSecret, HttpUtils sut)
        {
            var result = sut.CreateSigningKey(consumerSecret, accessTokenSecret);

            result.Should().Be(sut.Encode(consumerSecret) + "&" + sut.Encode(accessTokenSecret));
        }



        [Theory, AutoData]
        public void GetParametersString_AnonymousData_CorrectString(List<KeyValuePair<string, string>> parameters, HttpUtils sut)
        {
            var expectedValue = string.Concat(parameters.OrderBy(a => a.Key).Select(a => string.Format("{0}={1}", a.Key, a.Value)));

            var result = sut.GetParametersString(parameters);

            result.Should().Be(expectedValue);
        }

        [Theory, AutoData]
        public void GetParametersString_AnonymousDataWithDevimeter_CorrectString(
            string firstParameter,
            string secondParameter,
            HttpUtils sut)
        {
            var parameters = new[] {
                new KeyValuePair<string, string>("a", firstParameter),
                new KeyValuePair<string, string>("a", secondParameter),
            };
            var expectedValue = string.Format("{0}={1}&{2}={3}", parameters[0].Key, parameters[0].Value, parameters[1].Key, parameters[1].Value);

            var result = sut.GetParametersString(parameters, "&");

            result.Should().Be(expectedValue);
        }

        [Theory, AutoData]
        public void ParseParameterString_InvalidParameterString_EmptyCollection(string parameterString, HttpUtils sut)
        {
            var result = sut.ParseParameterString(parameterString);

            result.Should().BeEmpty();
        }

        [Theory, AutoData]
        public void ParseParameterString_ValidBodyParameterString_CorrectResult(List<KeyValuePair<string, string>> parameters, HttpUtils sut)
        {
            var parameterString = sut.GetParametersString(parameters, "&");

            var result = sut.ParseParameterString(parameterString);

            result.ShouldAllBeEquivalentTo(parameters);
        }

        [Theory, AutoData]
        public void ParseParameterString_ValidUrlParameterString_CorrectResult(List<KeyValuePair<string, string>> parameters, HttpUtils sut)
        {
            var parameterString = "?" + sut.GetParametersString(parameters, "&");

            var result = sut.ParseParameterString(parameterString);

            result.ShouldAllBeEquivalentTo(parameters);
        }

        [Theory]
        [InlineAutoData(
            "\u041f\u0443\u0442\u0438\u043d \u043f\u043e\u0434\u043f\u0438\u0441\u0430\u043b \u0437\u0430\u043a\u043e\u043d \u043e \u0448\u0442\u0440\u0430\u0444\u0430\u0445 \u0434\u043e 1 \u043c\u043b\u043d \u0440\u0443\u0431\u043b\u0435\u0439 \u0437\u0430 \u044d\u043a\u0441\u0442\u0440\u0435\u043c\u0438\u0441\u0442\u0441\u043a\u0438\u0435 \u043f\u0440\u0438\u0437\u044b\u0432\u044b \u0432 \u0421\u041c\u0418 #\u043e\u0431\u0449\u0435\u0441\u0442\u0432\u043e", 
            "Путин подписал закон о штрафах до 1 млн рублей за экстремистские призывы в СМИ #общество")]
        [InlineAutoData(
            "\u041f\u0443\u0442\u0438\u043d \u0440\u0430\u0442\u0438\u0444\u0438\u0446\u0438\u0440\u043e\u0432\u0430\u043b \u0434\u043e\u0433\u043e\u0432\u043e\u0440 \u043e \u0441\u043e\u0437\u0434\u0430\u043d\u0438\u0438 \u043f\u0443\u043b\u0430 \u0432\u0430\u043b\u044e\u0442\u043d\u044b\u0445 \u0440\u0435\u0437\u0435\u0440\u0432\u043e\u0432",
            "Путин ратифицировал договор о создании пула валютных резервов")]
        public void UnescapeUnicode_EscapedData_Unescaped(string data, string result, HttpUtils sut)
        {
            sut.UnescapeUnicode(data).Should().Be(result);
        }
    }
}

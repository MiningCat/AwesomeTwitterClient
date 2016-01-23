using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using TwitterClient.Core.Facade;

namespace TwitterClient.Core
{
    public class RequestAuthorizer : IRequestAuthorizer
    {
        private readonly ITwitterCredentials _credentials;
        private readonly IHttpUtils _oAuthUtils;

        public RequestAuthorizer(ITwitterCredentials credentials, IHttpUtils oAuthUtils)
        {
            if (credentials == null)
                throw new ArgumentNullException("credentials");

            if (oAuthUtils == null)
                throw new ArgumentNullException("oAuthUtils");

            _credentials = credentials;
            _oAuthUtils = oAuthUtils;
        }

        public HttpRequestMessage AuthorizeRequest(HttpRequestMessage request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var authorizationSection = GenerateAuthorizationSection(request);
            request.Headers.Authorization = new AuthenticationHeaderValue("OAuth", authorizationSection);

            return request;
        }

        private string GenerateAuthorizationSection(HttpRequestMessage request)
        {
            if (request == null)
                throw new ArgumentNullException("request");

            // Создаём параметры OAuth
            var oauthParameters = CreateOAuthParameters(); 
            var signature = CreateSignature(request, oauthParameters);
            
            oauthParameters.Add(new KeyValuePair<string, string>("oauth_signature", signature));

            return _oAuthUtils.GetParametersString(oauthParameters, ",");
        }

        private string CreateSignature(HttpRequestMessage request, ICollection<KeyValuePair<string, string>> oauthParameters)
        {
            //Добавляем к ним параметры запросы
            var parameters = GetParameters(request).Union(oauthParameters);
            //Создаём ParametersString
            var parametersString = _oAuthUtils.GetParametersString(parameters, "&");
            //Собираем ключ шифрования
            var key = _oAuthUtils.CreateSigningKey(_credentials.ConsumerSecret, _credentials.AccessTokenSecret);
            //Собираем сообщение, которое будем шифровать
            var body = _oAuthUtils.CreateSignatureBaseString(request.Method.Method, request.RequestUri.GetLeftPart(UriPartial.Path),
                parametersString);
            return _oAuthUtils.CreateSignature(body, key);
        }

        public ICollection<KeyValuePair<string, string>> CreateOAuthParameters()
        {
            var nonce = _oAuthUtils.GenerateNonce();
            var timeStamp = _oAuthUtils.GenerateTimeStamp();
            return new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("oauth_consumer_key", _credentials.ConsumerKey),
                new KeyValuePair<string, string>("oauth_nonce", nonce),
                new KeyValuePair<string, string>("oauth_signature_method", "HMAC-SHA1"),
                new KeyValuePair<string, string>("oauth_timestamp", timeStamp),
                new KeyValuePair<string, string>("oauth_token", _credentials.AccessToken),
                new KeyValuePair<string, string>("oauth_version", "1.0")
            };
        }


        public IEnumerable<KeyValuePair<string, string>> GetParameters(HttpRequestMessage request)
        {
            if (request == null) throw new ArgumentNullException("request");

            var query = request.RequestUri.Query;
            var parameters = _oAuthUtils.ParseParameterString(query);

            if (request.Content == null)
                return parameters;

            var parameterString = request.Content.ReadAsStringAsync().Result;
            return parameters.Union(_oAuthUtils.ParseParameterString(parameterString)).ToList();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using TwitterClient.Core.Facade;

namespace TwitterClient.Core
{
    public class HttpUtils : IHttpUtils
    {
        public string GenerateNonce()
        {
            return new Random()
              .Next(123400, int.MaxValue)
              .ToString("X", CultureInfo.InvariantCulture);
        }

        public string GenerateTimeStamp()
        {
            var time = DateTime.UtcNow - new DateTime(1970, 1, 1);

            return Convert.ToInt64(time.TotalSeconds).ToString();
        }

        public string CreateSignatureBaseString(string httpMethod, string url, string parametersString)
        {
            if (httpMethod == null) throw new ArgumentNullException("httpMethod");
            if (url == null) throw new ArgumentNullException("url");
            if (parametersString == null) throw new ArgumentNullException("parametersString");

            if (httpMethod != "GET" && httpMethod != "POST")
                throw new ArgumentOutOfRangeException("httpMethod", "Only GET и POST methods available (in uppercase)");

            return string.Format(
               CultureInfo.InvariantCulture,
               "{0}&{1}&{2}",
               httpMethod,
               Encode(url),
               Encode(parametersString));
        }

        public string CreateSigningKey(string consumerSecret, string accessTokenSecret)
        {
            if (consumerSecret == null) throw new ArgumentNullException("consumerSecret");
            if (accessTokenSecret == null) throw new ArgumentNullException("accessTokenSecret");

            return string.Format(
                CultureInfo.InvariantCulture,
                "{0}&{1}",
                Encode(consumerSecret),
                Encode(accessTokenSecret));
        }

        public string CreateSignature(string signatureBaseString, string signingKey)
        {
            if (signatureBaseString == null) throw new ArgumentNullException("signatureBaseString");
            if (signingKey == null) throw new ArgumentNullException("signingKey");

            var hmacsha1 = new HMACSHA1(Encoding.UTF8.GetBytes(signingKey));
            var signatureBytes = hmacsha1.ComputeHash(Encoding.UTF8.GetBytes(signatureBaseString));
            return Convert.ToBase64String(signatureBytes);
        }

        public string Encode(string value)
        {
            if (value == null) throw new ArgumentNullException("value");

            value = Uri.EscapeDataString(value);

            // Encode escapes with lowercase characters (e.g. %2f) but oAuth needs %2F
            value = Regex.Replace(value, "(%[0-9a-f][0-9a-f])", c => c.Value.ToUpper());

            // these characters are not escaped by Encode() but needed to be escaped
            value = value
                .Replace("(", "%28")
                .Replace(")", "%29")
                .Replace("$", "%24")
                .Replace("!", "%21")
                .Replace("*", "%2A")
                .Replace("'", "%27");

            // these characters are escaped by Encode() but will fail if unescaped!
            value = value.Replace("%7E", "~");

            return value;
        }

        public string GetParametersString(IEnumerable<KeyValuePair<string, string>> parameters, string delimiter = "")
        {
            if (parameters == null) throw new ArgumentNullException("parameters");
            if (delimiter == null) throw new ArgumentNullException("delimiter");

            return string.Join(delimiter,
                parameters.OrderBy(a => a.Key).Select(s => string.Format(
                CultureInfo.InvariantCulture,
                "{0}={1}",
                Encode(s.Key),
                Encode(s.Value))));
        }

        public IEnumerable<KeyValuePair<string, string>> ParseParameterString(string parameterString)
        {
            if (parameterString == null)
                throw new ArgumentNullException("parameterString");

            var nameValeCollection = HttpUtility.ParseQueryString(parameterString);

            return nameValeCollection.AllKeys
                .Where(key => !string.IsNullOrEmpty(key))
                .Select(key => new KeyValuePair<string, string>(key, nameValeCollection[key]));
        }

        public string UnescapeUnicode(string data)
        {
            return Regex.Replace(
                data,
                @"\\[Uu]([0-9A-Fa-f]{4})",
                m => char.ToString((char)ushort.Parse(m.Groups[1].Value, NumberStyles.AllowHexSpecifier))
            );
        }
    }
}

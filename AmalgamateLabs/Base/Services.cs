using AmalgamateLabs.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AmalgamateLabs.Base
{
    public class Services
    {
        public static async Task<XDocument> GetAmazonItemDataFromKeywords(List<string> keywordCollection, SystemConfig systemConfig)
        {
            // docs.aws.amazon.com/AWSECommerceService/latest/DG/ItemSearch.html
            // docs.aws.amazon.com/AWSECommerceService/latest/DG/rest-signature.html
            // docs.aws.amazon.com/AWSECommerceService/latest/DG/LocaleUS.html
            // docs.aws.amazon.com/general/latest/gr/signing_aws_api_requests.html

            const string SERVICE_DOMAIN = "https://webservices.amazon.com/onca/xml?";

            string prependedSignatureLines = "GET" + "\n" + "webservices.amazon.com" + "\n" + "/onca/xml" + "\n";
            string keywords = string.Empty;

            foreach (string keyword in keywordCollection)
            {
                if (!string.IsNullOrEmpty(keywords))
                {
                    keywords += "%20";
                }
                keywords += keyword;
            }

            string canonicalQueryString = $"AWSAccessKeyId={systemConfig.AWSAccessKey}&" +
                $"AssociateTag={systemConfig.AmazonAssociateID}&" +
                $"Keywords={keywords}&" +
                "Operation=ItemSearch&" +
                "ResponseGroup=Images,ItemAttributes&" +
                "SearchIndex=Movies&" +
                "Service=AWSECommerceService&" +
                "Sort=featured&" +
                $"Timestamp={DateTime.UtcNow:yyyy-MM-ddTHH:mm:ssZ}";

            string urlEncodedQueryString = canonicalQueryString.Replace(",", "%2C").Replace(":", "%3A");
            string signature = GetRequestSignature(systemConfig.AWSSecretKey, $"{prependedSignatureLines}{urlEncodedQueryString}");
            string urlEncodedSignature = signature.Replace("+", "%2B").Replace("/", "%2F").Replace("=", "%3D").Replace(",", "%2C").Replace(":", "%3A");
            //TODO: Try this instead.
            //string urlEncodedQueryString = System.Web.HttpUtility.UrlEncode(canonicalQueryString);
            //string signature = GetRequestSignature(systemConfig.AWSSecretKey, $"{prependedSignatureLines}{urlEncodedQueryString}");
            //string urlEncodedSignature2 = System.Web.HttpUtility.UrlEncode(signature);
            string requestUriString = $"{SERVICE_DOMAIN}{urlEncodedQueryString}&Signature={urlEncodedSignature}";

            return XDocument.Parse(await RequestInternetResponse(requestUriString));
        }

        /// <summary>
        /// Use a secrect to sign a string using SHA-256.
        /// </summary>
        private static string GetRequestSignature(string secretKey, string stringToSign)
        {
            byte[] key = Encoding.UTF8.GetBytes(secretKey);
            byte[] buffer = Encoding.UTF8.GetBytes(stringToSign);

            HMACSHA256 hMACSHA256 = new HMACSHA256(key);
            byte[] hash = hMACSHA256.ComputeHash(buffer);

            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Requests a response to an internet request and returns it as a string.
        /// </summary>
        private static async Task<string> RequestInternetResponse(string requestUriString)
        {
            string response = string.Empty;

            WebRequest webRequest = WebRequest.Create(requestUriString);
            WebResponse webResponse = await webRequest.GetResponseAsync();

            using (Stream receiveStream = webResponse.GetResponseStream())
            using (StreamReader streamReader = new StreamReader(receiveStream, Encoding.GetEncoding("utf-8")))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    response += line;
                }
            }

            return response;
        }
    }
}

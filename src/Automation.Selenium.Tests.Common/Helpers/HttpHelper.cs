using System;
using System.IO;
using System.Net;
using System.Text;
using Automation.Selenium.Tests.Common.Utils;

namespace Automation.Selenium.Tests.Common.Helpers
{
    public sealed class HttpHelper : HelperBase
    {
        public enum HttpType
        {
            Post,
            Get
        }

        private static readonly HttpHelper instance = new HttpHelper();

        private HttpHelper()
        {
        }

        public static HttpHelper GetInstance
        {
            get { return instance; }
        }

        /// <summary>
        ///     Return Response Code
        /// </summary>
        /// <param name="url"></param>
        /// <param name="httpType"></param>
        /// <returns></returns>
        public int GetResponseCode(string url, HttpType httpType)
        {
            Log.Debug("{httpType} ::: {url}", url, httpType);

            var request = (HttpWebRequest) WebRequest.Create(url);
            request.Method = httpType.ToString().ToUpper();
            HttpWebResponse response;
            try
            {
                if (httpType == HttpType.Post)
                {
                    var encoding = new ASCIIEncoding();
                    var bytesToWrite = encoding.GetBytes(string.Empty);
                    request.ContentLength = bytesToWrite.Length;
                }
                response = (HttpWebResponse) request.GetResponse();
            }
            catch
            {
                return (int) Constants.HttpStatusCode.NotFound;
            }

            var status = response.StatusCode;
            return (int) status;
        }

        /// <summary>
        ///     Performs and HTTP Post
        /// </summary>
        /// <param name="postUrl"></param>
        /// <param name="postBody"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public Constants.HttpCustomResponse HttpPost(string postUrl, string postBody, string username, string password,
            string contentType)
        {
            var httpCustomResponse = new Constants.HttpCustomResponse();
            var request = (HttpWebRequest) WebRequest.Create(postUrl);
            var encoding = new ASCIIEncoding();

            var bytesToWrite = encoding.GetBytes(postBody);

            request.Method = "POST";
            request.ContentLength = bytesToWrite.Length;
            request.ContentType = contentType;
            request.Headers["Authorization"] = GetBasicAuth(username, password);

            var newStream = request.GetRequestStream();
            newStream.Write(bytesToWrite, 0, bytesToWrite.Length);
            newStream.Close();

            try
            {
                var response = (HttpWebResponse) request.GetResponse();
                var dataStream = response.GetResponseStream();
                if (dataStream != null)
                {
                    var reader = new StreamReader(dataStream);

                    var responseFromServer = reader.ReadToEnd();

                    httpCustomResponse.Body = responseFromServer;
                    httpCustomResponse.Code = (int) response.StatusCode;
                }

                return httpCustomResponse;
            }
            catch (WebException e)
            {
                httpCustomResponse.Code = (int) ((HttpWebResponse) e.Response).StatusCode;
            }

            return httpCustomResponse;
        }

        private static string GetBasicAuth(string username, string password)
        {
            var authInfo = username + ":" + password;
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            return "Basic " + authInfo;
        }

        /// <summary>
        ///     Perform a HTTP Delete
        /// </summary>
        /// <param name="deleteUrl"></param>
        /// <param name="contentType"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public HttpStatusCode HttpDelete(string deleteUrl, string contentType, string username, string password)
        {
            var request = (HttpWebRequest) WebRequest.Create(deleteUrl);
            request.Method = "DELETE";
            request.ContentType = contentType;
            request.Headers["Authorization"] = GetBasicAuth(username, password);
            var response = (HttpWebResponse) request.GetResponse();
            return response.StatusCode;
        }
    }
}
using BuilderInterpreter.Models.BuilderModels;
using Lime.Protocol;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BuilderInterpreter.Helper
{
    static class CustomActionHelper
    {
        public static Task<HttpResponseMessage> ExecuteHttpRequest(this ProcessHttp processHttp)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var requestMessage = new HttpRequestMessage
                {
                    RequestUri = new Uri(processHttp.Uri),
                    Method = new HttpMethod(processHttp.Method.ToString())
                };

                processHttp.Headers.ForEach(x => requestMessage.Headers.Add(x.Key, x.Value));

                if (!string.IsNullOrEmpty(processHttp.Body))
                    requestMessage.Content = new StringContent(processHttp.Body);

                return httpClient.SendAsync(requestMessage);
            }
        }

        public static T MergeObjects<T>(T oldObject, T objectToMerge)
        {
            Type t = typeof(T);

            var properties = t.GetProperties().Where(prop => prop.CanRead && prop.CanWrite);

            foreach (var prop in properties)
            {
                var value = prop.GetValue(objectToMerge, null);
                if (value != null)
                    prop.SetValue(oldObject, value, null);
            }

            return oldObject;
        }
    }
}

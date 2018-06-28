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
        public static async Task<HttpResponseMessage> ExecuteHttpRequest(this ProcessHttp processHttp)
        {
            using (HttpClient httpClient = new HttpClient(new RetryHandler(1)))
            {
                var requestMessage = new HttpRequestMessage
                {
                    RequestUri = new Uri(processHttp.Uri),
                    Method = new HttpMethod(processHttp.Method.ToString())
                };

                processHttp.Headers.ForEach(x => requestMessage.Headers.TryAddWithoutValidation(x.Key, x.Value));

                if (!string.IsNullOrEmpty(processHttp.Body))
                {
                    var content = new StringContent(processHttp.Body);
                    content.Headers.Clear();
                    processHttp.Headers.ForEach(x => content.Headers.TryAddWithoutValidation(x.Key, x.Value));
                    requestMessage.Content = content;
                }

                return await httpClient.SendAsync(requestMessage);
            }
        }

        public static T MergeObjects<T>(T oldObject, T objectToMerge)
        {
            Type t = typeof(T);

            var properties = t.GetProperties().Where(prop => prop.CanRead && prop.CanWrite);

            foreach (var prop in properties)
            {
                var value = prop.GetValue(objectToMerge, null);
                if (value != null && !(value is string && string.IsNullOrWhiteSpace(value.ToString())))
                    prop.SetValue(oldObject, value, null);
            }

            var fields = t.GetFields().Where(field => field.IsPublic);

            foreach (var field in fields)
            {
                var value = field.GetValue(objectToMerge);
                if (value != null && !(value is string && string.IsNullOrWhiteSpace(value.ToString())))
                    field.SetValue(oldObject, value);
            }

            return oldObject;
        }
    }
}

using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BuilderInterpreter.Models.BuilderModels;
using Lime.Protocol;

namespace BuilderInterpreter.Helper
{
    internal static class CustomActionHelper
    {
        public static T MergeObjects<T>(T oldObject, T objectToMerge)
        {
            var t = typeof(T);

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
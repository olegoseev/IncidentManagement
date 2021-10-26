using IoT.IncidentManagement.ClientApp.Contracts;
using IoT.IncidentManagement.ClientServices.Exceptions;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientServices.Services
{
    public partial class AppClient : IAppClient
    {
        partial void UpdateJsonSerializerSettings(JsonSerializerSettings settings);
        partial void PrepareRequest(HttpClient client, HttpRequestMessage request, string url);
        partial void PrepareRequest(HttpClient client, HttpRequestMessage request, StringBuilder urlBuilder);
        partial void ProcessResponse(HttpClient client, HttpResponseMessage response);

        protected struct ObjectResponseResult<O>
        {
            public ObjectResponseResult(O responseObject, string responseText)
            {
                this.Object = responseObject;
                this.Text = responseText;
            }

            public O Object { get; }

            public string Text { get; }
        }

        public bool ReadResponseAsString { get; set; }

        protected virtual async Task<ObjectResponseResult<O>> ReadObjectResponseAsync<O>(HttpResponseMessage response, IReadOnlyDictionary<string, IEnumerable<string>> headers)
        {
            if (response == null || response.Content == null)
            {
                return new ObjectResponseResult<O>(default, string.Empty);
            }

            if (ReadResponseAsString)
            {
                var responseText = await response.Content.ReadAsStringAsync();
                try
                {
                    var typedBody = JsonConvert.DeserializeObject<O>(responseText, JsonSerializerSettings);
                    return new ObjectResponseResult<O>(typedBody, responseText);
                }
                catch (JsonException exception)
                {
                    var message = "Could not deserialize the response body string as " + typeof(O).FullName + ".";
                    throw new ApiException(message, (int)response.StatusCode, responseText, headers, exception);
                }
            }
            else
            {
                try
                {
                    using var responseStream = await response.Content.ReadAsStreamAsync();
                    using var streamReader = new System.IO.StreamReader(responseStream);
                    using var jsonTextReader = new JsonTextReader(streamReader);
                    var serializer = JsonSerializer.Create(JsonSerializerSettings);
                    var typedBody = serializer.Deserialize<O>(jsonTextReader);
                    return new ObjectResponseResult<O>(typedBody, string.Empty);
                }
                catch (JsonException exception)
                {
                    var message = "Could not deserialize the response body stream as " + typeof(O).FullName + ".";
                    throw new ApiException(message, (int)response.StatusCode, string.Empty, headers, exception);
                }
            }
        }

        private string ConvertToString(object value, CultureInfo cultureInfo)
        {
            if (value == null)
            {
                return null;
            }

            if (value is Enum)
            {
                var name = Enum.GetName(value.GetType(), value);
                if (name != null)
                {
                    var field = IntrospectionExtensions.GetTypeInfo(value.GetType()).GetDeclaredField(name);
                    if (field != null)
                    {
                        if (CustomAttributeExtensions.GetCustomAttribute(field, typeof(EnumMemberAttribute)) is EnumMemberAttribute attribute)
                        {
                            return attribute.Value ?? name;
                        }
                    }

                    return Convert.ToString(Convert.ChangeType(value, Enum.GetUnderlyingType(value.GetType()), cultureInfo));
                }
            }
            else if (value is bool boolean)
            {
                return System.Convert.ToString(boolean, cultureInfo).ToLowerInvariant();
            }
            else if (value is byte[] bytes)
            {
                return Convert.ToBase64String(bytes);
            }
            else if (value.GetType().IsArray)
            {
                var array = Enumerable.OfType<object>((Array)value);
                return string.Join(",", Enumerable.Select(array, o => ConvertToString(o, cultureInfo)));
            }

            var result = Convert.ToString(value, cultureInfo);
            return (result is null) ? string.Empty : result;
        }
    }
}


using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientApp.Responses
{
    public class RequestResponse<T> where T : class
    {
        [JsonProperty("success", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public bool Success { get; set; }

        [JsonProperty("message", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        [JsonProperty("validationErrors", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<string> ValidationErrors { get; set; }

        [JsonProperty("data", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public T Item { get; set; }
    }
}

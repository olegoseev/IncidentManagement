using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientServices.Utils
{
    public class ProblemDetails
    {
        [JsonProperty("type", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("title", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("status", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public int? Status { get; set; }

        [JsonProperty("detail", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Detail { get; set; }

        [JsonProperty("instance", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Instance { get; set; }

        private IDictionary<string, object> _additionalProperties = new Dictionary<string, object>();

        [Newtonsoft.Json.JsonExtensionData]
        public IDictionary<string, object> AdditionalProperties
        {
            get { return _additionalProperties; }
            set { _additionalProperties = value; }
        }

    }
}

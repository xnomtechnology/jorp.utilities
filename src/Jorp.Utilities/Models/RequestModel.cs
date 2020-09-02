using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Globalization;

namespace Jorp.Utilities.Models
{
    public partial class RequestModel
    {
        [JsonProperty("@schema")]
        public Uri Schema { get; set; }

        [JsonProperty("form")]
        public Form Form { get; set; }
    }

    public partial class Form
    {
        [JsonProperty("properties")]
        public Properties Properties { get; set; }

        [JsonProperty("values")]
        public Values[] Values { get; set; }
    }

    public partial class Properties
    {
        [JsonProperty("$type")]
        public string Type { get; set; }

        [JsonProperty("$name")]
        public string Name { get; set; }
    }

    public partial class Values
    {
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("properties")]
        public ValueProperties Properties { get; set; }
    }

    public partial class ValueProperties
    {
        [JsonProperty("$type")]
        public string Type { get; set; }
    }

    public partial class RequestModel
    {
        public static RequestModel FromJson(string json) => JsonConvert.DeserializeObject<RequestModel>(json, SchemaConverter.Settings);
    }

    public static class SchemaSerialize
    {
        public static string ToJson(this RequestModel self) => JsonConvert.SerializeObject(self, SchemaConverter.Settings);
    }

    internal static class SchemaConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}

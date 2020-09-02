using Jorp.Utilities.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jorp.Utilities.Extentions
{
    public static class SchemaExtentions
    {
        /// <summary>
        /// Deserialize from json to typed object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T FromJsonToModel<T>(this string json) => 
            string.IsNullOrEmpty(json) ? default : JsonConvert.DeserializeObject<T>(json);

        /// <summary>
        /// Serialize from model to Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns>string with serialize json content</returns>
        public static string ToJsonFromModel<T>(this T model) => JsonConvert.SerializeObject(model);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static bool IsValid<T>(this string json, string schema) 
        {
            if (string.IsNullOrEmpty(json))
               return false;
            JsonSchema parsedSchema = JsonSchema.Parse(schema);
            var jObject = JObject.Parse(json);

            return jObject.IsValid(parsedSchema);
        }


        public static T TryParseJson<T>(this string json, string schema) where T : new()
        {
            JsonSchema parsedSchema = JsonSchema.Parse(schema);
            JObject jObject = JObject.Parse(json);

            return jObject.IsValid(parsedSchema) ? JsonConvert.DeserializeObject<T>(json) : default(T);
        }
    }
}

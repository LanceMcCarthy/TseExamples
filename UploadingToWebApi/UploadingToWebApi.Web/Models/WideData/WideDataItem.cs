using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace QuickTypeUploadingToWebApi.Web.Models.WideData
{
    public enum WideDataItemValue { CellDataTest };

    public class WideDataItem
    {
        public static Dictionary<string, WideDataItemValue>[] FromJson(string json)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, WideDataItemValue>[]>(json, QuickTypeUploadingToWebApi.Web.Models.WideData.Converter.Settings);
        }
    }

    public static class Serialize
    {
        public static string ToJson(this Dictionary<string, WideDataItemValue>[] self)
        {
            return JsonConvert.SerializeObject(self, QuickTypeUploadingToWebApi.Web.Models.WideData.Converter.Settings);
        }
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                WideDataItemValueConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class WideDataItemValueConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(WideDataItemValue) || t == typeof(WideDataItemValue?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "Cell data test")
            {
                return WideDataItemValue.CellDataTest;
            }
            throw new Exception("Cannot unmarshal type WideDataItemValue");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (WideDataItemValue)untypedValue;
            if (value == WideDataItemValue.CellDataTest)
            {
                serializer.Serialize(writer, "Cell data test");
                return;
            }
            throw new Exception("Cannot marshal type WideDataItemValue");
        }

        public static readonly WideDataItemValueConverter Singleton = new WideDataItemValueConverter();
    }
}
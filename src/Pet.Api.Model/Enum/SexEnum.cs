using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace Pet.Api.Model.Enum
{
    public enum SexEnum
    {
        [Newtonsoft.Json.JsonConverter(typeof(JsonStringEnumConverter))]
        Male,

        [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
        Female
    }
}

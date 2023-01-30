using System.Text.Json.Serialization;

namespace Pet.Api.Model.Enum
{
    public enum SpecieEnum
    {
        [Newtonsoft.Json.JsonConverter(typeof(JsonStringEnumConverter))]
        Cat = 0,

        [Newtonsoft.Json.JsonConverter(typeof(JsonStringEnumConverter))]
        Dog = 1
    }
}

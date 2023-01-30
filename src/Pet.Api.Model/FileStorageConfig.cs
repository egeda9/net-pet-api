using Newtonsoft.Json;

namespace Pet.Api.Model
{
    public class FileStorageConfig
    {
        public const string Root = "FileStorageConfig";

        [JsonProperty("filePath")]
        public string FilePath { get; set; } = string.Empty;
    }
}

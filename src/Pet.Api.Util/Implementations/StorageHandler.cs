using Microsoft.Extensions.Options;
using Pet.Api.Model;

namespace Pet.Api.Util.Implementations
{
    public class StorageHandler : IStorageHandler
    {
        private readonly FileStorageConfig _fileStorageConfig;

        public StorageHandler(IOptions<FileStorageConfig> options)
        {
            this._fileStorageConfig = options.Value;
            VerifyFilePath();
        }

        /// <summary>
        /// Read storage json file
        /// </summary>
        /// <returns></returns>
        public async Task<string> ReadAsync()
        {
            return await File.ReadAllTextAsync(this._fileStorageConfig.FilePath);
        }

        /// <summary>
        /// Write to storage json file
        /// </summary>
        /// <param name="jsonData"></param>
        public async Task WriteAsync(string jsonData)
        {
            await File.WriteAllTextAsync(this._fileStorageConfig.FilePath, jsonData);
        }

        private void VerifyFilePath()
        {
            if (!File.Exists(this._fileStorageConfig.FilePath))
                File.Create(this._fileStorageConfig.FilePath);
        }
    }
}

namespace Pet.Api.Util
{
    public interface IStorageHandler
    {
        Task<string> ReadAsync();
        Task WriteAsync(string jsonData);
    }
}
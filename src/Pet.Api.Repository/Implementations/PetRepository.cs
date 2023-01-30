using Newtonsoft.Json;
using Pet.Api.Util;

namespace Pet.Api.Repository.Implementations
{
    public class PetRepository : IPetRepository
    {
        private readonly IStorageHandler _storageHandler;  

        public PetRepository(IStorageHandler storageHandler)
        {
            this._storageHandler = storageHandler ?? throw new ArgumentNullException(nameof(storageHandler));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IList<Model.Pet?>> GetAsync()
        {
            var jsonData = await this._storageHandler.ReadAsync();

            var pets = JsonConvert.DeserializeObject<List<Model.Pet>>(jsonData)
                       ?? new List<Model.Pet>();

            return pets;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pets"></param>
        /// <returns></returns>
        public async Task CreateAsync(IList<Model.Pet?> pets)
        {
            var jsonData = JsonConvert.SerializeObject(pets, new Newtonsoft.Json.Converters.StringEnumConverter());
            await this._storageHandler.WriteAsync(jsonData);
        }
    }
}

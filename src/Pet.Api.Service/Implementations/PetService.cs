using Pet.Api.Repository;

namespace Pet.Api.Service.Implementations
{
    public class PetService : IPetService
    {
        private readonly IPetRepository _petRepository;

        public PetService(IPetRepository petRepository)
        {
            this._petRepository = petRepository ?? throw new ArgumentNullException(nameof(petRepository));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pet"></param>
        /// <returns></returns>
        public async Task CreateAsync(Model.Pet pet)
        {
            pet.Id = Guid.NewGuid();
            pet.CreatedAt = DateTime.Now;
            var pets = await this._petRepository.GetAsync();

            pets.Add(pet);

            await this._petRepository.CreateAsync(pets);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IList<Model.Pet?>> GetAsync()
        {
            return await this._petRepository.GetAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Model.Pet?> GetAsync(Guid id)
        {
            var pets = await this._petRepository.GetAsync();

            return pets.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pet"></param>
        /// <returns></returns>
        public async Task<Model.Pet?> UpdateAsync(Guid id, Model.Pet pet)
        {
            var pets = await this._petRepository.GetAsync();

            var currentPet = pets.FirstOrDefault(x => x.Id == id);

            if (currentPet != null)
            {
                currentPet.Age = pet.Age;
                currentPet.Name = pet.Name;
                currentPet.Breed = pet.Breed;
                currentPet.Sex = pet.Sex;
                currentPet.Specie = pet.Specie;
                currentPet.UpdatedAt = DateTime.Now;
            }

            await this._petRepository.CreateAsync(pets);

            return currentPet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            var pets = await this._petRepository.GetAsync();

            var currentPet = pets.FirstOrDefault(x => x.Id == id);

            if (currentPet != null)
                pets.Remove(currentPet);

            await this._petRepository.CreateAsync(pets);
        }
    }
}

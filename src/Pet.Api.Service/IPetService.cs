namespace Pet.Api.Service
{
    public interface IPetService
    {
        Task CreateAsync(Model.Pet pet);

        Task<IList<Model.Pet?>> GetAsync();

        Task<Model.Pet?> GetAsync(Guid id);

        Task<Model.Pet?> UpdateAsync(Guid id, Model.Pet pet);

        Task DeleteAsync(Guid id);
    }
}
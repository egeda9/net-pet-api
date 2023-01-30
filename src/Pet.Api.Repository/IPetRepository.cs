namespace Pet.Api.Repository
{
    public interface IPetRepository
    {
        Task<IList<Model.Pet?>> GetAsync();

        Task CreateAsync(IList<Model.Pet?> pets);
    }
}
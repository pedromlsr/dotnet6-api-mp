using MP.ApiDotNet6.Domain.Entities;
using MP.ApiDotNet6.Domain.FiltersDb;

namespace MP.ApiDotNet6.Domain.Repositories
{
    public interface IPersonRepository
    {
        Task<ICollection<Person>> GetAllAsync();
        Task<PagedBaseResponse<Person>> GetPagedAsync(PersonFilterDb request);
        Task<Person> GetByIdAsync(int id);
        Task<int> GetIdByDocumentAsync(string document);
        Task<Person> CreateAsync(Person person);
        Task EditAsync(Person person);
        Task DeleteAsync(Person person);
    }
}

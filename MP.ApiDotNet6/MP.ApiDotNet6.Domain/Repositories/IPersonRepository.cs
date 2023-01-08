using MP.ApiDotNet6.Domain.Entities;

namespace MP.ApiDotNet6.Domain.Repositories
{
    public interface IPersonRepository
    {
        Task<ICollection<Person>> GetAllAsync();
        Task<Person> GetByIdAsync(int id);
        Task<Person> CreateAsync(Person person);
        Task EditAsync(Person person);
        Task DeleteAsync(Person person);
    }
}

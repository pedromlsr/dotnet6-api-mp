using MP.ApiDotNet6.Application.DTOs;
using MP.ApiDotNet6.Domain.FiltersDb;
using MP.ApiDotNet6.Domain.Repositories;

namespace MP.ApiDotNet6.Application.Services.Interfaces
{
    public interface IPersonService
    {
        Task<ResultService<ICollection<PersonDTO>>> GetAllAsync();
        Task<ResultService<PagedBaseResponseDTO<PersonDTO>>> GetPagedAsync(PersonFilterDb personFilterDb);
        Task<ResultService<PersonDTO>> GetByIdAsync(int id);
        Task<ResultService<PersonDTO>> CreateAsync(PersonDTO personDTO);
        Task<ResultService> EditAsync(PersonDTO personDTO);
        Task<ResultService> DeleteAsync(int id);
    }
}

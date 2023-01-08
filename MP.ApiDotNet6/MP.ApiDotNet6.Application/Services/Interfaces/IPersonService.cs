using MP.ApiDotNet6.Application.DTOs;

namespace MP.ApiDotNet6.Application.Services.Interfaces
{
    public interface IPersonService
    {
        Task<ResultService<ICollection<PersonDTO>>> GetByIdAsync();
        Task<ResultService<PersonDTO>> GetByIdAsync(int id);
        Task<ResultService<PersonDTO>> CreateAsync(PersonDTO personDTO);
        Task<ResultService> EditAsync(PersonDTO personDTO);
        Task<ResultService> DeleteAsync(int id);
    }
}

using AutoMapper;
using MP.ApiDotNet6.Application.DTOs;
using MP.ApiDotNet6.Application.DTOs.Validations;
using MP.ApiDotNet6.Application.Services.Interfaces;
using MP.ApiDotNet6.Domain.Entities;
using MP.ApiDotNet6.Domain.FiltersDb;
using MP.ApiDotNet6.Domain.Repositories;

namespace MP.ApiDotNet6.Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public PersonService(IPersonRepository personRepository, IMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public async Task<ResultService<ICollection<PersonDTO>>> GetAllAsync()
        {
            var people = await _personRepository.GetAllAsync();

            return ResultService.Ok(_mapper.Map<ICollection<PersonDTO>>(people));
        }

        public async Task<ResultService<PagedBaseResponseDTO<PersonDTO>>> GetPagedAsync(PersonFilterDb personFilterDb)
        {
            var peoplePaged = await _personRepository.GetPagedAsync(personFilterDb);
            var result = new PagedBaseResponseDTO<PersonDTO>(
                peoplePaged.TotalRegisters, _mapper.Map<List<PersonDTO>>(peoplePaged.Data));

            return ResultService.Ok(result);
        }

        public async Task<ResultService<PersonDTO>> GetByIdAsync(int id)
        {
            var person = await _personRepository.GetByIdAsync(id);

            if (person == null) return ResultService.Fail<PersonDTO>("Pessoa não encontrada!");

            return ResultService.Ok(_mapper.Map<PersonDTO>(person));
        }

        public async Task<ResultService<PersonDTO>> CreateAsync(PersonDTO personDTO)
        {
            if(personDTO == null)
                return ResultService.Fail<PersonDTO>("Objeto deve ser informado!");

            var result = new PersonDTOValidator().Validate(personDTO);

            if (!result.IsValid)
                return ResultService.RequestError<PersonDTO>("Problemas na validação dos campos!", result);

            var person = _mapper.Map<Person>(personDTO);
            var data = await _personRepository.CreateAsync(person);

            return ResultService.Ok(_mapper.Map<PersonDTO>(data));
        }

        public async Task<ResultService> EditAsync(PersonDTO personDTO)
        {
            if (personDTO == null) return ResultService.Fail<PersonDTO>("Objeto deve ser informado!");

            var result = new PersonDTOValidator().Validate(personDTO);

            if (!result.IsValid)
                return ResultService.RequestError<PersonDTO>("Problemas na validação dos campos!", result);

            var person = await _personRepository.GetByIdAsync(personDTO.Id);

            if (person == null) return ResultService.Fail<PersonDTO>("Pessoa não encontrada!");

            person = _mapper.Map(personDTO, person);
            await _personRepository.EditAsync(person);

            return ResultService.Ok($"Pessoa de id:{person.Id} foi editada com sucesso.");
        }

        public async Task<ResultService> DeleteAsync(int id)
        {
            var person = await _personRepository.GetByIdAsync(id);

            if (person == null) return ResultService.Fail<PersonDTO>("Pessoa não encontrada!");

            await _personRepository.DeleteAsync(person);

            return ResultService.Ok($"Pessoa de id:{id} foi deletada com sucesso.");
        }
    }
}

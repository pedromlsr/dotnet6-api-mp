﻿using Microsoft.AspNetCore.Mvc;
using MP.ApiDotNet6.Application.DTOs;
using MP.ApiDotNet6.Application.Services.Interfaces;

namespace MP.ApiDotNet6.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            var result = await _personService.GetAllAsync();

            if(result.IsSuccess) return Ok(result);

            return BadRequest(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            var result = await _personService.GetByIdAsync(id);

            if(result.IsSuccess) return Ok(result);

            return BadRequest(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] PersonDTO personDTO)
        {
            var result = await _personService.CreateAsync(personDTO);
            
            if(result.IsSuccess) return Ok(result);

            return BadRequest(result);
        }

        [HttpPut]
        public async Task<ActionResult> EditAsync([FromBody] PersonDTO personDTO)
        {
            var result = await _personService.EditAsync(personDTO);

            if (result.IsSuccess) return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var result = await _personService.DeleteAsync(id);

            if (result.IsSuccess) return Ok(result);

            return BadRequest(result);
        }
    }
}

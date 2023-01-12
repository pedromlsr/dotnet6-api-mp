using Microsoft.AspNetCore.Mvc;
using MP.ApiDotNet6.Application.DTOs;
using MP.ApiDotNet6.Application.Services.Interfaces;
using MP.ApiDotNet6.Domain.FiltersDb;

namespace MP.ApiDotNet6.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            var result = await _productService.GetAllAsync();

            if(result.IsSuccess) return Ok(result);

            return BadRequest(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            var result = await _productService.GetByIdAsync(id);

            if(result.IsSuccess) return Ok(result);

            return BadRequest(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] ProductDTO productDTO)
        {
            var result = await _productService.CreateAsync(productDTO);
            
            if(result.IsSuccess) return Ok(result);

            return BadRequest(result);
        }

        [HttpPut]
        public async Task<ActionResult> EditAsync([FromBody] ProductDTO productDTO)
        {
            var result = await _productService.EditAsync(productDTO);

            if (result.IsSuccess) return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var result = await _productService.DeleteAsync(id);

            if (result.IsSuccess) return Ok(result);

            return BadRequest(result);
        }
    }
}

using AutoMapper;
using MP.ApiDotNet6.Application.DTOs;
using MP.ApiDotNet6.Application.DTOs.Validations;
using MP.ApiDotNet6.Application.Services.Interfaces;
using MP.ApiDotNet6.Domain.Entities;
using MP.ApiDotNet6.Domain.Repositories;

namespace MP.ApiDotNet6.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ResultService<ICollection<ProductDTO>>> GetByIdAsync()
        {
            var products = await _productRepository.GetAllAsync();

            return ResultService.Ok(_mapper.Map<ICollection<ProductDTO>>(products));
        }

        public async Task<ResultService<ProductDTO>> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null) return ResultService.Fail<ProductDTO>("Produto não encontrado!");

            return ResultService.Ok(_mapper.Map<ProductDTO>(product));
        }

        public async Task<ResultService<ProductDTO>> CreateAsync(ProductDTO productDTO)
        {
            if(productDTO == null)
                return ResultService.Fail<ProductDTO>("Objeto deve ser informado!");

            var result = new ProductDTOValidator().Validate(productDTO);

            if (!result.IsValid)
                return ResultService.RequestError<ProductDTO>("Problemas na validação dos campos!", result);

            var product = _mapper.Map<Product>(productDTO);
            var data = await _productRepository.CreateAsync(product);

            return ResultService.Ok(_mapper.Map<ProductDTO>(data));
        }

        public async Task<ResultService> EditAsync(ProductDTO productDTO)
        {
            if (productDTO == null) return ResultService.Fail("Objeto deve ser informado!");

            var result = new ProductDTOValidator().Validate(productDTO);

            if (!result.IsValid)
                return ResultService.RequestError<ProductDTO>("Problemas na validação dos campos!", result);

            var product = await _productRepository.GetByIdAsync(productDTO.Id);

            if (product == null) return ResultService.Fail("Produto não encontrado!");

            product = _mapper.Map(productDTO, product);
            await _productRepository.EditAsync(product);

            return ResultService.Ok($"Produto de id:{product.Id} foi editado com sucesso.");
        }

        public async Task<ResultService> DeleteAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null) return ResultService.Fail("Produto não encontrado!");

            await _productRepository.DeleteAsync(product);
            return ResultService.Ok($"Produto de id:{id} foi deletado com sucesso.");
        }
    }
}

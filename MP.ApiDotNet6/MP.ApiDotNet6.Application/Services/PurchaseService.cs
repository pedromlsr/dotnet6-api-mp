using AutoMapper;
using MP.ApiDotNet6.Application.DTOs;
using MP.ApiDotNet6.Application.DTOs.Validations;
using MP.ApiDotNet6.Application.Services.Interfaces;
using MP.ApiDotNet6.Domain.Entities;
using MP.ApiDotNet6.Domain.Repositories;

namespace MP.ApiDotNet6.Application.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PurchaseService(
            IPurchaseRepository purchaseRepository,
            IPersonRepository personRepository,
            IProductRepository productRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _purchaseRepository = purchaseRepository;
            _personRepository = personRepository;
            _productRepository = productRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultService<ICollection<PurchaseDetailDTO>>> GetAllAsync()
        {
            var purchases = await _purchaseRepository.GetAllAsync();

            return ResultService.Ok(_mapper.Map<ICollection<PurchaseDetailDTO>>(purchases));
        }

        public async Task<ResultService<PurchaseDetailDTO>> GetByIdAsync(int id)
        {
            var purchase = await _purchaseRepository.GetByIdAsync(id);

            if (purchase == null) return ResultService.Fail<PurchaseDetailDTO>("Compra não encontrada!");

            return ResultService.Ok(_mapper.Map<PurchaseDetailDTO>(purchase));
        }

        public async Task<ResultService<PurchaseDTO>> CreateAsync(PurchaseDTO purchaseDTO)
        {
            if(purchaseDTO == null)
                return ResultService.Fail<PurchaseDTO>("Objeto deve ser informado!");

            var result = new PurchaseDTOValidator().Validate(purchaseDTO);

            if (!result.IsValid)
                return ResultService.RequestError<PurchaseDTO>("Problemas na validação dos campos!", result);

            try
            {
                await _unitOfWork.BeginTransaction();

                var personId = await _personRepository.GetIdByDocumentAsync(purchaseDTO.Document);
                var productId = await _productRepository.GetIdByCodErpAsync(purchaseDTO.CodErp);

                if (productId == 0)
                {
                    var product = new Product(purchaseDTO.ProductName, purchaseDTO.CodErp, purchaseDTO.Price);
                    await _productRepository.CreateAsync(product);
                    productId = product.Id;
                }

                var purchase = new Purchase(personId, productId);
                var data = await _purchaseRepository.CreateAsync(purchase);
                purchaseDTO.Id = data.Id;

                await _unitOfWork.Commit();

                return ResultService.Ok(purchaseDTO);
            }
            catch(Exception ex)
            {
                await _unitOfWork.Rollback();

                return ResultService.Fail<PurchaseDTO>($"{ex.Message}");
            }
        }

        public async Task<ResultService> EditAsync(PurchaseDTO purchaseDTO)
        {
            if (purchaseDTO == null) return ResultService.Fail<PurchaseDTO>("Objeto deve ser informado!");

            var result = new PurchaseDTOValidator().Validate(purchaseDTO);

            if (!result.IsValid)
                return ResultService.RequestError<PurchaseDTO>("Problemas na validação dos campos!", result);

            var purchase = await _purchaseRepository.GetByIdAsync(purchaseDTO.Id);

            if (purchase == null) return ResultService.Fail<PurchaseDTO>("Compra não encontrada!");

            var personId = await _personRepository.GetIdByDocumentAsync(purchaseDTO.Document);
            var productId = await _productRepository.GetIdByCodErpAsync(purchaseDTO.CodErp);
            
            purchase.Edit(purchase.Id, personId, productId);
            await _purchaseRepository.EditAsync(purchase);

            return ResultService.Ok($"Compra de id:{purchase.Id} foi editata com sucesso.");
        }

        public async Task<ResultService> DeleteAsync(int id)
        {
            var purchase = await _purchaseRepository.GetByIdAsync(id);

            if (purchase == null) return ResultService.Fail<PurchaseDTO>("Compra não encontrada!");

            await _purchaseRepository.DeleteAsync(purchase);

            return ResultService.Ok($"Compra de id:{id} foi deletada com sucesso.");
        }
    }
}

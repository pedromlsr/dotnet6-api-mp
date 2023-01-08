using FluentValidation;

namespace MP.ApiDotNet6.Application.DTOs.Validations
{
    public class ProductDTOValidator : AbstractValidator<ProductDTO>
    {
        public ProductDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Nome deve ser informado!");

            RuleFor(x => x.CodErp)
                .NotEmpty()
                .NotNull()
                .WithMessage("Código erp deve ser informado!");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Preço deve ser maior que 0!");
        }
    }
}

using FluentValidation;
using WebAPI.Dtos;

namespace WebAPI.Features.Product.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name must be provided")
            .MaximumLength(50).WithMessage("Name must not exceed 50 characters");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description must be provided");
        
        RuleFor(x => x.Price)
            .NotEmpty().WithMessage("Price must be provided");
        RuleFor(x => x.Sku)
            .NotEmpty().WithMessage("Sku must be provided");
        RuleFor(x => x.QuantityInStock)
            .Must(quantity => quantity > 0).WithMessage("Quantity must be greater than 0");
    }
}
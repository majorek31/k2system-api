using System.Text.RegularExpressions;
using FluentValidation;
using WebAPI.Dtos;
using WebAPI.Repositories.ProductRepository;

namespace WebAPI.Features.Order.Commands.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderDto>
{
    public CreateOrderCommandValidator(IProductRepository productRepository)
    {
        RuleFor(x => x.OrderItems.Count)
            .GreaterThan(0).WithMessage("Cannot create an order with no items");
        
        RuleFor(x => x.PostalCode)
            .Must((postalCode) =>  Regex.IsMatch(postalCode, @"^\d{2}-\d{3}$"))
            .WithMessage("Postal code is invalid");
        
        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required")
            .NotNull().WithMessage("City is required");
        
        RuleFor(x => x.StreetName)
            .NotEmpty().WithMessage("StreetName is required")
            .NotNull().WithMessage("StreetName is required");
        
        RuleFor(x => x.OrderItems)
            .MustAsync(async (items, cancellationToken) =>
            {
                // silly hack fix or database expload
                foreach (var item in items)
                {
                    var product = await productRepository.GetByIdAsync(item.ProductId);
                    if (product == null)
                        return false;
                }
                return true;
            }).WithMessage("At least one items is not valid");
        
        RuleFor(x => x.OrderItems)
            .MustAsync(async (items, cancellationToken) =>
            {
                foreach (var item in items)
                {
                    var product = await productRepository.GetByIdAsync(item.ProductId);
                    if (product == null) return false;
                    if (product.QuantityInStock < item.Quantity)
                    {
                        return false;
                    }
                }
                return true;
            })
            .WithMessage("Stock insufficient");
    }
}
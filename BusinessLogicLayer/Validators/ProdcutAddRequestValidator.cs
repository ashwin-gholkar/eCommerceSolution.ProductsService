using BusinessLogicLayer.DTO;
using FluentValidation;

namespace BusinessLogicLayer.Validators
{
    public class ProdcutAddRequestValidator:AbstractValidator<ProductAddRequest>
    {
        public ProdcutAddRequestValidator()
        {
            RuleFor(temp => temp.ProductName)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100).WithMessage("Product name must not exceed 100 characters.");
            RuleFor(temp => temp.Category)
                .IsInEnum().WithMessage("Category is required.");
            RuleFor(temp => temp.UnitPrice)
                .InclusiveBetween(0,double.MaxValue).WithMessage("Unit price must be a non-negative value.");
            RuleFor(temp => temp.QuantityInStock)
                .InclusiveBetween(0, int.MaxValue).WithMessage("Quantity in stock must be a non-negative value.");
            
        }
    }
}

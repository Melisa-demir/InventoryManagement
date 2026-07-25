using FluentValidation;
using InventoryService.DTOs;

namespace InventoryService.Validators
{
    public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>    
    {
        public UpdateProductRequestValidator() 
        {
            RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Ürün adı boş bırakılamaz.")
            .MaximumLength(100)
            .WithMessage("Ürün adı en fazla 100 karakter olabilir.");

            RuleFor(x => x.Description)
                .MaximumLength(500)
                .WithMessage("Açıklama en fazla 500 karakter olabilir.");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Ürün fiyatı 0'dan büyük olmalıdır.");

            RuleFor(x => x.StockQuantity)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Stok miktarı negatif olamaz.");

            RuleFor(x => x.MinimumStockLevel)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Minimum stok seviyesi negatif olamaz.");

            RuleFor(x => x.MinimumStockLevel)
                .LessThanOrEqualTo(x => x.StockQuantity)
                .WithMessage(
                    "Minimum stok seviyesi mevcut stok miktarından büyük olamaz.");
    }
    }
}

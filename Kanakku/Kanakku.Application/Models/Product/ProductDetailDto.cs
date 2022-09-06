using FluentValidation;
using Kanakku.Application.Requests.Product;
using Kanakku.Shared.Utilities;

namespace Kanakku.Application.Models.Product
{
    public class ProductDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortCode { get; set; }
        public List<ProductInstanceDto> ProductVariants { get; set; }
        public List<WorkDto> Works { get; set; }
        public int? ImageId { get; set; }
    }

    public class ProductDetailDtoValidator : AppAbstractValidator<ProductDetailDto>
    {
        public ProductDetailDtoValidator()
        {
            RuleFor(x => x.ShortCode).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.ProductVariants).NotNull().NotEmpty();

            RuleForEach(x => x.ProductVariants)
                .Must(variant => variant.SizeId > 0 && variant.Quantity > 0);
        }
    }
}

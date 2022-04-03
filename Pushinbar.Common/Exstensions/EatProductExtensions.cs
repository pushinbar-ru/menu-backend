using Pushinbar.Common.DTOs;
using Pushinbar.Common.Models;

namespace Pushinbar.Common.Exstensions
{
    public static class EatProductExtensions
    {
        public static EatProductDto ToDto(this EatProduct product)
        {
            return new EatProductDto()
            {
                Id = product.Id,
                Name = product.Name,
                Photo = product.Photo,
                Description = product.Description,
                Price = product.Price,
                Type = product.Type,
                Rest = product.Rest,
                Status = product.Status,
                LikesCount = product.LikesCount,
                Barcode = product.Barcode
            };
        }
    }
}
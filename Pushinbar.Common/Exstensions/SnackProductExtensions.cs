using Pushinbar.Common.DTOs;
using Pushinbar.Common.DTOs.Snack;
using Pushinbar.Common.Models;

namespace Pushinbar.Common.Exstensions
{
    public static class SnackProductExtensions
    {
        public static SnackProductDto ToDto(this SnackProduct product)
        {
            return new SnackProductDto()
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
                Barcode = product.Barcode,
                Subcategories = product.Subcategories
            };
        }
    }
}
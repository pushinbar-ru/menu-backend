using Pushinbar.Common.DTOs;
using Pushinbar.Common.DTOs.NotAlcohol;
using Pushinbar.Common.Models;
using Pushinbar.Common.Models.NotAlcohol;

namespace Pushinbar.Common.Exstensions
{
    public static class NotAlcoholProductExtensions
    {
        public static NotAlcoholProductDto ToDto(this NotAlcoholProduct product)
        {
            return new NotAlcoholProductDto()
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
                Subcategories = product.Subcategories,
                Volume = product.Volume
            };
        }
    }
}
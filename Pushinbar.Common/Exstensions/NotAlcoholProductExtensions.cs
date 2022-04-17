using Pushinbar.Common.DTOs;
using Pushinbar.Common.DTOs.NotAlcohol;
using Pushinbar.Common.Models;

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
                Alc = product.Alc,
                IBU = product.IBU,
                Subcategories = product.Subcategories,
                UntappdUrl = product.UntappdUrl,
                Brewery = product.Brewery,
                Volume = product.Volume
            };
        }
    }
}
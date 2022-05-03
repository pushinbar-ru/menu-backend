using Pushinbar.Common.DTOs;
using Pushinbar.Common.DTOs.Alcohol;
using Pushinbar.Common.Entities;
using Pushinbar.Common.Models;
using Pushinbar.Common.Models.Alcohol;

namespace Pushinbar.Common.Exstensions
{
    public static class AlcoholProductExtensions
    {
        public static AlcoholProductDto ToDto(this AlcoholProduct product)
        {
            return new AlcoholProductDto()
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

        public static void UpdateFromEntity(this AlcoholProduct alcoholProduct, AlcoholEntity alcoholEntity)
        {
            alcoholProduct.Id = alcoholEntity.Id;
            alcoholProduct.Name = alcoholEntity.Name;
            alcoholProduct.Photo = alcoholEntity.Photo;
            alcoholProduct.Description = alcoholEntity.Description;
            alcoholProduct.Price = alcoholEntity.Price;
            alcoholProduct.Type = alcoholEntity.Type;
            alcoholProduct.Status = alcoholEntity.Status;
            alcoholProduct.LikesCount = alcoholEntity.LikesCount;
            alcoholProduct.Barcode = alcoholEntity.Barcode;
            alcoholProduct.Alc = alcoholEntity.Alc;
            alcoholProduct.IBU = alcoholEntity.IBU;
            alcoholProduct.Subcategories = alcoholEntity.Subcategories;
            alcoholProduct.UntappdUrl = alcoholEntity.UntappdUrl;
            alcoholProduct.Brewery = alcoholEntity.Brewery;
            alcoholProduct.Volume = alcoholEntity.Volume;
        }
    }
}
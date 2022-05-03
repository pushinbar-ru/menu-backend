using Pushinbar.Common.DTOs;
using Pushinbar.Common.DTOs.Eat;
using Pushinbar.Common.Entities;
using Pushinbar.Common.Models;
using Pushinbar.Common.Models.Eat;

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
                Barcode = product.Barcode,
                Subcategories = product.Subcategories
            };
        }
        public static void UpdateFromEntity(this EatProduct alcoholProduct, EatEntity alcoholEntity)
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
            alcoholProduct.Subcategories = alcoholEntity.Subcategories;
        }
    }
}
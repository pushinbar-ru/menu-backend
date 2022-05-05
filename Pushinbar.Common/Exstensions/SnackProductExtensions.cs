using Pushinbar.Common.DTOs;
using Pushinbar.Common.DTOs.Snack;
using Pushinbar.Common.Entities;
using Pushinbar.Common.Models;
using Pushinbar.Common.Models.Snack;

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
        
        public static void UpdateFromEntity(this SnackProduct alcoholProduct, SnackEntity alcoholEntity)
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
            alcoholProduct.Subcategories = alcoholEntity.Subcategories.Split(',');;
        }
    }
}
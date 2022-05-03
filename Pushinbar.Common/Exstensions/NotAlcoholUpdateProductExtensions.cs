using Pushinbar.Common.DTOs.NotAlcohol;
using Pushinbar.Common.Models.NotAlcohol;

namespace Pushinbar.Common.Exstensions
{
    public static class NotAlcoholUpdateProductExtensions
    {
        
        public static void UpdateFromDto(this NotAlcoholUpdateProduct alcoholUpdateProduct,
            NotAlcoholUpdateProductDto alcoholUpdateProductDto)
        {
            alcoholUpdateProduct.Name = alcoholUpdateProductDto.Name;
            alcoholUpdateProduct.Photo = alcoholUpdateProductDto.Photo;
            alcoholUpdateProduct.Description = alcoholUpdateProductDto.Description;
            alcoholUpdateProduct.Status = alcoholUpdateProductDto.Status;
            alcoholUpdateProduct.Barcode = alcoholUpdateProductDto.Barcode;
            alcoholUpdateProduct.Subcategories = alcoholUpdateProductDto.Subcategories;
            alcoholUpdateProduct.Volume = alcoholUpdateProductDto.Volume;
        }
    }
}
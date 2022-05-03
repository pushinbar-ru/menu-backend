using Pushinbar.Common.DTOs.Eat;
using Pushinbar.Common.Models.Eat;

namespace Pushinbar.Common.Exstensions
{
    public static class EatUpdateProductExtensions
    {
        public static void UpdateFromDto(this EatUpdateProduct alcoholUpdateProduct,
            EatUpdateProductDto alcoholUpdateProductDto)
        {
            alcoholUpdateProduct.Name = alcoholUpdateProductDto.Name;
            alcoholUpdateProduct.Photo = alcoholUpdateProductDto.Photo;
            alcoholUpdateProduct.Description = alcoholUpdateProductDto.Description;
            alcoholUpdateProduct.Status = alcoholUpdateProductDto.Status;
            alcoholUpdateProduct.Barcode = alcoholUpdateProductDto.Barcode;
            alcoholUpdateProduct.Subcategories = alcoholUpdateProductDto.Subcategories;
        }
    }
}
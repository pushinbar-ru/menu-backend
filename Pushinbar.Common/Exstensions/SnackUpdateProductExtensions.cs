using Pushinbar.Common.DTOs.Snack;
using Pushinbar.Common.Models.Snack;

namespace Pushinbar.Common.Exstensions
{
    public static class SnackUpdateProductExtensions
    {
        
        public static void UpdateFromDto(this SnackUpdateProduct alcoholUpdateProduct,
            SnackUpdateProductDto alcoholUpdateProductDto)
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
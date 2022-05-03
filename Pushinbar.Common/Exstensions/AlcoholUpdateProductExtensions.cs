using Pushinbar.Common.DTOs.Alcohol;
using Pushinbar.Common.Models.Alcohol;

namespace Pushinbar.Common.Exstensions
{
    public static class AlcoholUpdateProductExtensions
    {
        public static void UpdateFromDto(this AlcoholUpdateProduct alcoholUpdateProduct,
            AlcoholUpdateProductDto alcoholUpdateProductDto)
        {
            alcoholUpdateProduct.Name = alcoholUpdateProductDto.Name;
            alcoholUpdateProduct.Photo = alcoholUpdateProductDto.Photo;
            alcoholUpdateProduct.Description = alcoholUpdateProductDto.Description;
            alcoholUpdateProduct.Status = alcoholUpdateProductDto.Status;
            alcoholUpdateProduct.Barcode = alcoholUpdateProductDto.Barcode;
            alcoholUpdateProduct.Subcategories = alcoholUpdateProductDto.Subcategories;
            alcoholUpdateProduct.IBU = alcoholUpdateProductDto.IBU;
            alcoholUpdateProduct.UntappdUrl = alcoholUpdateProductDto.UntappdUrl;
            alcoholUpdateProduct.Brewery = alcoholUpdateProductDto.Brewery;
            alcoholUpdateProduct.Volume = alcoholUpdateProductDto.Volume;
        }
    }
}
using Pushinbar.Common.Entities;
using Pushinbar.Common.Models.Alcohol;

namespace Pushinbar.Common.Exstensions
{
    public static class AlcoholEntityExtensions
    {
        public static void ApplyUpdate(this AlcoholEntity alcoholEntity, AlcoholUpdateProduct alcoholUpdateProduct)
        {
            alcoholEntity.Name = alcoholUpdateProduct.Name;
            alcoholEntity.Photo = alcoholUpdateProduct.Photo;
            alcoholEntity.Description = alcoholUpdateProduct.Description;
            alcoholEntity.Status = alcoholUpdateProduct.Status;
            alcoholEntity.Barcode = alcoholUpdateProduct.Barcode;
            alcoholEntity.Subcategories = alcoholUpdateProduct.Subcategories;
            alcoholEntity.IBU = alcoholUpdateProduct.IBU;
            alcoholEntity.UntappdUrl = alcoholUpdateProduct.UntappdUrl;
            alcoholEntity.Brewery = alcoholUpdateProduct.Brewery;
            alcoholEntity.Volume = alcoholUpdateProduct.Volume;
        }
    }
}
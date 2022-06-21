using Pushinbar.Common.Entities;
using Pushinbar.Common.Models.NotAlcohol;

namespace Pushinbar.Common.Exstensions
{
    public static class NotAlcoholEntityExtensions
    {
        public static void ApplyUpdate(this NotAlcoholEntity alcoholEntity, NotAlcoholUpdateProduct alcoholUpdateProduct)
        {
            alcoholEntity.Name = alcoholUpdateProduct.Name;
            alcoholEntity.Photo = alcoholUpdateProduct.Photo;
            alcoholEntity.Description = alcoholUpdateProduct.Description;
            alcoholEntity.Status = alcoholUpdateProduct.Status;
            alcoholEntity.Barcode = alcoholUpdateProduct.Barcode;
            alcoholEntity.Subcategories = string.Join(",", alcoholUpdateProduct.Subcategories);
            alcoholEntity.Volume = alcoholUpdateProduct.Volume;
        }
    }
}
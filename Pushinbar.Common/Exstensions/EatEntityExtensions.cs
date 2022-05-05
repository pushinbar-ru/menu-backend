using Pushinbar.Common.Entities;
using Pushinbar.Common.Models.Eat;

namespace Pushinbar.Common.Exstensions
{
    public static class EatEntityExtensions
    {
        public static void ApplyUpdate(this EatEntity alcoholEntity, EatUpdateProduct alcoholUpdateProduct)
        {
            alcoholEntity.Name = alcoholUpdateProduct.Name;
            alcoholEntity.Photo = alcoholUpdateProduct.Photo;
            alcoholEntity.Description = alcoholUpdateProduct.Description;
            alcoholEntity.Status = alcoholUpdateProduct.Status;
            alcoholEntity.Barcode = alcoholUpdateProduct.Barcode;
            alcoholEntity.Subcategories = alcoholUpdateProduct.Subcategories.ToString();
        }
    }
}
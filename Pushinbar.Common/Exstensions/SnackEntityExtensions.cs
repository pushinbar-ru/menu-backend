using System.Linq;
using Pushinbar.Common.Entities;
using Pushinbar.Common.Models.Snack;

namespace Pushinbar.Common.Exstensions
{
    public static class SnackEntityExtensions
    {
        public static void ApplyUpdate(this SnackEntity alcoholEntity, SnackUpdateProduct alcoholUpdateProduct)
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
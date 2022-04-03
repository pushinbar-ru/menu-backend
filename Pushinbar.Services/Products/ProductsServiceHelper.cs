using System;
using System.Collections.Generic;
using System.Linq;
using Pushinbar.KonturMarket.Client.Models;

namespace Pushinbar.Services.Products
{
    public static class ProductsServiceHelper
    {
        private const string AlcoholGroupTitle = "Пиво";
        private const string NotAlcoholGroupTitle = "б/а напитки";
        private const string EatGroupTitle = "Еда";
        private const string SnackGroupTitle = "Снеки";

        public static bool IsAlcohol(Guid groupId, ProductGroup[] productGroups) =>
            GetMainGroupTitle(groupId, productGroups).Equals(AlcoholGroupTitle, StringComparison.OrdinalIgnoreCase);
        
        public static bool IsNotAlcohol(Guid groupId, ProductGroup[] productGroups) =>
            GetMainGroupTitle(groupId, productGroups).Equals(NotAlcoholGroupTitle, StringComparison.OrdinalIgnoreCase);
        
        public static bool IsEat(Guid groupId, ProductGroup[] productGroups) =>
            GetMainGroupTitle(groupId, productGroups).Equals(EatGroupTitle, StringComparison.OrdinalIgnoreCase);
        
        public static bool IsSnack(Guid groupId, ProductGroup[] productGroups) =>
            GetMainGroupTitle(groupId, productGroups).Equals(SnackGroupTitle, StringComparison.OrdinalIgnoreCase);

        public static float GetProductRest(Guid productId, IEnumerable<ProductRest> productRests)
        {
            return productRests.FirstOrDefault(rest => rest.ProductId.Equals(productId))?.Rest ?? 0;
        }

        private static string GetMainGroupTitle(Guid groupId, ProductGroup[] productGroups)
        {
            var productGroup = productGroups.FirstOrDefault(group => group.Id.Equals(groupId));
            
            if (productGroup == null)
                throw new ArgumentException("Get main group failed.");
            
            while (!productGroup.ParentId.Equals(null))
            {
                productGroup = productGroups.FirstOrDefault(group => group.Id.Equals(productGroup.ParentId));
            }

            return productGroup.Name;
        }
    }
}
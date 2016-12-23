using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace TalentAgileShop.Model
{
    public static class RepositoryHelpers
    {


        public static List<Tuple<Product, int>> GetCartProducts(this IDataContext context, Cart cart)
        {

            var products = new List<Tuple<Product, int>>();

            foreach (var productInfo in cart.Products)
            {
                var product = context.Products.Include(p => p.Category)
                    .Include(p => p.Origin)
                    .FirstOrDefault(p => p.Id == productInfo.Id);

                if (product == null)
                {
                    continue;
                }

                products.Add(Tuple.Create(product, productInfo.Count));

            }

            return products;
            
        }




    }
}
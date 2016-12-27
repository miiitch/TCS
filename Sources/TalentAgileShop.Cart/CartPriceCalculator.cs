using System;
using System.Collections.Generic;
using System.Linq;
using TalentAgileShop.Model;

namespace Repo
{
    public class CartPriceCalculator: ICartPriceCalculator
    {
   

        public CartPrice ComputePrice(List<Tuple<Product, int>> products, string discountCode)
        {
            var result = new CartPrice();

            result.Products = products.Sum(p => p.Item1.Price * p.Item2);
            result.Delivery = products.Sum(p => p.Item2) * 10;
            if (discountCode != null)
            {
                result.InvalidDiscountCode = discountCode != "TEST";
            }
            
            return result;
        }
    }
}
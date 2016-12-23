using System;
using System.Collections.Generic;
using System.Linq;
using TalentAgileShop.Model;

namespace Repo
{
    public class CartPriceCalculator: ICartPriceCalculator
    {
   

        public CartPrice ComputePrice(List<Tuple<Product, int>> products)
        {
            var result = new CartPrice();

            result.ProductTotal = products.Sum(p => p.Item1.Price * p.Item2);


            return result;
        }
    }
}
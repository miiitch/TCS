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
            // Write cart price algorithm here
            return CartPrice();


        }
    }
}
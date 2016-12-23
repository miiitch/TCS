using System;
using System.Collections.Generic;

namespace TalentAgileShop.Model
{
    public interface ICartPriceCalculator
    {
        CartPrice ComputePrice(List<Tuple<Product, int>> products);
    }
}
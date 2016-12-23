using System;
using System.Collections.Generic;
using TalentAgileShop.Model;

namespace TalentAgileShop.Web.Models
{
    public class CartViewModel
    {
        public List<Tuple<Product, int>> Products { get; }
        public CartPrice Price { get; }




        public CartViewModel(List<Tuple<Product, int>> products, CartPrice price)
        {
            Products = products;
            Price = price;
        }
    }
}
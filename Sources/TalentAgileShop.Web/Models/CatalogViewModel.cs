using System.Collections.Generic;
using TalentAgileShop.Model;

namespace TalentAgileShop.Web.Models
{
    public class CatalogViewModel
    {
        public List<Product> Products { get; }

        public CatalogViewModel(List<Product> products)
        {
            Products = products;
        }
    }
}
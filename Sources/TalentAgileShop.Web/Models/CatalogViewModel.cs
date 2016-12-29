using System.Collections.Generic;
using System.Web.Mvc;
using TalentAgileShop.Model;

namespace TalentAgileShop.Web.Models
{
    public class CatalogViewModel
    {
        public List<Product> Products { get; }

        public ViewType CurrentViewType { get; set; }

        public bool AllowThumbnailView { get; set; }

        public CatalogViewModel(List<Product> products)
        {
            Products = products;
        }


        public enum ViewType
        {
            List,
            Thumbnail
        }
    }
}
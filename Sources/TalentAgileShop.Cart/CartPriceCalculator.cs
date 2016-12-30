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



            result.Products = 0;
            
            result.Delivery = 0;

            foreach (var product in products)
            {

               


                if (discountCode == "5BIG" &&
                    (product.Item1.Size == ProductSize.Large || product.Item1.Size == ProductSize.ExtraLarge))
                {
                    result.Products += product.Item1.Price * 0.95m * product.Item2;
                }
                else
                {
                    result.Products += product.Item1.Price * product.Item2;
                }

                switch (product.Item1.Size)
                {
                    case ProductSize.Small:

                        if (discountCode == "FREESMALL")
                        {
                            break;
                        }
                        result.Delivery += 5*product.Item2;
                        break;
                    case ProductSize.Medium:
                        if (discountCode == "FREESMALL")
                        {
                            break;
                        }
                        result.Delivery += 5 * product.Item2;
                        break;
                    case ProductSize.Large:
                        result.Delivery += 10 * product.Item2;
                        break;
                    case ProductSize.ExtraLarge:
                        result.Delivery += 20 * product.Item2;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            result.Delivery = Math.Min(50, result.Delivery);
            if (discountCode != null)
            {
                result.InvalidDiscountCode = discountCode != "TEST";
            }
            
            return result;
        }
    }
}
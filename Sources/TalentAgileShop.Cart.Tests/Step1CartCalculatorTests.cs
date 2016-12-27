using System;
using System.Collections.Generic;
using NFluent;
using NUnit.Framework;
using Repo;
using TalentAgileShop.Model;

namespace TalentAgileShop.Cart.Tests
{
    [TestFixture]
    public class Step1CartCalculatorTests
    {
        private CartPriceCalculator _calculator;


        [SetUp]
        public void Setup()
        {
            _calculator = new CartPriceCalculator();
        }


    



        [Test]
        public void Empty_Cart_Price_Is_Zero()
        {
            var products = new List<Tuple<Product, int>>();


            var actualPrice = _calculator.ComputePrice(products, null);

            Check.That(actualPrice.Delivery).IsEqualTo(0);
            Check.That(actualPrice.Products).IsEqualTo(0);
        }


        [Test]
        [TestCase(100,1)]
        [TestCase(200, 2)]
        [TestCase(300, 3)]
        [TestCase(400, 4)]
        public void Cart_With_N_Product_Price_Is_Sum_Of_Prices(decimal productPrice,int count)
        {
            var products = new List<Tuple<Product, int>>();
           
            products.AddProduct(productPrice, count);


            var price = _calculator.ComputePrice(products, null);

            Check.That(price.Products).IsEqualTo(count * productPrice);

        }
        [Test]
        [TestCase(ProductSize.Small,1,5)]
        [TestCase(ProductSize.Medium,1, 5)]
        [TestCase(ProductSize.Large,1,10)]
        [TestCase(ProductSize.ExtraLarge,1, 20)]
        [TestCase(ProductSize.Small, 2, 10)]
        [TestCase(ProductSize.Medium, 2, 10)]
        [TestCase(ProductSize.Large, 2, 20)]
        [TestCase(ProductSize.ExtraLarge, 2, 40)]
        public void Cart_Delivery_Price(ProductSize s,int count, decimal delivery)
        {
            var products = new List<Tuple<Product, int>>();

            products.AddProduct(0, count,s);

            var price = _calculator.ComputePrice(products, null);


            Check.That(price.Delivery).IsEqualTo(delivery);
        }

       

        [Test]
        public void Max_Delivery_Price_Is_50()
        {
            var products = new List<Tuple<Product, int>>();

            products.AddProduct(0, 5, ProductSize.ExtraLarge);

            var price = _calculator.ComputePrice(products, null);


            Check.That(price.Delivery).IsEqualTo(50);
        }

        [Test]
        public void FREESMALL_Discount()
        {
            var products = new List<Tuple<Product, int>>();

            products.AddProduct(0, 1, ProductSize.Small);
            products.AddProduct(0, 1, ProductSize.Medium);


            var price = _calculator.ComputePrice(products, "FREESMALL");

            Check.That(price.Delivery).IsEqualTo(0);
        }

        [Test]
        public void _5BIG_Discount_On_Large()
        {
            var products = new List<Tuple<Product, int>>();

            products.AddProduct(100, 1, ProductSize.Large);


            var price = _calculator.ComputePrice(products, "5BIG");

            Check.That(price.Products).IsEqualTo(95);
        }

        [Test]
        public void _5BIG_Discount_On_XLarge()
        {
            var products = new List<Tuple<Product, int>>();

            products.AddProduct(100, 1, ProductSize.ExtraLarge);


            var price = _calculator.ComputePrice(products, "5BIG");

            Check.That(price.Products).IsEqualTo(95);
        }

    }

    public static class CartHelper
    {
        public static void AddProduct(this List<Tuple<Product, int>> products, decimal price, int count = 1, ProductSize size = ProductSize.Small, string category = "No category", string country = "Nowhere")
        {
            var p = new Product()
            {
                Category = new Category() { Name = category },
                Description = null,
                Id = Guid.NewGuid().ToString(),
                Price = price,
                Image = null,
                Name = "No name",
                Origin = new Country() { Name = country },
                Size = size
            };
            products.Add(Tuple.Create(p, count));
        }
    }
}
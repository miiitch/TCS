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


    


        // 1. remove the Ignore attribute and write the algorithm 
        [Test]
        [Ignore("LAB1")]
        public void Empty_Cart_Price_Is_Zero()
        {
            var products = new List<Tuple<Product, int>>();


            var actualPrice = _calculator.ComputePrice(products, null);

            Check.That(actualPrice.Delivery).IsEqualTo(0);
            Check.That(actualPrice.Products).IsEqualTo(0);
        }


        // 2. remove the Ignore attribute and write the algorithm 
        [Test]
        [Ignore("LAB1")]
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
        [Ignore("LAB1")]
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


        // 3. remove the Ignore attribute and write the algorithm 
        [Test]
        [Ignore("LAB1")]
        public void Max_Delivery_Price_Is_50()
        {
            var products = new List<Tuple<Product, int>>();

            products.AddProduct(0, 5, ProductSize.ExtraLarge);

            var price = _calculator.ComputePrice(products, null);


            Check.That(price.Delivery).IsEqualTo(50);
        }

        // 4. Write a test for FREESMALL Discount first and then write the algorithm 
        [Test]
        [Ignore("LAB1")]
        public void FREESMALL_Discount()
        {
          // 
        }

      
        // 5. Write  test(s) for 5BIG Discount first and the write the algorithm
        

    }
}
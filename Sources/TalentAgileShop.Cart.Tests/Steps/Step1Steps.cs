﻿using System;
using System.Collections.Generic;
using NFluent;
using Repo;
using TalentAgileShop.Model;
using TechTalk.SpecFlow;

namespace TalentAgileShop.Cart.Tests.Steps
{
    [Binding]
    public class Step1Steps
    {
        [Given("a cart")]
        public void GivenACart()
        {
            ScenarioContext.Current["Cart"] = new List<Tuple<Product, int>>();
            ScenarioContext.Current["Code"] = null;
        }


        [Given(@"(.*) (.*) product with a price of (.*)")]
        public void GivenSmallProductWithAPriceOf(int count, ProductSize size,  decimal price)
        {
            var products = ScenarioContext.Current["Cart"] as List<Tuple<Product, int>>;

            products.AddProduct(price,count,size);
        }

        [Given(@"the discountCode (.*)")]
        public void GivenSmallProductWithAPriceOf(string discountCode)
        {
            ScenarioContext.Current["Code"] = discountCode;
        }

        [When(@"I calculate the total price")]
        public void WhenICalculateTheTotalPrice()
        {
            var products = ScenarioContext.Current["Cart"] as List<Tuple<Product, int>>;
            var discountCode = ScenarioContext.Current["Code"] as string;

            var calculator = new CartPriceCalculator();

            var price = calculator.ComputePrice(products, discountCode);

            ScenarioContext.Current["Price"] = price;

        }

        [Then(@"the product price should be (.*)")]
        public void ThenTheProductPriceShouldBe(decimal productsPrice)
        {
            var price = ScenarioContext.Current["Price"] as CartPrice;

            Check.That(price.Products).IsEqualTo(productsPrice);
        }

        [Then(@"the delivery price should be (.*)")]
        public void ThenTheDeliveryPriceShouldBe(decimal deliveryPrice)
        {
            var price = ScenarioContext.Current["Price"] as CartPrice;

            Check.That(price.Delivery).IsEqualTo(deliveryPrice);
        }
    }
}

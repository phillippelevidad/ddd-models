using Core;
using FluentAssertions;
using ShoppingCart.Model;
using System;
using System.Linq;
using Xunit;

namespace PlaygroundTests
{
    public class CartAssertions
    {
        private const string skuChewingGum = "6af4d241";
        private const string skuAssortedCandies = "9309d7b3";

        [Fact]
        public void CanStartNewCartForCustomer()
        {
            var customerId = new EntityId();
            var cart = new Cart(customerId);

            cart.Should().NotBeNull();
        }

        [Fact]
        public void CanAddItemToCart()
        {
            var customerId = new EntityId();
            var cart = new Cart(customerId);

            cart.Add(Product.Of(skuChewingGum, 10), Quantity.Of(1));

            cart.Items.Count.Should().Be(1);
        }

        [Fact]
        public void WhenProductThatExistsInCartIsAddedAgainShouldIncreaseQuantity()
        {
            var customerId = new EntityId();
            var cart = new Cart(customerId);

            cart.Add(Product.Of(skuChewingGum, 10), Quantity.Of(1));
            cart.Add(Product.Of(skuChewingGum, 10), Quantity.Of(1));

            cart.Items.Count.Should().Be(1);
            cart.Items.First().Quantity.Should().Be(Quantity.Of(2));
        }

        [Fact]
        public void CannotAddMoreThen10UnitsOfEachProduct()
        {
            var customerId = new EntityId();
            var cart = new Cart(customerId);

            cart.Add(Product.Of(skuChewingGum, 10), Quantity.Of(8));

            Action violation1 = () => cart.Add(Product.Of(skuChewingGum, 10), Quantity.Of(3));
            Action violation2 = () => cart.Add(Product.Of(skuAssortedCandies, 10), Quantity.Of(15));

            violation1.Should().Throw<Exception>();
            violation2.Should().Throw<Exception>();
        }

        [Fact]
        public void CartTotalIsCorrectlyCalculated()
        {
            var customerId = new EntityId();
            var cart = new Cart(customerId);

            cart.Add(Product.Of(skuChewingGum, 10), Quantity.Of(1));
            cart.Add(Product.Of(skuAssortedCandies, 5), Quantity.Of(2));

            cart.TotalAmount.Should().Be(20);
        }

        [Fact]
        public void CanRemoveItem()
        {
            var customerId = new EntityId();
            var cart = new Cart(customerId);
            var item = Product.Of(skuChewingGum, 10);

            cart.Add(item, Quantity.Of(1));
            cart.Remove(item);

            cart.Items.Count.Should().Be(0);
        }

        [Fact]
        public void CannotCloseCartUnder50Dollars()
        {
            var customerId = new EntityId();
            var cart = new Cart(customerId);
            var item = Product.Of(skuChewingGum, 10);

            cart.Add(item, Quantity.Of(1));
            Action violation = () => cart.CloseForCheckout();

            violation.Should().Throw<Exception>();
        }

        [Fact]
        public void CanCloseCartAbove50Dollars()
        {
            var customerId = new EntityId();
            var cart = new Cart(customerId);
            var item = Product.Of(skuChewingGum, 30);

            cart.Add(item, Quantity.Of(2));
            cart.CloseForCheckout();

            cart.DomainEvents.Should().ContainItemsAssignableTo<CartClosedForCheckout>();
        }
    }
}

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
        [Fact]
        public void CreateCartSucceeds()
        {
            var customerId = new EntityId();
            var cart = new Cart(customerId);

            cart.Should().NotBeNull();
        }

        [Fact]
        public void AddItemSucceeds()
        {
            var customerId = new EntityId();
            var cart = new Cart(customerId);

            cart.Add(Product.Of("6af4d241", 10), Quantity.Of(1));

            cart.Items.Count.Should().Be(1);
        }

        [Fact]
        public void AddExistingItemShouldIncreaseQuantity()
        {
            var customerId = new EntityId();
            var cart = new Cart(customerId);

            cart.Add(Product.Of("6af4d241", 10), Quantity.Of(1));
            cart.Add(Product.Of("6af4d241", 10), Quantity.Of(1));

            cart.Items.Count.Should().Be(1);
            cart.Items.First().Quantity.Should().Be(Quantity.Of(2));
        }

        [Fact]
        public void CannotAddMoreThen10UnitsOfEachProduct()
        {
            var customerId = new EntityId();
            var cart = new Cart(customerId);

            cart.Add(Product.Of("6af4d241", 10), Quantity.Of(8));

            Action violation1 = () => cart.Add(Product.Of("6af4d241", 10), Quantity.Of(3));
            Action violation2 = () => cart.Add(Product.Of("9309d7b3", 10), Quantity.Of(15));

            violation1.Should().Throw<Exception>();
            violation2.Should().Throw<Exception>();
        }

        [Fact]
        public void CartTotalIsCorrectlyCalculated()
        {
            var customerId = new EntityId();
            var cart = new Cart(customerId);

            cart.Add(Product.Of("6af4d241", 10), Quantity.Of(1));
            cart.Add(Product.Of("9309d7b3", 5), Quantity.Of(2));

            cart.TotalAmount.Should().Be(20);
        }

        [Fact]
        public void RemoveItemSucceeds()
        {
            var customerId = new EntityId();
            var cart = new Cart(customerId);
            var item = Product.Of("6af4d241", 10);

            cart.Add(item, Quantity.Of(1));
            cart.Remove(item);

            cart.Items.Count.Should().Be(0);
        }
    }
}

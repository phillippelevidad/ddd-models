﻿using Core;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart.Model
{
    public class Cart : AggregateRoot
    {
        private const int maxQuantityPerProduct = 10;
        private const decimal minCartAmountForCheckout = 50m;

        private readonly List<CartItem> items = new List<CartItem>();

        public Cart(EntityId customerId) : base(customerId)
        {
            CustomerId = customerId;
            IsClosed = false;
        }

        public EntityId CustomerId { get; }
        public bool IsClosed { get; private set; }

        public IReadOnlyList<CartItem> Items => items;
        public decimal TotalAmount => items.Sum(item => item.TotalAmount);

        public Result CanAdd(Product product, Quantity quantity)
        {
            var newQuantity = quantity;

            var existing = items.SingleOrDefault(item => item.Product == product);
            if (existing != null)
                newQuantity += existing.Quantity;

            if (newQuantity > maxQuantityPerProduct)
                return Result.Fail("Cannot add more than 10 units of each product.");

            return Result.Ok();
        }

        public void Add(Product product, Quantity quantity)
        {
            CanAdd(product, quantity)
                .OnFailure(error => throw new Exception(error));

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Product == product)
                {
                    items[i] = items[i].Add(quantity);
                    return;
                }
            }

            items.Add(new CartItem(product, quantity));
        }

        public void Remove(Product product)
        {
            var existing = items.SingleOrDefault(item => item.Product == product);

            if (existing != null)
                items.Remove(existing);
        }

        public void Remove(Product product, Quantity quantity)
        {
            var existing = items.SingleOrDefault(item => item.Product == product);

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Product == product)
                {
                    items[i] = items[i].Remove(quantity);
                    return;
                }
            }

            if (existing != null)
                existing = existing.Remove(quantity);
        }

        public Result CanCloseForCheckout()
        {
            if (IsClosed)
                return Result.Fail("The cart is already closed.");

            if (TotalAmount < minCartAmountForCheckout)
                return Result.Fail("The total amount should be at least 50 dollars in order to proceed to checkout.");

            return Result.Ok();
        }

        public void CloseForCheckout()
        {
            CanCloseForCheckout()
                .OnFailure(error => throw new Exception(error));

            IsClosed = true;
            AddDomainEvent(new CartClosedForCheckout(this));
        }

        public override string ToString()
        {
            return $"{CustomerId}, Items {items.Count}, Total {TotalAmount}";
        }
    }
}

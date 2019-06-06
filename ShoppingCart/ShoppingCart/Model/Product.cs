using CSharpFunctionalExtensions;
using System;

namespace ShoppingCart.Model
{
    public class Product : ValueObject<Product>
    {
        private Product(string sku, decimal unitPrice)
        {
            Sku = sku;
            UnitPrice = unitPrice;
        }

        public string Sku { get; }
        public decimal UnitPrice { get; }

        public static Result<Product> Create(string sku, decimal unitPrice)
        {
            if (string.IsNullOrEmpty(sku))
                return Result.Fail<Product>("Cannot create a product from an empty SKU name.");

            if (unitPrice <= 0)
                return Result.Fail<Product>("A product cannot be free or have a negative price.");

            return Result.Ok(new Product(sku, unitPrice));
        }

        public static Product Of(string sku, decimal unitPrice)
        {
            return Create(sku, unitPrice)
                .OnFailure(error => throw new Exception(error))
                .Result.Value;
        }

        public override string ToString()
        {
            return $"{Sku}, UnitPrice {UnitPrice}";
        }

        protected override bool EqualsCore(Product other)
        {
            return Sku == other.Sku &&
                UnitPrice == other.UnitPrice;
        }

        protected override int GetHashCodeCore()
        {
            return Sku.GetHashCode() ^ UnitPrice.GetHashCode();
        }
    }
}

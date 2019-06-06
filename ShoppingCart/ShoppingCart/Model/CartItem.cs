using CSharpFunctionalExtensions;

namespace ShoppingCart.Model
{
    public class CartItem : ValueObject<CartItem>
    {
        internal CartItem(Product product, Quantity quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public Product Product { get; }
        public Quantity Quantity { get; }
        public decimal TotalAmount => Product.UnitPrice * Quantity;

        public CartItem Add(Quantity quantity)
        {
            return new CartItem(Product, Quantity + quantity); 
        }

        public CartItem Remove(Quantity quantity)
        {
            return new CartItem(Product, Quantity - quantity);
        }

        public override string ToString()
        {
            return $"{Product}, Quantity {Quantity}";
        }

        protected override bool EqualsCore(CartItem other)
        {
            return Product == other.Product && Quantity == other.Quantity;
        }

        protected override int GetHashCodeCore()
        {
            return Product.GetHashCode() ^ Quantity.GetHashCode();
        }
    }
}

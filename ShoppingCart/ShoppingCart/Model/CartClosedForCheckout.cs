using Core;

namespace ShoppingCart.Model
{
    public class CartClosedForCheckout : IDomainEvent
    {
        public CartClosedForCheckout(Cart cart)
        {
            Cart = cart;
        }

        public Cart Cart { get; }
    }
}
# Domain: Shopping Cart

The Shopping Cart domain is a very simple one and the first in the series. It is just about managing the addition and removal of products to a cart after browsing the catalog and before proceeding to checkout.

## Use-Case

- The user adds products to a cart from the product catalog;
- The user has a list of the products in cart and sees the total amount;
- The user can't add more than 10 units of each product to the cart;
- The user can only proceed to checkout if they have at least 50 USD of products in the cart.

## Implementation Details

From the business rules above, given by the business people, the model tries its best to capture the language into the code and the implementation highlights are:

- The cart is represented as an Aggregate Root, managing all of the operations a customer can do with the cart and being in charge of ensuring the invariants are satisfied at all times. It is not possible for the model to reside in an invalid state;
- The cart deals with Product objects for Add and Remove operations, and then converts a Product to a CartItem, which adds the Quantity property. This is important because the customer does not add a CartItem to the cart, they add a Product and only then it becomes an item in the cart;
- The rule states that the customer can't **add** more than 10 units of each product to the cart, so this invariant is protected in the Add operation itself. If an attempt to add the 11th unit of a product is made, the customer is stopped right there because they are not allowed to do so;
- The checkout process is part of another Bounded Context, not captured in this model. So, the way to initiate the checkout process would be through integration events. This integration is started by the CartClosedForCheckout domain event that is added by the CloseForCheckout method in the Cart entity, but only if a minimum of 50 USD is reached. Some upper layer would pick this event up and transform it into an Integration Event, which in turn would be propagated to the right system by some BUS.

# Domain: Shopping Cart

The Shopping Cart domain is a very simple one and the first in the series. It is just about managing the addition and removal of products to a cart after browsing the catalog and before proceeding to checkout.

## Use Case

- The user browses the product catalog and chooses to add a product to the cart;
- The user can add other products to the cart;
- The user can add more of the same product to the cart;
- The user can remove products from the cart;
- The user cannot add more than 10 units of each product.

## Implementation Details

- `Cart` is the Aggregate Root and manages all of the operations a customer can do with a cart;
- `Product` is a Value Object representing the product that would come from a Catalog Bounded Context. It ensures a Product with an SKU and a valid price (not negative, not free);
- `Quantity` is a Value Object that represents an integer quantity value that must always be zero or more (in a cart, one cannot have -1 units of a product);
- `CartItem` is a Value Object that represents a product that was added to cart. It holds a reference to `Product` and `Quantity`.

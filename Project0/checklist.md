# functionality

- [X] place orders to store locations for customers
- [X] add a new customer
- [X] search customers by name
- [X] display details of an order
- [X] display all order history of a store location
- [X] display all order history of a customer
- [X] input validation
- [X] exception handling
- [X] persistent data; no hardcoding of data.(prices, customers, order history, etc.)
- [X] (optional: order history can be sorted by earliest, latest, cheapest, most expensive)
- [ ] (optional: get a suggested order for a customer based on his order history)
- [ ] (optional: display some statistics based on order history)
- [ ] (optional: deserialize data from disk) [optional]
- [ ] (optional: serialize data to disk) [optional]

# design

- [X] use EF Core (either database-first approach or code-first approach)
- [X] use a DB in third normal form
- [ ] don't use public fields
- [X] define and use at least one interface

# core / domain / business logic

- [X] class library
- [X] contains all business logic
- [X] contains domain classes (customer, order, store, product, etc.)
- [ ] documentation with <summary> XML comments on all public types and members (optional: <params> and <return>)
- [ ] (recommended: has no dependency on UI, data access, or any input/output considerations)

# customer

- [X] has first name, last name, etc.
- [X] (optional: has a default store location to order from)

# order

- [X] has a store location
- [X] has a customer
- [X] has an order time (when the order was placed)
- [X] can contain multiple kinds of product in the same order
- [ ] rejects orders with unreasonably high product quantities
- [ ] (optional: some additional business rules, like special deals)

# location

- [X] has an inventory
- [X] inventory decreases when orders are accepted
- [X] rejects orders that cannot be fulfilled with remaining inventory
- [ ] (optional: for at least one product, more than one inventory item decrements when ordering that product) product (etc.)

# user interface

- [X] interactive console application
- [X] has only display and input related code

# data access (recommended)

- [X] class library
- [X] recommended separate project for data access code using EF Core
- [ ] contains data access logic but no business logic
- [ ] use repository pattern for separation of concerns

# test

- [ ] at least 10 test methods
- [ ] focus on unit testing business logic; testing the console app is low priority because it will be replaced with the WebUI.

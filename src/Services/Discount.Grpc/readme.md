# Catalog Service

---

## Main Goals

* Maximum performance.
* Reduce response time as fast as possible.

## Use Case

* Add Item into Cart then Basket will consume this gRpc service to get the latest discount for each product inside cart.
* CRUD Discount: Work only in cart/basket deduct the total amount of the cart base on discount.
* CRUD Sale Event:
    * Work entire products. When a sale event is created it will affect entire products or some specific products and
      reduce those prices. The price will be reduced before it comes to front-end client.
    * It can be combined with coupons.

## Endpoint

<table><tbody><tr><td><strong>Method (gRPC)</strong></td><td><strong>Request URI</strong></td><td><strong>Use Cases</strong></td></tr><tr><td>GetDiscount</td><td>GetDiscountRequest</td><td>Get Discount</td></tr><tr><td>CreateDiscount</td><td>CreateDiscountRequest</td><td>Create Discount</td></tr><tr><td>UpdateDiscount</td><td>UpdateDiscountRequest</td><td>Update Discount</td></tr><tr><td>DeleteDiscount</td><td>DeleteDiscountRequest</td><td>Delete Discount</td></tr><tr><td>GetSaleEvent</td><td>GetSaleEventRequest</td><td>Get Sale Event</td></tr><tr><td>CreateSaleEvent</td><td>CreateSaleEventRequest</td><td>Create Sale Event</td></tr><tr><td>UpdateSaleEvent</td><td>UpdateSaleEventRequest</td><td>Update Sale Event</td></tr><tr><td>DeleteSaleEvent</td><td>DeleteSaleEventRequest</td><td>Delete Sale Event</td></tr></tbody></table>

## N-Layered Architecture

\- **N-tier architecture** is also called multi-tier/N-Layer architecture because the software is engineered to have the
processing, data management, and presentation functions physically and logically separated. That means that these
different functions are hosted on several machines or clusters, ensuring that services are provided without resources
being shared and, as such, these services are delivered at top capacity. The “N” in the name n-tier architecture refers
to any number from 1.

Not only does your software gain from being able to get services at the best possible rate, but it’s also easier to
manage. This is because when you work on one section, the changes you make will not affect the other functions. And if
there is a problem, you can easily pinpoint where it originates.

* When to use:
    * N-layer architecture, which is often confused with n-tier but is slightly different.
      N-layer architecture is a logical separation of code within the same physical tier/location. The key distinction
      from n-tier is that layers describe code organization while tiers describe physical separation. A typical n-layer
      architecture includes:
        * **_Presentation Layer_** : UI, API, ViewModels/DTOs, Input Validation
        * **_Business Layer_** : Business rules and logic, Validations, Exception
        * **_Data Access Layer_** : Database operations, Data mappings, CRUD operations, Repository implementations
        * **_Domain Layer_** : Entity Models, Enum, etc.....
    * Consider an N-Layer architecture for:
        * Simple web applications.
        * A good starting point when architectural requirements are cleared.
        * Well-organized code structure are required.
    * Benefits:
        * Organized codebase
        * Easier maintenance
        * Better code reusability
        * Simplified unit testing
        * Clear separation of concerns
    * Challenges:
        * Complexity Management:
            * Large number of classes/interfaces to maintain
            * Complex dependency management
            * Steeper learning curve for new developers
            * Can lead to "analysis paralysis" in layer design
        * Performance Overhead
        * Tight Coupling Issues
            * Changes in lower layers can affect upper layers
            * Risk of creating unnecessary dependencies
            * Difficulty in maintaining clean interfaces
            * Circular dependency problems
        * Higher maintenance cost, Difficulty in refactoring across layers, Version control complexity.
* Map to N-Layer
    * Models = Domain Layer
    * Data = DAL
    * Services = BLL
    * Protos = PL

## Design Pattern

* CQRS : Command Query Responsibility Segregation divide operation into commands (Write to DB) and queries (Read from
  DB)
* Mediator: Reduce chaotic dependencies between objects, reducing direct dependencies and simplifying communications.

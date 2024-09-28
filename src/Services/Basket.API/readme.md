# Basket Service

---

## Use Case

*   CRUD Operation
    *   Get cart data by UserId
    *   Add a new item to the cart.
    *   Update existing product quantity.
    *   Delete the whole cart.
*   gRPC Operation
    *   Get discount and deduct the discount from the item price
*   Checkout and Publish the event to RabbitMQ

## Endpoint

<table><tbody><tr><td>Method</td><td>URI</td><td>Use Case</td></tr><tr><td>GET</td><td>/cart?id={user-id}</td><td>Get Cart data by user id</td></tr><tr><td>POST</td><td>/cart</td><td>Add or Update Item inside the cart</td></tr><tr><td>DELETE</td><td>/cart?id={user-id}</td><td>Delete whole cart with user id</td></tr><tr><td>POST</td><td>/cart/checkout</td><td>Check out</td></tr></tbody></table>

## Design Pattern

*   CQRS : Command Query Responsibility Segregation divides operation into commands (Write to DB) and queries (Read from DB)
*   Mediator: Reduce chaotic dependencies between objects, reduce direct dependencies and simplify communications.
*   Proxy
*   Decorator
*   Cache-aside

## Minimal API

## Data Structure

Document databases store catalogs in JSON format. MongoDB is another viable option for this purpose.

**Marten** is a useful .NET library for storing data as JSON.
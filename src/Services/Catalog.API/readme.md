# Catalog Service

---

## Use Case

* Listing Product and Categories, able to search product
* CRUD Operation
  * Listing Products and Categories
  * Get Product by ProductId
  * Get Products by Category
  * Create new Product
  * Update existed product
  * Delete product
  * Sorting and Filtering product
  * Pagination

## Endpoint

<table><tbody><tr><td><strong>Method</strong></td><td><strong>URI</strong></td><td><strong>Use Case</strong></td></tr><tr><td>GET</td><td>/products</td><td>List all products</td></tr><tr><td>GET</td><td>/products/{id}</td><td>Get a product by Id</td></tr><tr><td>GET</td><td>/products/category</td><td>Get products by Category</td></tr><tr><td>POST</td><td>/products</td><td>Create a new product</td></tr><tr><td>PUT</td><td>/product/{id}</td><td>Update a product</td></tr><tr><td>DELETE</td><td>/product/{id}</td><td>Remove a product</td></tr></tbody></table>

## Vertical Slice Architecture

We have 2 options for Architecture. Vertical Slice (VS) and Clean.

_**VS**_ is good for projects that require individual features and highly-flexible while _**Clean**_ is suitable for long-term maintainability, sergeration part of applications and still good at software development principles.

Besides that _**VS**_ focuses on divide applications into single slices/distinct feature which includes all classes needed for a specific feature (Self-contained and independent). _**Clean**_ focus on separating parts of the application makes the application lose coupling and protects Business Logic from out-side changes like Database Change or UI changes.

Overall, Clean Architecture still best for almost projects but not mine cause Catalog API may not be extended in the future or extended some simple features so we going to use _**VS**_ aiming to organize features individually and easy to manage.

## Design Pattern

* CQRS : Command Query Responsibility Segregation divide operation into commands (Write to DB) and queries (Read from DB)
* Mediator: Reduce chaotic dependencies between objects, reducing direct dependencies and simplifying communications.

## FastEndPoint

FastEndpoints is a developer friendly alternative to Minimal APIs & MVC  
It nudges you towards the REPR Design Pattern (Request-Endpoint-Response) for convenient & maintainable endpoint creation with virtually no boilerplate.

Performance is on par with Minimal APIs. It's faster, uses less memory and does around 35k more requests per second than a MVC Controller in our benchmarks.

* ### [Head-To-Head Benchmark](https://fast-endpoints.com/benchmarks#head-to-head-benchmark)

<table><tbody><tr><td style="border:0px solid rgb(234, 234, 234);padding:0px 0.571429em 0.571429em;vertical-align:bottom;"><strong>Method</strong></td><td style="border:0px solid rgb(234, 234, 234);padding:0px 0.571429em 0.571429em;text-align:right;vertical-align:bottom;"><strong>Mean</strong></td><td><strong>Ratio</strong></td><td><strong>Gen0</strong></td><td><strong>Gen1</strong></td><td><strong>Allocated</strong></td><td><strong>Alloc-Ratio</strong></td></tr><tr><td>FastEndpoints</td><td style="border:0px solid rgb(234, 234, 234);padding:0.571429em;text-align:right;">40.32 μs</td><td style="border:0px solid rgb(234, 234, 234);padding:0.571429em;text-align:right;">1.00</td><td>2.0000</td><td>-</td><td style="border:0px solid rgb(234, 234, 234);padding:0.571429em;text-align:right;">16.71 KB</td><td>1.00</td></tr><tr><td>ASP NET 7 Minimal APIs</td><td style="border:0px solid rgb(234, 234, 234);padding:0.571429em;text-align:right;">44.07 μs</td><td>1.09</td><td>2.1000</td><td>-</td><td style="border:0px solid rgb(234, 234, 234);padding:0.571429em;text-align:right;">17.07 KB</td><td>1.02</td></tr><tr><td>FastEndpoints (CodeGen)</td><td style="border:0px solid rgb(234, 234, 234);padding:0.571429em;text-align:right;">44.67 μs</td><td>1.11</td><td>2.0000</td><td>-</td><td style="border:0px solid rgb(234, 234, 234);padding:0.571429em;text-align:right;">16.75 KB</td><td>1.00</td></tr><tr><td>FastEndpoints (Scoped Validator)</td><td style="border:0px solid rgb(234, 234, 234);padding:0.571429em;text-align:right;">57.83 μs</td><td>1.43</td><td>3.4000</td><td>0.1000</td><td style="border:0px solid rgb(234, 234, 234);padding:0.571429em;text-align:right;">28.08 KB</td><td>1.68</td></tr><tr><td>ASP NET 7 MVC Controller</td><td style="border:0px solid rgb(234, 234, 234);padding:0.571429em;text-align:right;">63.97 μs</td><td>1.59</td><td>2.8000</td><td>0.1000</td><td style="border:0px solid rgb(234, 234, 234);padding:0.571429em;text-align:right;">23.58 KB</td><td>1.41</td></tr></tbody></table>

_**Why choose FastEndPoint but not Minimal?**_ : The first thing MinimalAPI has faster at process but not about performance. FastEndpoints might be more suited for complex scenarios where additional features and structure are beneficial, while Minimal APIs are excellent for straightforward cases.

## Data Structure

Document Database by storing Catalog as JSON type. Also, we can use MongoDB as an alternative solution.

**Marten** is a `_library_` that transforms PostgreSQL into _.NET transactional Document DB_.
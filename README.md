# pizzabox

PizzaBox is a console-based pizza ordering application.

## architecture (REQUIRED)

- [solution] PizzaBox.sln
  - [project - console] PizzaBox.Client.csproj
  - [project - classlib] PizzaBox.Domain.csproj
    - [folder] Abstracts
    - [folder] Interfaces
    - [folder] Models
  - [project - classlib ] PizzaBox.Storing.csproj
    - [folder] Repositories
  - [project - xunit] PizzaBox.Testing.csproj
    - [folder] Tests

## requirements

The application is centered around the interaction of 4 main objects:

- Customer
- Order
- Pizza
- Store

### store

- ✔️ [required] there should exist at least 2 stores for a customer to choose from
- ✔️ [required] each store should be able to view any and [all ✔️] of their placed orders
- ✔️ [required] each store should be able to view any and all of their sales (weekly, monthly, quarterly)

### order

- ✔️ [required] each order must be able to modify its collection of pizzas
- ✔️ [required] each order must be able to compute its pricing
- ✔️ [required] each order must be limited to a total pricing of no more than $250
- ✔️ [required] each order must be limited to a collection of pizzas of no more than 50

### pizza

- ✔️ [required] each pizza must be able to have a crust
- ✔️ [required] each pizza must be able to have a size
- ✔️ [required] each pizza must be able to have toppings
- ✔️ [required] each pizza must be able to compute its pricing
- ✔️ [required] each pizza must have no less than 2 default toppings
- ✔️ [required] each pizza must have no more than 5 total toppings

### customer

- ✔️ [required] must be able to view its order history
- ✔️ [required] must be able to only order from 1 location in a 24-hour period with no reset
- ✔️ [required] must be able to only order once in a 2-hour period

## technologies

- .NET Core - C#
- .NET Core - EF + SQL
- .NET Core - xUnit

## timelines

- due on Mar-15 at 11p Central
- present on Mar-17 starting at 9.30a Central
- implement as many requirements as you can (don't push to get all done)
- should be an mvp status (able to order at least 1 pizza)

## as a customer

- ✔️ should be able to launch application
- ✔️ should be able to view all stores
- ✔️ should be able to select a store
- ✔️ should be to place an order
- ✔️ with either custom or preset pizzas
- ✔️ if custom
- ✔️ select crust, size and toppings
- ✔️ if preset
- ✔️ select pizza and its size
- ✔️ see a tally of my order
- ✔️ add or remove more pizzas
- ✔️ and checkout when complete with latest order
- ✔️ see my order history
- ✔️ make a new order

## store story

as a store, i should be able do this:

- ✔️ access the application
- ✔️ select options for order history, sales
- ✔️ if order history
- ✔️ select options for all store orders and orders associated to a user (filtering)
- ✔️ if sales
- ✔️ see pizza type, count, revenue by week or by month

> the goal is to try to complete as many reqs as you can in the time alloted. :)

### Repo specific section:

package commands needed for [Domain] project:

- dotnet tool install --global dotnet-ef --version 5.0.4

- dotnet add package Microsoft.EntityFrameworkCore.Tools --version 5.0.4
- dotnet add package Microsoft.EntityFrameworkCore.Design --version 5.0.4
- dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 5.0.4

package commands needed for [Testing] project:

- dotnet add package xUnit

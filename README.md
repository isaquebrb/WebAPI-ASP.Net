# WebAPI-ASP.Net

## Introduction
This is a simple project of a `RESTful` service that was built using `ASP .Net Web API.` The project presents products and it's categories in an `Json`. You can use `Postman` to populate the datebase (change the connection string), by sending requisitions of verbs like `POST`.

## Requiriments:

- Visual Studio
- Entity Framework
- SQL Server
- Postman

## Sending Request

| Verbs | URL  | Description |
| ----- | ---- | ----------- |
| GET | /products/ | Show all products (include categories)
| GET | /categories/ | Show all categories
| GET | /categories/{categoryId}/products | Show a product by a category
| POST | /products | Inset a product
| DELETE | /products?productId=x | Delete a product
| PUT | /products | Update a product

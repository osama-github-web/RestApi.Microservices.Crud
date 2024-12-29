```markdown
# Product API

## Overview

The Product API is a microservice designed to manage products and their associated categories. It allows for operations such as adding, updating, retrieving, and deleting products, as well as managing product categories. This API communicates with the Category API using RabbitMQ for asynchronous processing.

## Features

- **Add Product**: Create a new product and associate it with a category.
- **Get Product List**: Retrieve all products, including their associated categories.
- **Get Product by ID**: Retrieve a specific product by its ID.
- **Update Product**: Update an existing product's details.
- **Delete Product**: Remove a product from the database.
- **Add Category**: Add a new category to the system.
- **Get Product Categories**: Retrieve all categories related to products.

## Technologies Used

- ASP.NET Core
- Entity Framework Core for database interactions
- JSON for data interchange

## Getting Started

### Prerequisites

- .NET SDK (version 6.0 or later)
- A SQL database (e.g., SQL Server, PostgreSQL) configured for the application

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/ProductApi.git
   cd ProductApi
   ```

2. Restore the dependencies:
   ```bash
   dotnet restore
   ```

3. Set up the database:
   - Update the connection string in `appsettings.json`.
   - Run migrations to create the database schema:
     ```bash
     dotnet ef database update
     ```

4. Start the RabbitMQ server (if not already running).

5. Run the application:
   ```bash
   dotnet run
   ```

### API Endpoints

#### Products

- **GET** `/api/Product/productlist` - Retrieve a list of all products.
- **GET** `/api/Product/getproductbyid?id={id}` - Retrieve a product by its ID.
- **POST** `/api/Product/addproduct` - Add a new product.
- **PUT** `/api/Product/updateproduct` - Update an existing product.
- **DELETE** `/api/Product/deleteproduct?id={id}` - Delete a product by its ID.

#### Categories

- **GET** `/api/Categories/GetAll` - Retrieve all categories.
- **POST** `/api/Categories/AddCategory` - Add a new category.
- **POST** `/api/Categories/AddProductCategory` - Associate a product with a category.


## Contributing

Contributions are welcome! Please feel free to submit a pull request or open an issue to discuss improvements or bugs.

## License

This project is licensed under the MIT License. See the LICENSE file for details.
```

### Instructions for Use

1. **Modify the GitHub URL**: Replace `https://github.com/yourusername/ProductApi.git` with the actual URL of your repository.
2. **Update Information**: Adjust any sections to reflect your specific project details, such as prerequisites, installation steps, and API endpoints.
3. **Add License**: If you have a specific license, make sure to include a corresponding LICENSE file in your repository.

Feel free to expand on any section to include more details about your project as necessary!

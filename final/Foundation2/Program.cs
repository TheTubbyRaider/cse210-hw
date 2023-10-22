using System;
using System.Collections.Generic;

class Address
{
    private string StreetAddress { get; set; }
    private string City { get; set; }
    private string StateProvince { get; set; }
    private string Country { get; set; }

    public Address(string streetAddress, string city, string stateProvince, string country)
    {
        StreetAddress = streetAddress;
        City = city;
        StateProvince = stateProvince;
        Country = country;
    }

    public bool IsInUSA()
    {
        return string.Equals(Country, "USA", StringComparison.OrdinalIgnoreCase);
    }

    public override string ToString()
    {
        return $"{StreetAddress}\n{City}, {StateProvince}\n{Country}";
    }
}

class Product
{
    public string Name { get; private set; }
    public string ProductId { get; private set; }
    public double Price { get; private set; }
    public int Quantity { get; private set; }

    public Product(string name, string productId, double price, int quantity)
    {
        Name = name;
        ProductId = productId;
        Price = price;
        Quantity = quantity;
    }

    public double GetTotalPrice()
    {
        return Price * Quantity;
    }
}

class Customer
{
    public string Name { get; private set; }
    public Address ShippingAddress { get; private set; }

    public Customer(string name, Address shippingAddress)
    {
        Name = name;
        ShippingAddress = shippingAddress;
    }

    public bool IsInUSA()
    {
        return ShippingAddress.IsInUSA();
    }
}

class Order
{
    private List<Product> Products { get; set; }
    private Customer Customer { get; set; }

    public Order(Customer customer, List<Product> products)
    {
        Customer = customer;
        Products = products;
    }

    public double CalculateTotalPrice()
    {
        double total = 0;
        foreach (Product product in Products)
        {
            total += product.GetTotalPrice();
        }

        if (Customer.IsInUSA())
        {
            total += 5.0; // USA shipping cost
        }
        else
        {
            total += 35.0; // International shipping cost
        }

        return total;
    }

    public string GetPackingLabel()
    {
        string label = "Packing Label:\n";
        foreach (Product product in Products)
        {
            label += $"{product.Name} (ID: {product.ProductId})\n";
        }
        return label;
    }

    public string GetShippingLabel()
    {
        string label = "Shipping Label:\n";
        label += Customer.ToString();
        return label;
    }
}

class Program
{
    static void Main()
    {
        // Create an address for a customer
        Address usaAddress = new Address("123 Main St", "Los Angeles", "CA", "USA");

        // Create a customer
        Customer usaCustomer = new Customer("John Doe", usaAddress);

        // Create a list of products for the first order
        Product product1 = new Product("Laptop", "12345", 999.99, 2);
        Product product2 = new Product("Phone", "67890", 599.99, 3);

        List<Product> order1Products = new List<Product> { product1, product2 };

        // Create the first order
        Order order1 = new Order(usaCustomer, order1Products);

        // Create a list of products for the second order
        Product product3 = new Product("Tablet", "54321", 299.99, 1);
        Product product4 = new Product("Camera", "24680", 699.99, 2);

        List<Product> order2Products = new List<Product> { product3, product4 };

        // Create the second order
        Order order2 = new Order(usaCustomer, order2Products);

        // Display the information for the first order
        Console.WriteLine("Order 1:");
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine("Total Price: $" + order1.CalculateTotalPrice());

        // Display the information for the second order
        Console.WriteLine("\nOrder 2:");
        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine("Total Price: $" + order2.CalculateTotalPrice());
    }
}


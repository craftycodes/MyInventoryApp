// Models/Product.cs
public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }

    public Product(int productId, string name, string description, decimal price, int stockQuantity)
    {
        ProductId = productId;
        Name = name;
        Description = description;
        Price = price;
        StockQuantity = stockQuantity;
    }

    public void UpdateStock(int quantity)
    {
        StockQuantity -= quantity;
    }
}


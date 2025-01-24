// Models/Order.cs
public class Order
{
    public int OrderId { get; set; }
    public List<Product> Products { get; set; }
    public decimal TotalAmount { get; set; }

    public Order(int orderId)
    {
        OrderId = orderId;
        Products = new List<Product>();
    }

    public void AddProduct(Product product)
    {
        Products.Add(product);
        TotalAmount += product.Price;
    }
}


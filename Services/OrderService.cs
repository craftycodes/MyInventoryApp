using System.Text.Json;
using MyInventoryApp.Models;


namespace MyInventoryApp.Services
{
    public class OrderService
    {
        private Inventory _inventory;
        private const string OrdersFilePath = "orders.json"; // The path to save/load orders
        private List<Order> _orders = new List<Order>();

        public OrderService(Inventory inventory)
        {
            _inventory = inventory;
            LoadOrders(); // Load orders from file on startup
        }

        // Place an order and add it to the orders list
        public bool PlaceOrder(int productId, int quantity)
        {
            Product product = _inventory.GetProductById(productId);
            if (product != null && product.StockQuantity >= quantity)
            {
                product.UpdateStock(quantity);
                Order order = new Order(_orders.Count + 1);
                order.AddProduct(product);
                _orders.Add(order);
                SaveOrders(); // Save orders after placing an order
                Console.WriteLine($"Order placed successfully for {quantity} {product.Name}(s).");
                return true;
            }
            else
            {
                Console.WriteLine($"Not enough stock for {product?.Name ?? "Product"}.");
                return false;
            }
        }

        // Save orders to JSON file
        public void SaveOrders()
        {
            var json = JsonSerializer.Serialize(_orders);
            File.WriteAllText(OrdersFilePath, json);
        }

        // Load orders from JSON file
        public void LoadOrders()
        {
            if (File.Exists(OrdersFilePath))
            {
                var json = File.ReadAllText(OrdersFilePath);
                _orders = JsonSerializer.Deserialize<List<Order>>(json) ?? new List<Order>();
            }
        }

        // Optionally, return the list of orders for display
        public List<Order> GetOrders() => _orders;
    }
}



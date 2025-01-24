using System.Text.Json;

namespace MyInventoryApp.Models
{
    public class Inventory
    {
        private List<Product> _products = new List<Product>();
        private const string InventoryFilePath = "inventory.json"; // The path to save/load inventory

        public void AddProduct(Product product)
        {
            _products.Add(product);
        }

        public void ViewInventory()
        {
            foreach (var product in _products)
            {
                Console.WriteLine($"{product.Name} - ${product.Price} - {product.StockQuantity} in stock");
            }
        }

        public Product? GetProductById(int productId)
        {
            return _products.FirstOrDefault(p => p.ProductId == productId);
        }

        // Save inventory to JSON file
        public void SaveInventory()
        {
            var json = JsonSerializer.Serialize(_products);
            File.WriteAllText(InventoryFilePath, json);
        }

        // Load inventory from JSON file
        public void LoadInventory()
        {
            if (File.Exists(InventoryFilePath))
            {
                var json = File.ReadAllText(InventoryFilePath);
                _products = JsonSerializer.Deserialize<List<Product>>(json) ?? new List<Product>();
            }
        }
    }
}



using MyInventoryApp.Models;
using MyInventoryApp.Services;

class Program
{
    static void Main(string[] args)
    {
        // Create inventory and order service
        Inventory inventory = new Inventory();
        inventory.LoadInventory(); // Load inventory from file
        OrderService orderService = new OrderService(inventory);

        // Start the UI loop
        bool isRunning = true;
        while (isRunning)
        {
            Console.Clear();
            Console.WriteLine("Welcome to My Inventory System");
            Console.WriteLine("1. View Inventory");
            Console.WriteLine("2. Place Order");
            Console.WriteLine("3. View Orders");
            Console.WriteLine("4. Exit");
            Console.Write("Please choose an option (1-4): ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ViewInventory(inventory);
                    break;
                case "2":
                    PlaceOrder(inventory, orderService);
                    break;
                case "3":
                    ViewOrders(orderService);
                    break;
                case "4":
                    isRunning = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }

            if (isRunning)
            {
                Console.WriteLine("\nPress any key to return to the main menu...");
                Console.ReadKey();
            }
        }

        // Save inventory and orders when exiting
        inventory.SaveInventory(); // Save inventory to file
        orderService.SaveOrders(); // Save orders to file
        Console.WriteLine("Thank you for using the Inventory System!");
    }

    static void ViewInventory(Inventory inventory)
    {
        Console.Clear();
        Console.WriteLine("Inventory:");
        inventory.ViewInventory();
    }

    static void PlaceOrder(Inventory inventory, OrderService orderService)
    {
        Console.Clear();
        Console.WriteLine("Enter the product ID to order:");
        int productId;
        if (int.TryParse(Console.ReadLine(), out productId))
        {
            Console.WriteLine("Enter the quantity:");
            int quantity;
            if (int.TryParse(Console.ReadLine(), out quantity))
            {
                bool success = orderService.PlaceOrder(productId, quantity);
                if (success)
                {
                    Console.WriteLine("Order placed successfully!");
                }
                else
                {
                    Console.WriteLine("Failed to place order. Check stock and try again.");
                }
            }
            else
            {
                Console.WriteLine("Invalid quantity.");
            }
        }
        else
        {
            Console.WriteLine("Invalid product ID.");
        }
    }

    static void ViewOrders(OrderService orderService)
    {
        Console.Clear();
        Console.WriteLine("Orders:");
        var orders = orderService.GetOrders();
        foreach (var order in orders)
        {
            Console.WriteLine($"Order ID: {order.OrderId}, Total Amount: {order.TotalAmount}");
            foreach (var product in order.Products)
            {
                Console.WriteLine($"  {product.Name} - {product.Price} x {product.StockQuantity}");
            }
        }
    }
}


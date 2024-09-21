using Microsoft.Extensions.DependencyInjection;
using PetStore;
using PetStore.Logic;
using PetStore.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

var services = CreateServiceCollection();
var productLogic = services.GetService<IProductLogic>();

string? userInput = DisplayMenuAndGetInput();

while (userInput?.ToLower() != "exit")
{
    if (userInput == "1")
    {
        var dogLeash = new DogLeash();

        Console.WriteLine("Creating a dog leash...");

        Console.Write("Enter the material the leash is made out of: ");
        dogLeash.Material = Console.ReadLine() ?? "Unknown";  // Handle null input

        Console.Write("Enter the length in inches: ");
        if (int.TryParse(Console.ReadLine(), out int length))
        {
            dogLeash.LengthInches = length;
        }
        else
        {
            Console.WriteLine("Invalid input for length. Defaulting to 0.");
            dogLeash.LengthInches = 0;
        }

        Console.Write("Enter the name of the leash: ");
        dogLeash.Name = Console.ReadLine() ?? "Unknown";  // Handle null input

        Console.Write("Give the product a short description: ");
        dogLeash.Description = Console.ReadLine() ?? "No description";  // Handle null input

        Console.Write("Give the product a price: ");
        if (decimal.TryParse(Console.ReadLine(), out decimal price))
        {
            dogLeash.Price = price;
        }
        else
        {
            Console.WriteLine("Invalid input for price. Defaulting to 0.");
            dogLeash.Price = 0;
        }

        Console.Write("How many products do you have on hand? ");
        if (int.TryParse(Console.ReadLine(), out int quantity))
        {
            dogLeash.Quantity = quantity;
        }
        else
        {
            Console.WriteLine("Invalid input for quantity. Defaulting to 0.");
            dogLeash.Quantity = 0;
        }

        productLogic?.AddProduct(dogLeash);  // Null check
        Console.WriteLine("Added a dog leash");
    }
    else if (userInput == "2")
    {
        Console.Write("What is the name of the dog leash you would like to view? ");
        var dogLeashName = Console.ReadLine();
        var dogLeash = productLogic?.GetDogLeashByName(dogLeashName ?? "");  // Null check
        Console.WriteLine(JsonSerializer.Serialize(dogLeash));
        Console.WriteLine();
    }
    else if (userInput == "3")
    {
        Console.WriteLine("The following products are in stock: ");
        var inStock = productLogic?.GetOnlyInStockProducts();
        foreach (var item in inStock ?? new List<string>())
        {
            Console.WriteLine(item);
        }
        Console.WriteLine();
    }
    else if (userInput == "4")
    {
        Console.WriteLine($"The total price of inventory on hand is {productLogic?.GetTotalPriceOfInventory()}");
        Console.WriteLine();
    }

    userInput = DisplayMenuAndGetInput();
}

static string DisplayMenuAndGetInput()
{
    Console.WriteLine("Press 1 to add a product");
    Console.WriteLine("Press 2 to view a Dog Leash Product");
    Console.WriteLine("Press 3 to view in stock products");
    Console.WriteLine("Press 4 to view the total price of current inventory");
    Console.WriteLine("Type 'exit' to quit");
    
    return Console.ReadLine() ?? string.Empty;  // Return user input or an empty string
}

static IServiceProvider CreateServiceCollection()
{
    return new ServiceCollection()
        .AddTransient<IProductLogic, ProductLogic>()  // Register ProductLogic
        .BuildServiceProvider();
}

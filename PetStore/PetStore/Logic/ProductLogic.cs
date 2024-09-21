using System;
using System.Collections.Generic;
using System.Linq;
using PetStore.Models; // Ensure this is here to reference Product, DogLeash, and CatFood

namespace PetStore.Logic
{
    public class ProductLogic : IProductLogic
    {
        private readonly List<Product> _products;
        private readonly Dictionary<string, DogLeash> _dogLeash;
        private readonly Dictionary<string, CatFood> _catFood;

        public ProductLogic()
        {
            _products = new List<Product>
            {
                new DogLeash
                {
                    Description = "A rope dog leash made from strong rope.",
                    LengthInches = 60,
                    Material = "Rope",
                    Name = "Rope Dog Leash",
                    Price = 21.00m,
                    Quantity = 0
                },
                new DryCatFood
                {
                    Quantity = 6,
                    Price = 25.59m,
                    Name = "Plain 'Ol Cat Food",
                    Description = "Nothing fancy to find here.  Just the basic stuff your cat needs to live a healthy life",
                    WeightPounds = 10,
                    KittenFood = false
                },
                new CatFood
                {
                    Quantity = 48,
                    Price = 12.99m,
                    Name = "Fancy Cat Food",
                    Description = "Food that isn't only delicious, but made from the finest of all cat food stuff",
                    KittenFood = false
                }
            };

            _dogLeash = new Dictionary<string, DogLeash>();
            _catFood = new Dictionary<string, CatFood>();
        }

        public void AddProduct(Product product)
        {
            if (product is DogLeash leash && leash != null && leash.Name != null)
            {
                _dogLeash[leash.Name] = leash;
            }

            if (product is CatFood catFood && catFood != null && catFood.Name != null)
            {
                _catFood[catFood.Name] = catFood;
            }

            _products.Add(product);
        }

        public List<Product> GetAllProducts() => _products;

        public DogLeash? GetDogLeashByName(string name)
        {
            return _dogLeash.ContainsKey(name) ? _dogLeash[name] : null;
        }

        public List<string> GetOnlyInStockProducts()
        {
            return _products.Where(x => x.Quantity > 0).Select(x => x.Name ?? "Unknown Product").ToList();
        }

        public decimal GetTotalPriceOfInventory()
        {
            return _products.Where(x => x.Quantity > 0).Sum(x => x.Price);
        }
    }
}

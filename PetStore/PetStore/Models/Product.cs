using System;

namespace PetStore.Models
{
    public class Product
    {
        public decimal Price { get; set; }

        public string? Name { get; set; }  // Name can be nullable

        public int Quantity { get; set; }

        public string? Description { get; set; }  // Description can be nullable
    }
}

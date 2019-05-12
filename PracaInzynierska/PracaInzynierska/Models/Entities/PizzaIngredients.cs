using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PracaInzynierska.Models.Entities
{
    public class PizzaIngredients
    {
        [Key]
        public int IngredientID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string NameOfImage { get; set; }
        public decimal Price { get; set; }
    }
}

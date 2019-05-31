using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PracaInzynierska.Models.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Display(Name = "Kategoria")]
        [Required(ErrorMessage = "Podaj nazwę kategorii")]
        [StringLength(100)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Podaj opis kategorii")]
        [Display(Name = "Opis")]
        public string Description { get; set; }
        [Display(Name = "Obrazek")]
        public string NameOfImage { get; set; }
        [Display(Name = "Pokaż na głównej stronie")]
        public bool ShowProductsFromTheseCategoryInHomePage { get; set; }

        public ICollection<Product> Product { get; set; }
    }
}

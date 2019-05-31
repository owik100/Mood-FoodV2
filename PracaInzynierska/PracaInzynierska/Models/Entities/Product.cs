using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PracaInzynierska.Models.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "Podaj nazwę produktu")]
        [StringLength(50)]
        public string Name { get; set; }
        [Display(Name = "Opis")]
        [Required(ErrorMessage = "Podaj opis produktu")]
        public string Description { get; set; }
        [Display(Name = "Obrazek")]
        public string NameOfImage { get; set; }
        [Required(ErrorMessage = "Podaj cenę produktu")]
        [Display(Name = "Cena")]
        public decimal Price { get; set; }
        [Display(Name = "Produkt dnia")]
        public bool ProductOfTheDay { get; set; }
        [Display(Name = "Ukryty")]
        public bool Hidden { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}

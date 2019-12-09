using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PracaInzynierska.Models.Entities
{
    public class Restaurant
    {
        [HiddenInput(DisplayValue = false)]
        public int RestaurantId { get; set; }

        [Display(Name = "Miasto")]
        [Required(ErrorMessage = "Podaj miasto")]
        [StringLength(50)]
        public string City { get; set; }
        [Display(Name = "Ulica")]
        [Required(ErrorMessage = "Podaj ulicę")]
        [StringLength(50)]
        public string Street { get; set; }

        [Display(Name = "Numer lokalu")]
        [Required(ErrorMessage = "Podaj numer lokalu")]
        [StringLength(10)]
        public string Number { get; set; }

    }
}

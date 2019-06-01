using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PracaInzynierska.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Imię")]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Display(Name = "Nazwisko")]
        [StringLength(50)]
        public string LastName { get; set; }
        [Display(Name = "Miasto")]
        [StringLength(50)]
        public string City { get; set; }
        [Display(Name = "Ulica")]
        [StringLength(50)]
        public string Street { get; set; }
        [Display(Name = "Kod pocztowy")]
        [StringLength(6)]
        public string ZIPCode { get; set; }
        [Display(Name = "Numer domu/mieszkania")]
        [StringLength(10)]
        public string HouseNumber { get; set; }

    }
}

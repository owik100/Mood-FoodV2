﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PracaInzynierska.Models.Entities
{
    public class Order
    {
        [HiddenInput(DisplayValue = false)]
        public int OrderId { get; set; }
        [Display(Name ="Imię")]
        [Required(ErrorMessage = "Podaj imię")]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Display(Name = "Nazwisko")]
        [Required(ErrorMessage = "Podaj nazwisko")]
        [StringLength(50)]
        public string LastName { get; set; }
        [Display(Name = "Miasto")]
        [Required(ErrorMessage = "Podaj miasto")]
        [StringLength(50)]
        public string City { get; set; }
        [Display(Name = "Ulica")]
        [Required(ErrorMessage = "Podaj ulicę")]
        [StringLength(50)]
        public string Street { get; set; }
        [Display(Name = "Kod pocztowy")]
        [Required(ErrorMessage = "Podaj kod pocztowy")]
        [StringLength(6)]
        public string ZIPCode { get; set; }
        [Display(Name = "Numer domu/mieszkania")]
        [Required(ErrorMessage = "Podaj numer domu")]
        [StringLength(10)]
        public string HouseNumber { get; set; }
        [Display(Name = "Numer telefonu")]
        [Required(ErrorMessage = "Podaj numer telefonu")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Podaj adres email")]
        [EmailAddress(ErrorMessage = "Błędny format adresu email.")]
        public string Emial { get; set; }
        [Display(Name = "Dodatkowe informacje")]
        public string OptionalDescription { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public decimal OrderValue { get; set; }

        public List<OrderItem> OrderItem { get; set; }
    }

    public enum OrderStatus
    {
        New,
        Complited
    }
}

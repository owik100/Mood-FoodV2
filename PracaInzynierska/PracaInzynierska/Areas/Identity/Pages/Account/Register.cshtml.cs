using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using PracaInzynierska.Models.Entities;

namespace PracaInzynierska.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage ="Pole Email jest wymagane")]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Pole Hasło jest wymagane")]
            [StringLength(100, ErrorMessage = "{0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Hasło")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Powtórz Hasło")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Display(Name = "Imię")]
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
            [Required(ErrorMessage = "Podaj numer domu/mieszkania")]
            [StringLength(10)]
            public string HouseNumber { get; set; }

            [Display(Name = "Numer telefonu")]
            [Required(ErrorMessage = "Podaj numer telefonu")]
            [DataType(DataType.PhoneNumber)]
            public string PhoneNumber { get; set; }
            
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser {
                    UserName = Input.Email,
                    Email = Input.Email,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    City = Input.City,
                    Street = Input.Street,
                    ZIPCode = Input.ZIPCode,
                    HouseNumber = Input.HouseNumber,
                    PhoneNumber = Input.PhoneNumber};

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}

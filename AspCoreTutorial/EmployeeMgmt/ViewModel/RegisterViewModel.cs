using EmployeeMgmt.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMgmt.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Remote(action: "IsEmailInUse", controller:"Account")]
        [DomainValidation(appDomain: "daimler.com", ErrorMessage = "The domain name must be daimler.com")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string  Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Passoword")]
        [Compare("Password", ErrorMessage = "Password and confirm password should be same")]
        public string ConfirmPassword { get; set; }
    }
}

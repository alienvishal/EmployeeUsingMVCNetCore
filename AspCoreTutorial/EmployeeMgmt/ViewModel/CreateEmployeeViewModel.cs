using EmployeeMgmt.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMgmt.ViewModel
{
    public class CreateEmployeeViewModel
    {
        [MaxLength(50, ErrorMessage = "Name Cannot Exceed to 50 character")]
        [Required]
        public string Name { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invaild Email Format")]
        [Required]
        [Display(Name = "Offical Email")]
        public string Email { get; set; }
        [Required]
        public Dept? Department { get; set; }
        [Display(Name = "Upload File")]
        public IFormFile Photo { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMgmt.Utilities
{
    public class DomainValidationAttribute : ValidationAttribute
    {
        private readonly string appDomain;

        public DomainValidationAttribute(string appDomain)
        {
            this.appDomain = appDomain;
        }

        public override bool IsValid(object value)
        {
            string[] str = value.ToString().Split("@");
            return str[1].ToUpper() == appDomain.ToUpper();
        }
    }
}

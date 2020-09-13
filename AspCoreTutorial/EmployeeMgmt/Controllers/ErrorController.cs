using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMgmt.Controllers
{
    public class ErrorController: Controller
    {
        [Route("Error/{StatusCode}")]
        public IActionResult HttpStatusCode(int StatusCode)
        {
            switch(StatusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry the resource you requested could not found";
                    break;
            }
            return View("NotFound");
        }

        [Route("Error")]
        public IActionResult ExceptionError()
        {
            return View("Exception");
        }
    }
}

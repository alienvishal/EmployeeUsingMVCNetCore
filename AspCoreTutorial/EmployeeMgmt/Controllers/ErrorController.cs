using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMgmt.Controllers
{
    public class ErrorController: Controller
    {
        private readonly ILogger<ErrorController> logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            this.logger = logger;
        }

        [Route("Error/{StatusCode}")]
        public IActionResult HttpStatusCode(int StatusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch(StatusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry the resource you requested could not found";
                    logger.LogWarning($"404 Error Occoured Path = {statusCodeResult.OriginalPath}" + $" and QueryString={statusCodeResult.OriginalQueryString}");
                    break;
            }
            return View("NotFound");
        }

        [Route("Error")]
        public IActionResult ExceptionError()
        {
            var expectionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            logger.LogError($"The path {expectionDetails.Path} threw an exception {expectionDetails.Error}");
            return View("Exception");
        }
    }
}

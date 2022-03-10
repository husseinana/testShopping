using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace API.Errors
{
    [Route("errors/{code}")]
    [ApiExplorerSettings(IgnoreApi =true)]
    public class ErrorController :BaseAPIController
    {
        public IActionResult Error(int code)
        {
            return (new ObjectResult (new ApiResponse(code)));
        }
    }
}
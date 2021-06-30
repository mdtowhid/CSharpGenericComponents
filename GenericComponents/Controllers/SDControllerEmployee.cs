using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericComponents.Controllers
{
    public class SDController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

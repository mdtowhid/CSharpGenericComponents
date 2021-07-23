using ComponentModels;
using ExcelDataReader;
using GenericComponents.Helpers;
using GenericComponents.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GenericComponents.Controllers
{
    public class ExcelController : Controller
    {
        //private readonly IHostingEnvironment _hostingEnvironment;
        //public ExcelController(IHostingEnvironment hostingEnvironment)
        //{
        //    _hostingEnvironment = hostingEnvironment;GemBox.Spreadsheet
        //}

        public IActionResult UploadExcel()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadExcel(IFormFile ExcelFile)
        {
            var l = ExcelHelper.ExcelEngine<Excel>(ExcelFile, "Col");
            return View(l);
        }

        /*
         GemBox.Spreadsheet,
         EPPlus,https://dotnetcoretutorials.com/2019/12/09/reading-excel-files-in-net-core/
         
         */


        
    }
}

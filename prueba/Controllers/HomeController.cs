using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using prueba.Models;
using prueba.Reports;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace prueba.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        private readonly IWebHostEnvironment _oHostEnvironment;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        public HomeController(IWebHostEnvironment oHostEnvironment)
        {
            _oHostEnvironment = oHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}

        public AcceptedResult PrintStudent(int param)
        {
            List<Student> oStudents = new List<Student>();
            for(int i = 1; i <= 10; i++)
            {
                Student oStudent = new Student();
                oStudent.Id = i;
                oStudent.Name = "Student" + i;
                oStudent.Address = "Address" + i;
                oStudents.Add(oStudent);
            }
            StudentReport rpt = new StudentReport(_oHostEnvironment);
            return File(rpt.Report(oStudents), "application/pdf");
        }
    }
}

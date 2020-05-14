using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    using ViewDataItems.TestingController;

    namespace ViewDataItems.TestingController
    {
        public static class VI {
            public const string Name = "name";
        }
    }

    public class TestingController : Controller
    {
        public String Hello(int number)
        {
            return $"number = {number}";
        }

        public IActionResult Index(string name)
        {
            ViewData[VI.Name] = name;
            return View();
        }
    }
}

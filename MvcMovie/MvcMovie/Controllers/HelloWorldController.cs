using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcMovie.Controllers
{
    public class HelloWorldController : Controller
    {
        // GET: HelloWorld
        public ViewResult Index()
        {
            return View();
        }

        public ViewResult Welcome(string name, int num = 1)
        {
            ViewBag.name = "Hello "+name;
            ViewBag.num = num;
            return View();
        }
    }
}
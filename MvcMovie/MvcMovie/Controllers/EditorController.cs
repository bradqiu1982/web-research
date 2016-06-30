using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace MvcMovie.Controllers
{
    public class EditorController : Controller
    {
        // GET: Editor
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, ActionName("Index")]
        [ValidateAntiForgeryToken]
        public ActionResult IndexPost()
        {
            var value = "";
            value = value + Request.Form["editor1"];
            ViewBag.newhtml = Server.HtmlDecode(value);
            return View();
        }
    }
}
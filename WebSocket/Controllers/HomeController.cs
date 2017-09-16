using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSocket.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult WebMessage(string title, string message)
        {
            ViewBag.Title = title;
            ViewBag.Message = HttpUtility.HtmlDecode(message);



            return View();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdvertBoardUI.Controllers
{
    public class PersonalBoardController : Controller
    {
        // GET: PersonalBoard
        public ActionResult Index()
        {
            return View();
        }
    }
}
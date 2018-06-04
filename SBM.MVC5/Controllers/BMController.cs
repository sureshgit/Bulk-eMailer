using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SBM.MVC5.Models;

namespace SBM.MVC5.Controllers
{
    public class BMController : Controller
    {
        // GET: BM
        //[Authorize]
        public ActionResult SendMail()
        {
            return View();
        }

        //[Authorize]
        [HttpPost]
        public ActionResult SendMail(Mail model)
        {
            SendMail sm = new SendMail();
            ViewBag.status = sm.SenDEmail("SBMkEY", model);
            return View();
        }

    }
}
using FishMarket.Web.Infrastructure.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FishMarket.Web.Controllers
{
    [Authorize]
    [SessionExpireFilter]
    public class FishController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
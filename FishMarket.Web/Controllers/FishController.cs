using FishMarket.Model.ViewModel;
using FishMarket.Repository.Interface;
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
        IFish fishRepository;
        public FishController(IFish fishRepository)
        {
            this.fishRepository = fishRepository;
        }

        public ActionResult Index()
        {
            var fishListModel = new FishListViewModel();
            if (Session["CurrentUserId"] != null)
            {
                var userId = (int)Session["CurrentUserId"];
                fishListModel.Fishes = fishRepository.GetFishesByUserId(userId).ToList();
            }

            return View(fishListModel);
        }

        public FileContentResult GetImage(int fishId)
        {
            var fish = fishRepository.GetFishById(fishId);
            if (fish == null)
            {
                return null;
            }

            return File(fish.ImageData, fish.ImageMimeType);
        }
    }
}
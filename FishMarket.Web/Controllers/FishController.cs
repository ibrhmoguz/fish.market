using FishMarket.Model.Entities;
using FishMarket.Model.ViewModel;
using FishMarket.Repository.Interface;
using FishMarket.Web.Infrastructure.Concrete;
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

        public ViewResult Edit(int id)
        {
            var fishModel = new Fish();
            var fish = fishRepository.GetFishById(id);
            if (fish != null)
            {
                fishModel = fish;
            }

            return View(fishModel);
        }

        [HttpPost]
        public ActionResult Edit(Fish fish, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                fish.UserId = (int)Session["CurrentUserId"];
                if (image != null)
                {
                    fish.ImageMimeType = image.ContentType;
                    fish.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(fish.ImageData, 0, image.ContentLength);
                }
                fishRepository.SaveFish(fish);
                TempData["message"] = string.Format("{0}  has  been  saved", fish.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(fish);
            }
        }

        public ViewResult RemoveImage(int fishId)
        {
            var fish = fishRepository.GetFishById(fishId);
            fish.ImageData = null;
            fish.ImageMimeType = null;
            fishRepository.SaveFish(fish);
            return View("Edit", fish);
        }
    }
}
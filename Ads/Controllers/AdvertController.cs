using System;
using Ads.Database;
using Ads.Services;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Ads.Controllers
{
    public class AdvertController : BaseController
    {
        private IAdsDbContext _adsDbContext;
        public AdvertController()
        {
            _adsDbContext = new AdsDbContext();
        }
        public ActionResult Iframe(int advertId)
        {
            var advert = GetAdvertById(advertId);
            return View(advert);
        }

        [HttpGet]
        public JsonResult GetAdvert(int advertId)
        {
            return Json(GetAdvertById(advertId), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetAll()
        {
            return Json(_adsDbContext.GetAllByUserId(userId), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveAdvert(Advert advert)
        {
            advert.UserId = userId;
            if (advert.Id == 0)
                return Json(_adsDbContext.Add(advert), JsonRequestBehavior.AllowGet);

            return Json(_adsDbContext.Update(advert), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void Delete(int advertId)
        {
            _adsDbContext.Delete(advertId);
        }

        [HttpPost]
        public JsonResult Upload()
        {
            var result = "";
            if (Request.Files.AllKeys.Any())
            {
                var httpPostedFile = Request.Files["UploadedImage"];

                if (httpPostedFile != null)
                {
                    var fileExt = Path.GetExtension(httpPostedFile.FileName)?.Substring(1);
                    var fileName = $"{Guid.NewGuid().ToString()}.{fileExt}";

                    var fileSavePath = Path.Combine(Server.MapPath("~/UploadedFiles"), fileName);
                    httpPostedFile.SaveAs(fileSavePath);
                    result = fileName;
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private Advert GetAdvertById(int advertId)
        {
            return _adsDbContext.GetById(advertId);
        }
    }
}

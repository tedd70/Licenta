using Ads.Database;
using Ads.Services;
using System.Web.Mvc;

namespace Ads.Controllers
{
    public class ConfigurationController : BaseController
    {
        private IAdsDbContext _adsDbContext;
        public ConfigurationController()
        {
            _adsDbContext = new AdsDbContext();
        }
        public ActionResult Index()
        {
            if (!isAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public Advert GetAdvert(int advertId)
        {
            return _adsDbContext.GetById(advertId);
        }

        public Advert Add(Advert advert)
        {
            return null;
        }
    }
}

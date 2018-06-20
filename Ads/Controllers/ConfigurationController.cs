using Ads.Database;
using Ads.Services;
using System.Web.Mvc;

namespace Ads.Controllers
{
    public class ConfigurationController : Controller
    {
        private IAdsDbContext _adsDbContext;
        public ConfigurationController()
        {
            _adsDbContext = new AdsDbContext();
        }
        public ActionResult Index()
        {
            var adsList = _adsDbContext.GetAll();
            return View(adsList);
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

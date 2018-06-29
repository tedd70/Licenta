using Ads.Database;
using Ads.Services;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Ads.Controllers
{
    public class ProductsController : Controller
    {
        public JsonResult GetAll()
        {
            var products = new List<Product>
            {
                new Product {
                    Currency="$",
                    Description="",
                    Id=1,
                    Image="http://www.pplandscape-gardening.co.uk/images/6mibsYZgW5q/nike-men-black-red-2011-air-presto-84QZ.jpg",
                    Price=90,
                    Title="Professional Running Shoes",
                    OldPrice=150,
                    Url="http://www.nike.com"
                },

                new Product
                {
                    Currency="RON",
                    Image="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRn8whj7DbffK_ax0FhAd-pOiXRxmV2lRIPp-aYjCzgezdvsdB-5w",
                    Id=2,
                    Price=300,
                    OldPrice=449,
                    Title="Professional Bascketball Shoes",
                    Url="http://www.nike.com",
                    Description=""
                },

                new Product
                {
                    Currency="RON",
                    Image="https://c.static-nike.com/a/images/t_PDP_864_v1/f_auto/cna0q5tvlmb9lfdbne5m/air-max-97-shoe-z3TlrlVN.jpg",
                    Id=3,
                    Price=249,
                    OldPrice=399,
                    Title="Confort Shoes",
                    Url="http://www.nike.com",
                    Description=""
                },

                new Product
                {
                    Currency="$",
                    Image="https://c.static-nike.com/a/images/t_PDP_864_v1/f_auto/axjsjtginfybsjoriwec/jordan-flyknit-elevation-23-shoe-2Mw3Y0.jpg",
                    Id=4,
                    Price=199,
                    OldPrice=249,
                    Title="Walking shoes",
                    Url="http://www.nike.com",
                    Description=""
                }
            };
            return Json(products, JsonRequestBehavior.AllowGet);
        }
    }
}

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
                    Image="https://images.nike.com/is/image/DotCom/PDP_HERO/AO5117_100_A_PREM/air-max-plus-nic-qs-shoe.jpg",
                    Id=2,
                    Price=300,
                    OldPrice=449,
                    Title="Professional Bascketball Shoes",
                    Url="https://store.nike.com/ro/en_gb/pd/air-max-plus-nic-qs-shoe/pid-12257967/pgid-12364962",
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

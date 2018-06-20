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
    public class BaseController : Controller
    {
        public int userId => int.Parse(Request.Cookies["userId"].Value);
    }
}

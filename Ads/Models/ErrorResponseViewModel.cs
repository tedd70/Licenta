using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ads.Models
{
    public class ErrorResponseViewModel
    {
        public bool HasErrors { get; set; }
        public List<string> Errors { get; set; }
    }
}
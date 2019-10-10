using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShenYu.mgr.core.Areas.Admin.Controllers;

namespace ShenYu.mgr.core.Controllers
{
    public class MieShenController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
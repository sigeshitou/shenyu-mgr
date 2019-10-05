﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ShenYu.mgr.core.Controllers
{
    public class HomeController : ControllerBase
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>

        public ActionResult DashBord()
        {
            return Content($"Ant后台快速开发框架 版本号：{ typeof(Startup).Assembly.GetName().Version.ToString()}");
        }
    }
}
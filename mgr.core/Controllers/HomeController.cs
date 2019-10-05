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
            return Content($"神域科技后台系统");
        }
    }
}
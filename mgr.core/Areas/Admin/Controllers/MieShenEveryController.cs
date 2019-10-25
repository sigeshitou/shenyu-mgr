using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Logging;
using Microsoft.AspNetCore.Mvc;
using Repository.DapperRepository;
using Repository.Interface;
using ViewModels.Admin;

namespace ShenYu.mgr.core.Areas.Admin.Controllers
{
    [API("产出")]
    [Area("Admin")]
    public class MieShenEveryController : BaseController
    {
        private readonly ICommonRespository CommonRespository;
        private readonly DapperClient _SqlDB;
        public MieShenEveryController(ICommonRespository _commonRespository, IDapperFactory dapperFactory)
        {

            CommonRespository = _commonRespository;
            _SqlDB = dapperFactory.CreateClient("SqlDb");
        }
        public IActionResult Index()
        {
            return View();
        }
        public ViewResult MieShenEveryList()
        {
            return View();
        }
        /// <summary>
        /// 获取相应的列表数据
        /// </summary>
        /// <returns></returns>
        [API("分页获取灭神产出数据")]
        public async Task<JsonResult> GetMieShenEveryList(MieShenEveryVm model)
        {
            string strwhere = "1=1";
            if (model.Region != null && model.Region != "")
            {
                strwhere += $" and region in (select realregion from region_real where region=" + model.Region + ")";
            }
            if (model.Platform != null && model.Platform != "")
            {
                strwhere += $" and platfrom='" + model.Platform + "'";
            }
            if (model.Bdate != null && model.Bdate != "")
            {
                strwhere += $" and bdate='" +Convert.ToDateTime(model.Bdate).ToString("yyyy-MM-dd") + "'";
            }
            model.Sql = @"select * from mieshen_every  where " + strwhere;
            var result = await CommonRespository.GetQueryResult(_SqlDB, model);
         
            return Json(result);
        }
    }
}
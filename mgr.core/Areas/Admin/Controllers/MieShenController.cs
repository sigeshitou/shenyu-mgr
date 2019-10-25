using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Configuration;
using Microsoft.AspNetCore.Mvc;
using Repository.DapperRepository;
using Repository.Interface;
using ViewModels.Admin;
using ViewModels.Result;

namespace ShenYu.mgr.core.Areas.Admin.Controllers
{
  
    [API("灭神账号")]
    [Area("Admin")]
    public class MieShenController : BaseController
    {
        private readonly ICommonRespository CommonRespository;
        private readonly DapperClient _SqlDB;
        public MieShenController(ICommonRespository _commonRespository, IDapperFactory dapperFactory)
        {

            CommonRespository = _commonRespository;
            _SqlDB = dapperFactory.CreateClient("SqlDb");
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public ViewResult MieShenList()
        {
            return View();
        }
        /// <summary>
        /// 获取相应的列表数据
        /// </summary>
        /// <returns></returns>
        [API("分页获取灭神账号")]
        public async Task<JsonResult> GetMieShenList(MieShenVm model)
        {
            string strwhere = "1=1";
            if (model.UserName != null && model.UserName != "")
            {
                strwhere += $" and a.username='" + model.UserName + "'";
            }
            model.Sql = $"select * from region_real where  " + strwhere;
            model.Sql = @"select a.*,ISNULL(b.status,a.status) sta from mieshen_info  a left join mieshen_online b
on a.platfrom = b.platfrom and a.region = b.region and a.gamename = b.gamename and a.username = b.username where "+ strwhere;
            var result = await CommonRespository.GetQueryResult(_SqlDB,model);

            return Json(result);
        }
    }
}
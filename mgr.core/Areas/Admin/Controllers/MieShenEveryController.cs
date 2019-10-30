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
            model.Sql = @"select * from mieshen_every ss  where ";


            if (model.Bdate != null && model.Bdate != "")
            {
                if (Convert.ToDateTime(model.Bdate).Date==DateTime.Now.Date)
                {
                    model.Sql = @" select * from (select aa.platfrom,aa.region,CONVERT(VARCHAR(10),GETDATE(),111) bdate,bb.BindMoney,bb.Money,
 bb.BindMoney-aa.BindMoney yesBindMoney,bb.Money-aa.Money yesMoney from (
 SELECT a.platfrom,b.realregion region,SUM(BindMoney) BindMoney,SUM(Money) Money FROM  mieshen_every (NOLOCK) a,dbo.region_real b
  WHERE a.platfrom=b.platform AND a.region=b.region  AND a.bdate=CONVERT(VARCHAR(10),DATEADD(DAY,-1,GETDATE()),111)
  GROUP BY a.platfrom,b.realregion) aa,
  (
 SELECT a.platfrom,b.realregion region,SUM(ISNULL(a.BindMoney,0)) BindMoney,sum(ISNULL(a.Money,0)) Money FROM dbo.mieshen_info (NOLOCK) a,dbo.region_real b
  WHERE a.platfrom=b.platform AND a.region=b.region AND a.gamename=b.gamename
  GROUP BY a.platfrom,b.realregion) bb where aa.platfrom=bb.platfrom
  and aa.region=bb.region) ss where 
  ";
                }
                else
                {
                    strwhere += $" and ss.bdate='" + Convert.ToDateTime(model.Bdate).ToString("yyyy-MM-dd") + "'";
                }
              
            }
           
            if (model.Region != null && model.Region != "")
            {
                strwhere += $" and ss.region in (select realregion from region_real where region=" + model.Region + ")";
            }
            if (model.Platform != null && model.Platform != "")
            {
                strwhere += $" and ss.platfrom='" + model.Platform + "'";
            }
            model.Sql = model.Sql + strwhere;
            var result = await CommonRespository.GetQueryResult(_SqlDB, model);
         
            return Json(result);
        }
    }
}
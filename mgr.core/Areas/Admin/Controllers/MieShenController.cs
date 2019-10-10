using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Configuration;
using Microsoft.AspNetCore.Mvc;
using Repository.DapperRepository;
using ViewModels.Admin;
using ViewModels.Result;

namespace ShenYu.mgr.core.Areas.Admin.Controllers
{
  
    [API("Home")]
    [Area("Admin")]
    public class MieShenController : BaseController
    {
        private readonly DapperClient _SqlDB;
        public MieShenController(IDapperFactory dapperFactory)
        {
           
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
        [API("分页获取所有的角色")]
        public async Task<JsonResult> GetMieShenList(MieShenVm model)
        {
            var result = new SearchResult<List<dynamic>>();
            
            var respositoryResult = _SqlDB.Query<dynamic>(@"exec p_mieshen_query @username", new { username = model.UserName });
            result.Status = ResultConfig.Ok;
            result.Info = ResultConfig.SuccessfulMessage;
            result.Rows = respositoryResult;
            result.Total = respositoryResult.Count;
           
            return Json(result);
 
            //var result = new SearchResult<List<SystemRole>>();
            //var respositoryResult = await RoleRespository.GetList(model, UserToken);
            //result.Status = ResultConfig.Ok;
            //result.Info = ResultConfig.SuccessfulMessage;
            //result.Rows = respositoryResult.Item2;
            //result.Total = respositoryResult.Item1;
            //return Json(result);
        }
    }
}
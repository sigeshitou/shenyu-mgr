using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Configuration;
using Microsoft.AspNetCore.Mvc;
using Repository.DapperRepository;
using Repository.Interface;
using ShenYu.mgr.core.Filter;
using ViewModels.Admin;
using ViewModels.Result;
using ViewModels.Reuqest;

namespace ShenYu.mgr.core.Areas.Admin.Controllers
{
    [API("Home")]
    [Area("Admin")]
    public class RegionController : Controller
    {
        private readonly DapperClient _SqlDB;
        private readonly ICommonRespository CommonRespository;

        public RegionController(ICommonRespository _commonRespository, IDapperFactory dapperFactory)
        {
           
            CommonRespository = _commonRespository;
            _SqlDB = dapperFactory.CreateClient("SqlDb");


        }
        public IActionResult Index()
        {
            return View();
        }
        public ViewResult RegionList()
        {
            return View();
        }
        /// <summary>
        /// 获取相应的列表数据
        /// </summary>
        /// <returns></returns>
        [API("分页相应的区服列表")]
        public async Task<JsonResult> GetRegionList(RegionVm model)
        {
            string strwhere = "1=1";
            if (model.Region!=null&& model.Region!="")
            {
                strwhere+=$" and region={model.Region} or realregion={model.Region}";
            }
            model.Sql = $"select * from region_real where  "+ strwhere;
            var result = await CommonRespository.GetQueryResult(_SqlDB, model);

            return Json(result);
 
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [API("新增区服")]
        public async Task<JsonResult> AddRegion([ModelBinder(typeof(JsonNetBinder)), FromForm]RegionDataVm vm)
        {
            var result = new ResultJsonNoDataInfo();
            try
            {
                string sql = $"exec p_mieshen_create '{vm.Platform}',{vm.Region},'{vm.Opentime}'";
                var respositoryResult = _SqlDB.Execute(sql);
                result.Status = ResultConfig.Ok;
                result.Info = ResultConfig.SuccessfulMessage;
            }
            catch (Exception ex)
            {

                result.Status = ResultConfig.Fail;
                result.Info = ex.Message;
            }
            
            return Json(result);
        }
       
        [HttpPost]
        [API("修改区服")]
        public async Task<JsonResult> EditRegion([ModelBinder(typeof(JsonNetBinder)), FromForm]RegionDataVm vm)
        {
            var result = new ResultJsonNoDataInfo();
            try
            {
                string sql = $"update region_real set realregion={vm.RealRegion} where gamename='MS' and platform='{vm.Platform}' and region={vm.Region}";
                var respositoryResult = _SqlDB.Execute(sql);
                result.Status = ResultConfig.Ok;
                result.Info = ResultConfig.SuccessfulMessage;
            }
            catch (Exception ex)
            {

                result.Status = ResultConfig.Fail;
                result.Info = ex.Message;
            }

            return Json(result);
        }
        [HttpPost]
        [API("查询平台区服")]
        public async Task<JsonResult> GetPlatform(string id)
        {
            var result = new SearchResult<List<dynamic>>();
            try
            {
                string sql = $"SELECT  code,descript FROM basecode WHERE cat=@cat ORDER BY seq";
                var respositoryResult = _SqlDB.Query<dynamic>(sql,new { cat=id}).ToList();
                result.Rows = respositoryResult;
                result.Status = ResultConfig.Ok;
                result.Info = ResultConfig.SuccessfulMessage;
            }
            catch (Exception ex)
            {

                result.Status = ResultConfig.Fail;
                result.Info = ex.Message;
            }

            return Json(result);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [API("删除区服")]
        public async Task<JsonResult> DeleteRegion([ModelBinder(typeof(JsonNetBinder)), FromForm]RegionDataVm vm)
        {
            var result = new ResultJsonNoDataInfo();
            try
            {
                string sql = $"delete from region_real where gamename='MS' and platform='{vm.Platform}' and region={vm.Region}";
                var respositoryResult = _SqlDB.Execute(sql);
                result.Status = ResultConfig.Ok;
                result.Info = ResultConfig.SuccessfulMessage;
            }
            catch (Exception ex)
            {

                result.Status = ResultConfig.Fail;
                result.Info = ex.Message;
            }

            return Json(result);
        }
    }
}
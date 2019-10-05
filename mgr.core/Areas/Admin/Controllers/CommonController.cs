﻿using System.Collections.Generic;
using System.IO;
using ShenYu.mgr.core.Filter;
using Configuration;
using Infrastructure.Web;
using Microsoft.AspNetCore.Mvc;
using Repository.Interface;
using ServicesModel;
using ViewModels.Result;
using ViewModels.Reuqest;

namespace ShenYu.mgr.core.Areas.Admin.Controllers
{
    /// <summary>
    /// 公共
    /// </summary>
    [API("公共")]
    [Area("Admin")]
    public class CommonController : BaseController
    {
        private readonly ICommonRespository CommonRespository;


        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="_commonRespository"></param>
        public CommonController(ICommonRespository _commonRespository)
        {
            CommonRespository = _commonRespository;
        }




        #region CodeGen

        /// <summary>
        /// 获取所有的表名称和列
        /// </summary>
        /// <returns></returns>
        [AuthorizeFilter]
        [API("获取所有的表名称和列")]
        [HttpPost]
        public JsonResult GetDbTablesAndColumns()
        {
            var result = new ResultJsonInfo<string>();
            var respositoryResult = CommonRespository.GetDbTablesAndColumns();
            result.Status = ResultConfig.Ok;
            result.Info = ResultConfig.SuccessfulMessage;
            result.Data = "var schema = " + respositoryResult;
            return Json(result);
        }

        /// <summary>
        /// 获取数据库表
        /// </summary>
        /// <returns></returns>

        [AuthorizeFilter]
        [API("获取数据库表")]
        [HttpPost]
        public JsonResult GetDbTables()
        {
            var result = new ResultJsonInfo<List<CodeGenTable>>();
            var respositoryResult = CommonRespository.GetDbTables();
            result.Status = ResultConfig.Ok;
            result.Info = ResultConfig.SuccessfulMessage;
            result.Data = respositoryResult;
            return Json(result);
        }

        /// <summary>
        /// 获取数据表下的所有字段
        /// </summary>
        /// <returns></returns>
        [AuthorizeFilter]
        [API("获取数据表下的所有字段")]
        [HttpPost]
        public JsonResult GetDbTableColumns(string tableName)
        {
            var result = new ResultJsonInfo<List<CodeGenField>>();
            var respositoryResult = CommonRespository.GetDbTablesColumns(tableName);
            result.Status = ResultConfig.Ok;
            result.Info = ResultConfig.SuccessfulMessage;
            result.Data = respositoryResult;
            return Json(result);
        }

        /// <summary>
        /// 生成代码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, FileDownload]
        [AuthorizeFilter]
        [API("生成代码")]
        public ActionResult CodeGenDown([FromForm] CodeGenVm model)
        {
            var data = CommonRespository.CodeGen(model);
            return File(data, System.Net.Mime.MediaTypeNames.Application.Zip, "CodeGen_" + model.TableName.Split('→')[0] + ".zip");
        }
        #endregion





        #region 代码自动生成

        /// <summary>
        /// 代码生成页面
        /// </summary>
        /// <returns></returns>
        [AuthorizeFilter]
        [API("代码生成页面访问")]
        public ActionResult CodeGen()
        {
            return View();
        }

        #endregion




        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [AuthorizeFilter]
        [API("下载文件")]
        public ActionResult Download(string fileName)
        {
            fileName = fileName.Replace("'", "");
            var filePath = WebUtils.GetMapPath(fileName);
            return File(filePath, "application/octet-stream", new FileInfo(filePath).Name);
        }


    }
}
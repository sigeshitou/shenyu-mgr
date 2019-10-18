using System;
using System.Collections.Generic;
using System.Text;
using ViewModels.Condition;

namespace ViewModels.Admin
{
    /// <summary>
    /// 区服模型
    /// </summary>
    public class RegionVm : ConditionBase
    {
        /// <summary>
        /// 平台
        /// </summary>
        public string Platform { get; set; }

        /// <summary>
        /// 区服
        /// </summary>
        public string Region { get; set; }
        /// <summary>
        /// 真实区服
        /// </summary>

        public string RealRegion { get; set; }
        /// <summary>
        /// 开区时间
        /// </summary>
        public string  Opentime { get; set; }

    }
    public class RegionDataVm 
    {
        /// <summary>
        /// 平台
        /// </summary>
        public string Platform { get; set; }

        /// <summary>
        /// 区服
        /// </summary>
        public string Region { get; set; }
        /// <summary>
        /// 真实区服
        /// </summary>

        public string RealRegion { get; set; }
        /// <summary>
        /// 开区时间
        /// </summary>
        public string Opentime { get; set; }

    }
}

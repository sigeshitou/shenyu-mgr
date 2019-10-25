using System;
using System.Collections.Generic;
using System.Text;
using ViewModels.Condition;
 

namespace ViewModels.Admin
{
    public  class MieShenVm: ConditionBase
    {
        public string UserName { get; set; }
       
    }
    public class MieShenEveryVm : ConditionBase
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
        /// 时间
        /// </summary>
        public string Bdate { get; set; }

    }
}

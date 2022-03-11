using System;
using System.Collections.Generic;
using System.Text;

namespace Hcj.MyWeb.Dal.Models.Common
{

    /// <summary>
    /// 项目统一返回类
    /// </summary>
    public class BaseResponse
    {
        public bool Flag { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }
        public dynamic Data { get; set; }
    }
}

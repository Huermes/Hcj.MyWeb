using Hcj.MyWeb.Dal.Models.PO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hcj.MyWeb.Dal.Models.BO
{
    /// <summary>
    /// 查询BO
    /// </summary>
    public class MessageQueryResponse : TT_Hcj_Message_PO
    {
        public int Total { get; set; }
    }
}

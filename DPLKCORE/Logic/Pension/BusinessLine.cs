using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DPLKCORE.Class.Pension
{
    public class BusinessLine
    {
        public int BusinessLineNmbr { get; set; }
        public string BusinessLineNm { get; set; }
        public short BusinessSctrNmbr { get; set; }
        public DateTime LastChangeDt { get; set; }

        //public virtual BusinessSctr BusinessSctrNmbrNavigation { get; set; }
    }
}
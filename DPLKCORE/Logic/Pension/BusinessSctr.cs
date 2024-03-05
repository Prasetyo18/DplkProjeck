using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DPLKCORE.Class.Pension
{
    public class BusinessSctr
    {
       /* public BusinessSctr()
        {
            BusinessLines = new HashSet<BusinessLine>();
        }*/

        public short BusinessSctrNmbr { get; set; }
        public string BusinessSctrNm { get; set; }
        public DateTime LastChangeDt { get; set; }

        public virtual ICollection<BusinessLine> BusinessLines { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace DPLKCORE.Class
{
    public class ReportUtility
    {
        public static Stream ConvertToStream(String reportDef, ReportDocument rd)
        {
            System.IO.Stream st = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            //ExportOptions _option = new ExportOptions();
            //_option.ExportFormatOptions = expo
            return st;
        }
    }
}
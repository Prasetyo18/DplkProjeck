using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

namespace DPLKCORE.Class
{
    public class JavaScript
    {
        //private Page page = HttpContext.Current.CurrentHandler as Page;

        public void ErrorMessage(Page p,string Message)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<script type='text/javascript' >");
            sb.AppendLine("var jq = jQuery.noConflict();");
            sb.AppendLine("jq(window).load(function () {");
            sb.AppendLine("Swal.fire({");
            sb.AppendLine("icon:'error',");
            sb.AppendLine("text:'" + Message + "'");
            sb.AppendLine("})");
            sb.AppendLine("});");
            sb.AppendLine("</script>");

            p.ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
        }

        public void ModalPopUp(Page p, string ID)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<script type='text/javascript' >");
            sb.AppendLine("var jq = jQuery.noConflict();");
            sb.AppendLine("jq(window).load(function () {");
            sb.AppendLine("jq('#" + ID + "').modal('show');");
            sb.AppendLine(" });");
            sb.AppendLine("</script>");

            p.ClientScript.RegisterClientScriptBlock(this.GetType(), "Modal", sb.ToString());
        }
    }
}
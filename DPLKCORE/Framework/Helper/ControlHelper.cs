using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DPLKCORE.Framework.Helper
{
    public class ControlHelper
    {
        public static void ClearTextBox(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).Text = String.Empty;
                }
                if (control.Controls.Count > 0)
                {
                    ClearTextBox(control);
                }
            }
        }

        public static T FindControlRecursive<T>(Control parent, string controlId) where T : Control
        {
            if (parent == null)
            {
                return null;
            }

            if (parent.ID == controlId)
            {
                return (T)parent;
            }

            foreach (Control control in parent.Controls)
            {
                T foundControl = FindControlRecursive<T>(control, controlId);
                if (foundControl != null)
                {
                    return foundControl;
                }
            }

            return null;
        }
    }
}
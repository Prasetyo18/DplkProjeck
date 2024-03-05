using AjaxControlToolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace DPLKCORE.Framework.Helper
{
    public class TabHelper
    {
        public static void NextTab(Control parent)
        {
            TabContainer tabContainer = LocateTabContainer(parent);
            if (tabContainer != null)
            {
                if (tabContainer.ActiveTabIndex < tabContainer.Tabs.Count - 1)
                {
                    tabContainer.ActiveTabIndex += 1;
                }
            }

        }

        private static TabContainer LocateTabContainer(Control parent)
        {
            foreach (Control item in parent.Controls)
            {
                if (item is TabContainer)
                {
                    return (TabContainer)item;
                }

                if (item.HasControls())
                {
                    TabContainer foundContainer = LocateTabContainer(item);
                    if (foundContainer != null)
                    {
                        return foundContainer;
                    }
                }
            }
            return null;
        }
    }
}
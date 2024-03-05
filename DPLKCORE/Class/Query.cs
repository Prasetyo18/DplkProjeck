using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DPLKCORE.Class
{
    public class Query
    {
        public static String Where(List<String> arguments, Boolean isWhere)
        {
            String where = "";
            int i = 0;
            if (arguments.Count > 0)
            {
                if (isWhere)
                {
                    where = String.Format(" where {0} ", arguments[0]);
                }
                else
                {
                    where = String.Format("and {0} ", arguments[0]);
                }

                i = 1;
                while (i < arguments.Count)
                {
                    where += String.Format(" and {0} ", arguments[i]);
                    i++;
                }
            }

            return where;
        }

        public static String Where(List<String> arguments)
        {
            return Where(arguments, true);
        }
    }
}
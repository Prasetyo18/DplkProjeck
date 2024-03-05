using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DPLKCORE.Framework;
using System.Data;

namespace DPLKCORE.Logic.Pension
{
    public class ProductSetupModels
    {
        public int productTypeNmbr { get; set; }
        public int setupTypeNmbr { get; set; }
        public string setupTypeNm { get; set; }
        public string defltValue { get; set; }
        public string additionalInfo { get; set; }
        public DateTime lastChangeDt { get; set; }


        public List<ProductSetupModels> SetupProduct(Database db)
        {
            List<ProductSetupModels> output = new List<ProductSetupModels>();
            string query = "EXEC SP_PRODUCT_SETUP_R @product_type_nmbr";
            db.setQuery(query);
            db.AddParameter("@product_type_nmbr", this.productTypeNmbr);

            DataTable dt = db.ExecuteQuery();
            foreach (DataRow item in dt.Rows)
            {
                ProductSetupModels model = new ProductSetupModels
                {
                    setupTypeNmbr = Convert.ToInt32(item["setup_type_nmbr"]),
                    setupTypeNm = item["setup_type_nm"].ToString(),
                    defltValue = item["deflt_value"].ToString(),
                    additionalInfo = item["additional_info"].ToString(),
                    lastChangeDt = Convert.ToDateTime(item["last_change_dt"])
                };
                output.Add(model);
            }
            return output;

        }


    }
}
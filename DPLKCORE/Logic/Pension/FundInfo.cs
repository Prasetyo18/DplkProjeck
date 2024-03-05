using DPLKCORE.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DPLKCORE.Logic.Pension
{
    public class FundInfo
    {

        public short InvTypeNumber { get; set; }
        public short MoneyTypeNumber { get; set; }
        public float ContribCountPeriod { get; set; }
        public float ContribPeriod { get; set; }
        public float DistribCountPeriod { get; set; }
        public float DistribPeriod { get; set; }
        public float FeeCountPeriod { get; set; }
        public float FeePeriod { get; set; }
        public float LapseCountPeriod { get; set; }
        public float LapsePeriod { get; set; }
        public float RiskCountPeriod { get; set; }
        public float RiskPeriod { get; set; }
        public float RollEarnCountPeriod { get; set; }
        public float RollEarnPeriod { get; set; }
        public float UnitCount { get; set; }
        public float AccountValue { get; set; }
        public float Earning { get; set; }
        public float MoveoutCountPeriod { get; set; }
        public float MoveoutPeriod { get; set; }
        public float MoveinCountPeriod { get; set; }
        public float MoveinPeriod { get; set; }
        public float OtherFeePeriod { get; set; }
        public float OtherFeeCountPeriod { get; set; }
        public float ContribCountDt { get; set; }
        public float ContribDt { get; set; }
        public float DistribCountDt { get; set; }
        public float DistribDt { get; set; }
        public float FeeCountDt { get; set; }
        public float FeeDt { get; set; }
        public float LapseCountDt { get; set; }
        public float LapseDt { get; set; }
        public float RiskCountDt { get; set; }
        public float RiskDt { get; set; }
        public float RollEarnCountDt { get; set; }
        public float RollEarnDt { get; set; }
        public float MoveoutCountDt { get; set; }
        public float MoveoutDt { get; set; }
        public float MoveinCountDt { get; set; }
        public float MoveinDt { get; set; }
        public float Price { get; set; }
        public float BeginningUnitCount { get; set; }
        public float TACountDt { get; set; }
        public float TADt { get; set; }
        public float OtherFeeDt { get; set; }
        public float OtherFeeCountDt { get; set; }
        public float TACountPeriod { get; set; }
        public float TAPeriod { get; set; }
        public short StatusTypeNumber { get; set; }
        public int ProductTypeNumber { get; set; }
        public float ContributionCountRedirect { get; set; }
        public float ContributionRedirectPeriod { get; set; }
        public float UnitNonFund { get; set; }
        public float FeeNonFund { get; set; }
        public string ClientNm { get; set; }
        public string ClientNmbr { get; set; }



        public string GetClientNameOnCertificateFund(Database db)
        {
            string query = "EXEC dbo.TXT_CLIENTNAME_ON_SCR_CERTIFICATEFUND @client_nmbr";
            string clientName = string.Empty;

            try
            {
                db.setQuery(query);
                db.AddParameter("@client_nmbr", ClientNmbr);

                using (var reader = db.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        clientName = reader.GetString(0);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return clientName;
        }

    }

}

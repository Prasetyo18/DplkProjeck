using DPLKCORE.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DPLKCORE.Logic.Pension
{
    public class WithdrawEstimation
    {

        public int ProductTypeNumber { get; set; }
        public int GroupNumber { get; set; }
        public float MinAnnuityPercentage { get; set; }
        public float MinAnnuityAmount { get; set; }
        public DateTime MppCertificateEffectiveDate { get; set; }

        public int SubTransactionNumber { get; set; }
        public int MoneyTypeNumber { get; set; }
        public float NetContribution { get; set; }
        public float WithdrawalContribution { get; set; }
        public int InvestmentTypeNumber { get; set; }
        public float AccountValue { get; set; }
        public float MoneyAccountValue { get; set; }
        public float MoneyWithdrawal { get; set; }
        public float RealWithdrawal { get; set; }
        public string MoneyType { get; set; }
        public string InvestmentType { get; set; }
        public float Contribution { get; set; }
        public float Distribution { get; set; }
        public float NetContributionEarning { get; set; }
        public float Earning { get; set; }
        public float Fee { get; set; }
        public float AccountValueEarning { get; set; }
        public float Unit { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime UnitPriceEffectiveDate { get; set; }
        public int CertificateNumber { get; set; }
        public DateTime CycleDate { get; set; }
        public int WithdrawalTypeNumber { get; set; }
        public float WithdrawalPercentage { get; set; }
        public string ProcessorId { get; set; }
        public int BatchId { get; set; }
        public short EstimateType { get; set; }
        public string LsBankName { get; set; }
        public string LsAccountNumber { get; set; }
        public string LsAccountName { get; set; }
        public string LsNote { get; set; }
        public string AnBankName { get; set; }
        public string AnAccountNumber { get; set; }
        public string AnAccountName { get; set; }
        public string AnNote { get; set; }
        public float LsPercentage { get; set; }
        public string LsBranch { get; set; }
        public string AnBranch { get; set; }
        public int CerNmbrMpp { get; set; }
        public float WdGrossAdditional { get; set; }
        public DateTime ExitDate { get; set; }
        public int GroupFlag { get; set; }
        public int ForfeitureMppFlag { get; set; }
        public int TransactionOldNumber { get; set; }
        public int WdTransferType { get; set; }


        public void CalculatorClick(Database db)
        {
            string query = "exec sp_WD_estimate_new " +
                           "@cer_nmbr, @trns_dt, @withd_pct, @sub_Trns_nmbr, " +
                           "@processor_id, @batch_id, @estimate_type, " +
                           "@ls_bank_nm, @ls_acct_nmbr, @ls_acct_nm, @ls_note, " +
                           "@an_bank_nm, @an_acct_nmbr, @an_acct_nm, @an_note, " +
                           "@ls_pct, @ls_branch, @an_branch, @cer_nmbr_MPP, " +
                           "@wd_gross_additional, @exit_dt, @grp_flg, " +
                           "@forfeiture_MPP_flg, @trns_old_nmbr, @wd_transfer_type";

            try
            {
                db.setQuery(query);

                db.AddParameter("@cer_nmbr",CertificateNumber);
                db.AddParameter("@trns_dt", TransactionDate);
                db.AddParameter("@withd_pct", WithdrawalPercentage);
                db.AddParameter("@sub_Trns_nmbr", SubTransactionNumber);
                db.AddParameter("@processor_id", ProcessorId);
                db.AddParameter("@batch_id", BatchId);
                db.AddParameter("@estimate_type", EstimateType);
                db.AddParameter("@ls_bank_nm", LsBankName);
                db.AddParameter("@ls_acct_nmbr", LsAccountNumber);
                db.AddParameter("@ls_acct_nm", LsAccountName);
                db.AddParameter("@ls_note", LsNote);
                db.AddParameter("@an_bank_nm", AnBankName);
                db.AddParameter("@an_acct_nmbr", AnAccountNumber);
                db.AddParameter("@an_acct_nm", AnAccountName);
                db.AddParameter("@an_note", AnNote);
                db.AddParameter("@ls_pct", LsPercentage);
                db.AddParameter("@ls_branch", LsBranch);
                db.AddParameter("@an_branch", AnBranch);
                db.AddParameter("@cer_nmbr_MPP", CerNmbrMpp);
                db.AddParameter("@wd_gross_additional", WdGrossAdditional);
                db.AddParameter("@exit_dt", ExitDate);
                db.AddParameter("@grp_flg",GroupFlag);
                db.AddParameter("@forfeiture_MPP_flg", ForfeitureMppFlag);
                db.AddParameter("@trns_old_nmbr", TransactionOldNumber);
                db.AddParameter("@wd_transfer_type", WdTransferType);

                db.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void GetWithdrawalAvailableAmount(Database db)
        {
            string query = "exec sp_wd_available_amt " +
                           "@cer_nmbr, @cycle_dt, @wd_type_nmbr";

            try
            {
                db.setQuery(query);

                db.AddParameter("@cer_nmbr", CertificateNumber);
                db.AddParameter("@cycle_dt", CycleDate);
                db.AddParameter("@wd_type_nmbr", WithdrawalTypeNumber);

                db.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }





    }
}
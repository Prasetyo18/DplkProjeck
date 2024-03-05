using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DPLKCORE.Logic.Pension
{
    public class ProsesClaimRequest
    {
        public string RegId { get; set; }
        public int TrnsId { get; set; }
        public string TransType { get; set; }
        public string Name { get; set; }
        public string Dob { get; set; }
        public string Phone { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string TakenBy { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DPLKCORE.Logic.Pension
{
    public partial class DbBank
    {
        public int SeqNmbr { get; set; }
        public string BankNmbr { get; set; }
        public string BankBiNmbr { get; set; }
        public string BankNm { get; set; }
        public string BankAddr { get; set; }
        public int? BankCentral { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASCIWebApp.Models
{
    public class IACSBankCustomerRequireTheDepositortoTtheBank
    {
        public int IACSBankCustomerRequireTheDepositortoTtheBankId { get; set; }

        public ushort BDLBanksCode { get; set; }

        public ulong LiabilityApprovalDocNamber { get; set; }

        public string LAccountNumber { get; set; }

        public string LDocType { get; set; }

        public string LDepositStatus { get; set; }

        public string LCurrencyCode { get; set; }

        public byte LAmountOnDeposit { get; set; }

        public decimal LAnnualInterestRate { get; set; }

        public decimal LTotolInterest { get; set; }

        public decimal LEndingBalans { get; set; }
    }
}

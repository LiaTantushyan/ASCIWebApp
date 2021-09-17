using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASCIWebApp.Models
{
	public partial class IACSBankCustomerGaranteengOfDeposit
	{
		public int IACSBankCustomerGaranteengOfDepositId { get; set; }

		public decimal GaranteengOfDeposit { get; set; }

		public decimal SumOfInterest { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASCIWebApp.Models
{
	public partial class IACSBankCustomer
	{
		public int IACSBankCustomerId { get; set; }
		public IACSBankCustomerDepositor Depositor { get; set; }
		
		public IACSBankCustomerRequireTheDepositortoTtheBank RequireTheDepositortoTtheBank { get; set; }

		public IACSBankCustomerGaranteengOfDeposit GaranteengOfDeposit { get; set; }
	}
}

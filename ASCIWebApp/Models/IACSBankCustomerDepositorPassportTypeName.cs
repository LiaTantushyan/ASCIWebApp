using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASCIWebApp.Models
{
	public partial class IACSBankCustomerDepositorPassportTypeName
	{
		public int IACSBankCustomerDepositorPassportTypeNameId { get; set; }
		public string PassType { get; set; }

		public string PassportNum { get; set; }

		public DateTime PassportDateOfIssue { get; set; }

		public DateTime PassportDateOfExpiry { get; set; }
	}
}

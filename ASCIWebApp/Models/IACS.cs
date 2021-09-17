using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASCIWebApp.Models
{
	[Keyless]
	public partial class IACS
	{
        public int IACSId { get; set; }
        public IACSDepositDocumentHeader DepositDocumentHeader { get; set; }

		public IACSBankCustomer BankCustomer { get; set; }
	}
}

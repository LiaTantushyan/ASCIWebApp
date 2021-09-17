using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASCIWebApp.Models
{
	public partial class IACSDepositDocumentHeader
	{
		public int IACSDepositDocumentHeaderId { get; set; }

		public ushort BanksCode { get; set; }

		public DateTime DocumentCreateDate { get; set; }
	}
}

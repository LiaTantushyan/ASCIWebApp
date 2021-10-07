using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ASCIWebApp.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ASCIWebApp.Data
{
	public class IACSDbContext : DbContext
	{
		public DbSet<IACS> IACS { get; set; }

		public DbSet<IACSBankCustomer> IACSBankCustomers { get; set; }

		public DbSet<IACSBankCustomerDepositor> IACSBankCustomerDepositors { get; set; }

		public DbSet<IACSBankCustomerDepositorPassportTypeName> IACSBankCustomersDepositorPasports { get; set; }

		public DbSet<IACSBankCustomerGaranteengOfDeposit> IACSBankCustomerGaranteengsofDeposits { get; set; }

		public DbSet<IACSBankCustomerRequireTheDepositortoTtheBank> IACSBankCustomersRequireTheDepositorToTheBank { get; set; }

		public DbSet<IACSDepositDocumentHeader> IACSDepositDocumentHeaders { get; set; }

		public IACSDbContext()		
		{

		}
		public IACSDbContext(DbContextOptions<IACSDbContext> options)
			: base(options)
		{

		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

        }		
	}
}

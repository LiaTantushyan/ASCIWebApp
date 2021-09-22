﻿// <auto-generated />
using System;
using ASCIWebApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ASCIWebApp.Migrations
{
    [DbContext(typeof(IACSDbContext))]
    [Migration("20210922075738_Initialize")]
    partial class Initialize
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ASCIWebApp.Models.IACS", b =>
                {
                    b.Property<int?>("BankCustomerIACSBankCustomerId")
                        .HasColumnType("int");

                    b.Property<int?>("DepositDocumentHeaderIACSDepositDocumentHeaderId")
                        .HasColumnType("int");

                    b.Property<int>("IACSId")
                        .HasColumnType("int");

                    b.HasIndex("BankCustomerIACSBankCustomerId");

                    b.HasIndex("DepositDocumentHeaderIACSDepositDocumentHeaderId");

                    b.ToTable("IACS");
                });

            modelBuilder.Entity("ASCIWebApp.Models.IACSBankCustomer", b =>
                {
                    b.Property<int>("IACSBankCustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DepositorIACSBankCustomerDepositorId")
                        .HasColumnType("int");

                    b.Property<int?>("GaranteengOfDepositIACSBankCustomerGaranteengOfDepositId")
                        .HasColumnType("int");

                    b.Property<int?>("RequireTheDepositortoTtheBankIACSBankCustomerRequireTheDepositortoTtheBankId")
                        .HasColumnType("int");

                    b.HasKey("IACSBankCustomerId");

                    b.HasIndex("DepositorIACSBankCustomerDepositorId");

                    b.HasIndex("GaranteengOfDepositIACSBankCustomerGaranteengOfDepositId");

                    b.HasIndex("RequireTheDepositortoTtheBankIACSBankCustomerRequireTheDepositortoTtheBankId");

                    b.ToTable("IACSBankCustomers");
                });

            modelBuilder.Entity("ASCIWebApp.Models.IACSBankCustomerDepositor", b =>
                {
                    b.Property<int>("IACSBankCustomerDepositorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LocationAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LocationCountry")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("LocationDistrictName")
                        .HasColumnType("bigint");

                    b.Property<byte>("NNumber")
                        .HasColumnType("tinyint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nationality")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PassportTypeNameIACSBankCustomerDepositorPassportTypeNameId")
                        .HasColumnType("int");

                    b.Property<string>("RegistrationAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegistrationCountry")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("RegistrationDistrictName")
                        .HasColumnType("bigint");

                    b.Property<string>("SecondName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sex")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("SocCardNum")
                        .HasColumnType("bigint");

                    b.Property<string>("SurName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IACSBankCustomerDepositorId");

                    b.HasIndex("PassportTypeNameIACSBankCustomerDepositorPassportTypeNameId");

                    b.ToTable("IACSBankCustomerDepositors");
                });

            modelBuilder.Entity("ASCIWebApp.Models.IACSBankCustomerDepositorPassportTypeName", b =>
                {
                    b.Property<int>("IACSBankCustomerDepositorPassportTypeNameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PassType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PassportDateOfExpiry")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PassportDateOfIssue")
                        .HasColumnType("datetime2");

                    b.Property<string>("PassportNum")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IACSBankCustomerDepositorPassportTypeNameId");

                    b.ToTable("IACSBankCustomersDepositorPasports");
                });

            modelBuilder.Entity("ASCIWebApp.Models.IACSBankCustomerGaranteengOfDeposit", b =>
                {
                    b.Property<int>("IACSBankCustomerGaranteengOfDepositId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("GaranteengOfDeposit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("SumOfInterest")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("IACSBankCustomerGaranteengOfDepositId");

                    b.ToTable("IACSBankCustomerGaranteengsofDeposits");
                });

            modelBuilder.Entity("ASCIWebApp.Models.IACSBankCustomerRequireTheDepositortoTtheBank", b =>
                {
                    b.Property<int>("IACSBankCustomerRequireTheDepositortoTtheBankId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BDLBanksCode")
                        .HasColumnType("int");

                    b.Property<decimal>("LAccountNumber")
                        .HasColumnType("decimal(20,0)");

                    b.Property<byte>("LAmountOnDeposit")
                        .HasColumnType("tinyint");

                    b.Property<decimal>("LAnnualInterestRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("LCurrencyCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LDepositStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LDocType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("LEndingBalans")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("LTotolInterest")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("LiabilityApprovalDocNamber")
                        .HasColumnType("decimal(20,0)");

                    b.HasKey("IACSBankCustomerRequireTheDepositortoTtheBankId");

                    b.ToTable("IACSBankCustomersRequireTheDepositorToTheBank");
                });

            modelBuilder.Entity("ASCIWebApp.Models.IACSDepositDocumentHeader", b =>
                {
                    b.Property<int>("IACSDepositDocumentHeaderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BanksCode")
                        .HasColumnType("int");

                    b.Property<DateTime>("DocumentCreateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("IACSDepositDocumentHeaderId");

                    b.ToTable("IACSDepositDocumentHeaders");
                });

            modelBuilder.Entity("ASCIWebApp.Models.IACS", b =>
                {
                    b.HasOne("ASCIWebApp.Models.IACSBankCustomer", "BankCustomer")
                        .WithMany()
                        .HasForeignKey("BankCustomerIACSBankCustomerId");

                    b.HasOne("ASCIWebApp.Models.IACSDepositDocumentHeader", "DepositDocumentHeader")
                        .WithMany()
                        .HasForeignKey("DepositDocumentHeaderIACSDepositDocumentHeaderId");

                    b.Navigation("BankCustomer");

                    b.Navigation("DepositDocumentHeader");
                });

            modelBuilder.Entity("ASCIWebApp.Models.IACSBankCustomer", b =>
                {
                    b.HasOne("ASCIWebApp.Models.IACSBankCustomerDepositor", "Depositor")
                        .WithMany()
                        .HasForeignKey("DepositorIACSBankCustomerDepositorId");

                    b.HasOne("ASCIWebApp.Models.IACSBankCustomerGaranteengOfDeposit", "GaranteengOfDeposit")
                        .WithMany()
                        .HasForeignKey("GaranteengOfDepositIACSBankCustomerGaranteengOfDepositId");

                    b.HasOne("ASCIWebApp.Models.IACSBankCustomerRequireTheDepositortoTtheBank", "RequireTheDepositortoTtheBank")
                        .WithMany()
                        .HasForeignKey("RequireTheDepositortoTtheBankIACSBankCustomerRequireTheDepositortoTtheBankId");

                    b.Navigation("Depositor");

                    b.Navigation("GaranteengOfDeposit");

                    b.Navigation("RequireTheDepositortoTtheBank");
                });

            modelBuilder.Entity("ASCIWebApp.Models.IACSBankCustomerDepositor", b =>
                {
                    b.HasOne("ASCIWebApp.Models.IACSBankCustomerDepositorPassportTypeName", "PassportTypeName")
                        .WithMany()
                        .HasForeignKey("PassportTypeNameIACSBankCustomerDepositorPassportTypeNameId");

                    b.Navigation("PassportTypeName");
                });
#pragma warning restore 612, 618
        }
    }
}

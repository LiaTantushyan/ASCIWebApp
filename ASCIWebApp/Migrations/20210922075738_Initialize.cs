using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ASCIWebApp.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IACSBankCustomerGaranteengsofDeposits",
                columns: table => new
                {
                    IACSBankCustomerGaranteengOfDepositId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GaranteengOfDeposit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SumOfInterest = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IACSBankCustomerGaranteengsofDeposits", x => x.IACSBankCustomerGaranteengOfDepositId);
                });

            migrationBuilder.CreateTable(
                name: "IACSBankCustomersDepositorPasports",
                columns: table => new
                {
                    IACSBankCustomerDepositorPassportTypeNameId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PassType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassportNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassportDateOfIssue = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PassportDateOfExpiry = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IACSBankCustomersDepositorPasports", x => x.IACSBankCustomerDepositorPassportTypeNameId);
                });

            migrationBuilder.CreateTable(
                name: "IACSBankCustomersRequireTheDepositorToTheBank",
                columns: table => new
                {
                    IACSBankCustomerRequireTheDepositortoTtheBankId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BDLBanksCode = table.Column<int>(type: "int", nullable: false),
                    LiabilityApprovalDocNamber = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    LAccountNumber = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    LDocType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LDepositStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LCurrencyCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LAmountOnDeposit = table.Column<byte>(type: "tinyint", nullable: false),
                    LAnnualInterestRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LTotolInterest = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LEndingBalans = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IACSBankCustomersRequireTheDepositorToTheBank", x => x.IACSBankCustomerRequireTheDepositortoTtheBankId);
                });

            migrationBuilder.CreateTable(
                name: "IACSDepositDocumentHeaders",
                columns: table => new
                {
                    IACSDepositDocumentHeaderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BanksCode = table.Column<int>(type: "int", nullable: false),
                    DocumentCreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IACSDepositDocumentHeaders", x => x.IACSDepositDocumentHeaderId);
                });

            migrationBuilder.CreateTable(
                name: "IACSBankCustomerDepositors",
                columns: table => new
                {
                    IACSBankCustomerDepositorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NNumber = table.Column<byte>(type: "tinyint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SurName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SocCardNum = table.Column<long>(type: "bigint", nullable: false),
                    PassportTypeNameIACSBankCustomerDepositorPassportTypeNameId = table.Column<int>(type: "int", nullable: true),
                    RegistrationCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationDistrictName = table.Column<long>(type: "bigint", nullable: false),
                    RegistrationAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationDistrictName = table.Column<long>(type: "bigint", nullable: false),
                    LocationAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IACSBankCustomerDepositors", x => x.IACSBankCustomerDepositorId);
                    table.ForeignKey(
                        name: "FK_IACSBankCustomerDepositors_IACSBankCustomersDepositorPasports_PassportTypeNameIACSBankCustomerDepositorPassportTypeNameId",
                        column: x => x.PassportTypeNameIACSBankCustomerDepositorPassportTypeNameId,
                        principalTable: "IACSBankCustomersDepositorPasports",
                        principalColumn: "IACSBankCustomerDepositorPassportTypeNameId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IACSBankCustomers",
                columns: table => new
                {
                    IACSBankCustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepositorIACSBankCustomerDepositorId = table.Column<int>(type: "int", nullable: true),
                    RequireTheDepositortoTtheBankIACSBankCustomerRequireTheDepositortoTtheBankId = table.Column<int>(type: "int", nullable: true),
                    GaranteengOfDepositIACSBankCustomerGaranteengOfDepositId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IACSBankCustomers", x => x.IACSBankCustomerId);
                    table.ForeignKey(
                        name: "FK_IACSBankCustomers_IACSBankCustomerDepositors_DepositorIACSBankCustomerDepositorId",
                        column: x => x.DepositorIACSBankCustomerDepositorId,
                        principalTable: "IACSBankCustomerDepositors",
                        principalColumn: "IACSBankCustomerDepositorId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IACSBankCustomers_IACSBankCustomerGaranteengsofDeposits_GaranteengOfDepositIACSBankCustomerGaranteengOfDepositId",
                        column: x => x.GaranteengOfDepositIACSBankCustomerGaranteengOfDepositId,
                        principalTable: "IACSBankCustomerGaranteengsofDeposits",
                        principalColumn: "IACSBankCustomerGaranteengOfDepositId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IACSBankCustomers_IACSBankCustomersRequireTheDepositorToTheBank_RequireTheDepositortoTtheBankIACSBankCustomerRequireTheDepos~",
                        column: x => x.RequireTheDepositortoTtheBankIACSBankCustomerRequireTheDepositortoTtheBankId,
                        principalTable: "IACSBankCustomersRequireTheDepositorToTheBank",
                        principalColumn: "IACSBankCustomerRequireTheDepositortoTtheBankId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IACS",
                columns: table => new
                {
                    IACSId = table.Column<int>(type: "int", nullable: false),
                    DepositDocumentHeaderIACSDepositDocumentHeaderId = table.Column<int>(type: "int", nullable: true),
                    BankCustomerIACSBankCustomerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_IACS_IACSBankCustomers_BankCustomerIACSBankCustomerId",
                        column: x => x.BankCustomerIACSBankCustomerId,
                        principalTable: "IACSBankCustomers",
                        principalColumn: "IACSBankCustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IACS_IACSDepositDocumentHeaders_DepositDocumentHeaderIACSDepositDocumentHeaderId",
                        column: x => x.DepositDocumentHeaderIACSDepositDocumentHeaderId,
                        principalTable: "IACSDepositDocumentHeaders",
                        principalColumn: "IACSDepositDocumentHeaderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IACS_BankCustomerIACSBankCustomerId",
                table: "IACS",
                column: "BankCustomerIACSBankCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_IACS_DepositDocumentHeaderIACSDepositDocumentHeaderId",
                table: "IACS",
                column: "DepositDocumentHeaderIACSDepositDocumentHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_IACSBankCustomerDepositors_PassportTypeNameIACSBankCustomerDepositorPassportTypeNameId",
                table: "IACSBankCustomerDepositors",
                column: "PassportTypeNameIACSBankCustomerDepositorPassportTypeNameId");

            migrationBuilder.CreateIndex(
                name: "IX_IACSBankCustomers_DepositorIACSBankCustomerDepositorId",
                table: "IACSBankCustomers",
                column: "DepositorIACSBankCustomerDepositorId");

            migrationBuilder.CreateIndex(
                name: "IX_IACSBankCustomers_GaranteengOfDepositIACSBankCustomerGaranteengOfDepositId",
                table: "IACSBankCustomers",
                column: "GaranteengOfDepositIACSBankCustomerGaranteengOfDepositId");

            migrationBuilder.CreateIndex(
                name: "IX_IACSBankCustomers_RequireTheDepositortoTtheBankIACSBankCustomerRequireTheDepositortoTtheBankId",
                table: "IACSBankCustomers",
                column: "RequireTheDepositortoTtheBankIACSBankCustomerRequireTheDepositortoTtheBankId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IACS");

            migrationBuilder.DropTable(
                name: "IACSBankCustomers");

            migrationBuilder.DropTable(
                name: "IACSDepositDocumentHeaders");

            migrationBuilder.DropTable(
                name: "IACSBankCustomerDepositors");

            migrationBuilder.DropTable(
                name: "IACSBankCustomerGaranteengsofDeposits");

            migrationBuilder.DropTable(
                name: "IACSBankCustomersRequireTheDepositorToTheBank");

            migrationBuilder.DropTable(
                name: "IACSBankCustomersDepositorPasports");
        }
    }
}

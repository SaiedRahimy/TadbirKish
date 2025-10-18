using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TadbirKish.DataReception.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabaseAndEnities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Data");

            migrationBuilder.CreateTable(
                name: "Coverages",
                schema: "Data",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Min = table.Column<long>(type: "bigint", nullable: false),
                    Max = table.Column<long>(type: "bigint", nullable: false),
                    Coefficient = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coverages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RequestCoverages",
                schema: "Data",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TotalNetPremium = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestCoverages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RequestCoverageDetails",
                schema: "Data",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestCoverageId = table.Column<int>(type: "int", nullable: false),
                    CoverageId = table.Column<int>(type: "int", nullable: false),
                    GrossPremium = table.Column<long>(type: "bigint", nullable: false),
                    NetPremium = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestCoverageDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestCoverageDetails_Coverages_CoverageId",
                        column: x => x.CoverageId,
                        principalSchema: "Data",
                        principalTable: "Coverages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestCoverageDetails_RequestCoverages_RequestCoverageId",
                        column: x => x.RequestCoverageId,
                        principalSchema: "Data",
                        principalTable: "RequestCoverages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "Data",
                table: "Coverages",
                columns: new[] { "Id", "Coefficient", "CreatedBy", "LastModifiedBy", "Max", "Min", "Name" },
                values: new object[,]
                {
                    { 1, 0.0051999999999999998, null, null, 500000000L, 5000L, "جراحی" },
                    { 2, 0.0041999999999999997, null, null, 400000000L, 4000L, "دندانپزشکی" },
                    { 3, 0.0050000000000000001, null, null, 200000000L, 2000L, "بستری" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Coverages_Name",
                schema: "Data",
                table: "Coverages",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RequestCoverageDetails_CoverageId",
                schema: "Data",
                table: "RequestCoverageDetails",
                column: "CoverageId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestCoverageDetails_RequestCoverageId_CoverageId",
                schema: "Data",
                table: "RequestCoverageDetails",
                columns: new[] { "RequestCoverageId", "CoverageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RequestCoverages_Name",
                schema: "Data",
                table: "RequestCoverages",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestCoverageDetails",
                schema: "Data");

            migrationBuilder.DropTable(
                name: "Coverages",
                schema: "Data");

            migrationBuilder.DropTable(
                name: "RequestCoverages",
                schema: "Data");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentJobHelperSystem.Data.Migrations
{
    public partial class FixCreatedOnValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "JobAds",
                keyColumn: "Id",
                keyValue: new Guid("0867a542-adf6-45dd-85e5-9ca001ba96c8"));

            migrationBuilder.AlterColumn<string>(
                name: "TypeOfEmployment",
                table: "JobAds",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "JobAds",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 8, 3, 16, 46, 37, 888, DateTimeKind.Utc).AddTicks(5417));

            migrationBuilder.AlterColumn<string>(
                name: "CompanyNumber",
                table: "Employers",
                type: "nvarchar(9)",
                maxLength: 9,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.InsertData(
                table: "JobAds",
                columns: new[] { "Id", "CategoryId", "CityOfWork", "Description", "Email", "EmployerId", "ForeignLanguage", "HomeOffice", "LogoUrl", "OffDaysCount", "PhoneNumber", "Salary", "Title", "TypeOfEmployment", "WorkerId" },
                values: new object[] { new Guid("bb3804f2-bcc6-441c-b055-60fde7ceef91"), 1001, "Sofia", "fasdgsgdfgdfg", "employer@gmail.com", new Guid("53f74e02-0c81-45b2-b443-912f2a37d410"), "English", false, "sdgsdfgdfgdgh", 15, "0876545222", 2500.00m, "nz", "Permanent", new Guid("1461a7de-14e2-4031-a2d9-5edc19df3b8e") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "JobAds",
                keyColumn: "Id",
                keyValue: new Guid("bb3804f2-bcc6-441c-b055-60fde7ceef91"));

            migrationBuilder.AlterColumn<string>(
                name: "TypeOfEmployment",
                table: "JobAds",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "JobAds",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 3, 16, 46, 37, 888, DateTimeKind.Utc).AddTicks(5417),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyNumber",
                table: "Employers",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(9)",
                oldMaxLength: 9);

            migrationBuilder.InsertData(
                table: "JobAds",
                columns: new[] { "Id", "CategoryId", "CityOfWork", "CreatedOn", "Description", "Email", "EmployerId", "ForeignLanguage", "HomeOffice", "LogoUrl", "OffDaysCount", "PhoneNumber", "Salary", "Title", "TypeOfEmployment", "WorkerId" },
                values: new object[] { new Guid("0867a542-adf6-45dd-85e5-9ca001ba96c8"), 1001, "Sofia", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "fasdgsgdfgdfg", "employer@gmail.com", new Guid("53f74e02-0c81-45b2-b443-912f2a37d410"), "English", false, "sdgsdfgdfgdgh", 15, "0876545222", 2500.00m, "nz", "Permanent", new Guid("1461a7de-14e2-4031-a2d9-5edc19df3b8e") });
        }
    }
}

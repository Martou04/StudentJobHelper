using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentJobHelperSystem.Data.Migrations
{
    public partial class AddCreatedOnColumnToJobAds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "JobAds",
                keyColumn: "Id",
                keyValue: new Guid("d981d528-0a80-4404-b5b4-b33b849550fa"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "JobAds",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 3, 16, 46, 37, 888, DateTimeKind.Utc).AddTicks(5417));

            migrationBuilder.InsertData(
                table: "JobAds",
                columns: new[] { "Id", "CategoryId", "CityOfWork", "Description", "Email", "EmployerId", "ForeignLanguage", "HomeOffice", "LogoUrl", "OffDaysCount", "PhoneNumber", "Salary", "Title", "TypeOfEmployment", "WorkerId" },
                values: new object[] { new Guid("0867a542-adf6-45dd-85e5-9ca001ba96c8"), 1001, "Sofia", "fasdgsgdfgdfg", "employer@gmail.com", new Guid("53f74e02-0c81-45b2-b443-912f2a37d410"), "English", false, "sdgsdfgdfgdgh", 15, "0876545222", 2500.00m, "nz", "Permanent", new Guid("1461a7de-14e2-4031-a2d9-5edc19df3b8e") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "JobAds",
                keyColumn: "Id",
                keyValue: new Guid("0867a542-adf6-45dd-85e5-9ca001ba96c8"));

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "JobAds");

            migrationBuilder.InsertData(
                table: "JobAds",
                columns: new[] { "Id", "CategoryId", "CityOfWork", "Description", "Email", "EmployerId", "ForeignLanguage", "HomeOffice", "LogoUrl", "OffDaysCount", "PhoneNumber", "Salary", "Title", "TypeOfEmployment", "WorkerId" },
                values: new object[] { new Guid("d981d528-0a80-4404-b5b4-b33b849550fa"), 1001, "Sofia", "fasdgsgdfgdfg", "employer@gmail.com", new Guid("53f74e02-0c81-45b2-b443-912f2a37d410"), "English", false, "sdgsdfgdfgdgh", 15, "0876545222", 2500.00m, "nz", "Permanent", new Guid("1461a7de-14e2-4031-a2d9-5edc19df3b8e") });
        }
    }
}

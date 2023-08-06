using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentJobHelperSystem.Data.Migrations
{
    public partial class AddIsActiveColumnForSoftDeletingJobAds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "JobAds",
                keyColumn: "Id",
                keyValue: new Guid("bb3804f2-bcc6-441c-b055-60fde7ceef91"));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "JobAds",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.InsertData(
                table: "JobAds",
                columns: new[] { "Id", "CategoryId", "CityOfWork", "Description", "Email", "EmployerId", "ForeignLanguage", "HomeOffice", "LogoUrl", "OffDaysCount", "PhoneNumber", "Salary", "Title", "TypeOfEmployment", "WorkerId" },
                values: new object[] { new Guid("5bfa4e6f-41f8-47ad-872d-9227196c5be1"), 1001, "Sofia", "fasdgsgdfgdfg", "employer@gmail.com", new Guid("53f74e02-0c81-45b2-b443-912f2a37d410"), "English", false, "sdgsdfgdfgdgh", 15, "0876545222", 2500.00m, "nz", "Permanent", new Guid("1461a7de-14e2-4031-a2d9-5edc19df3b8e") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "JobAds",
                keyColumn: "Id",
                keyValue: new Guid("5bfa4e6f-41f8-47ad-872d-9227196c5be1"));

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "JobAds");

            migrationBuilder.InsertData(
                table: "JobAds",
                columns: new[] { "Id", "CategoryId", "CityOfWork", "CreatedOn", "Description", "Email", "EmployerId", "ForeignLanguage", "HomeOffice", "LogoUrl", "OffDaysCount", "PhoneNumber", "Salary", "Title", "TypeOfEmployment", "WorkerId" },
                values: new object[] { new Guid("bb3804f2-bcc6-441c-b055-60fde7ceef91"), 1001, "Sofia", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "fasdgsgdfgdfg", "employer@gmail.com", new Guid("53f74e02-0c81-45b2-b443-912f2a37d410"), "English", false, "sdgsdfgdfgdgh", 15, "0876545222", 2500.00m, "nz", "Permanent", new Guid("1461a7de-14e2-4031-a2d9-5edc19df3b8e") });
        }
    }
}

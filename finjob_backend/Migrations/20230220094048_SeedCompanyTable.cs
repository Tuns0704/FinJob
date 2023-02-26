using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace finjob_backend.Migrations
{
    /// <inheritdoc />
    public partial class SeedCompanyTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "CreatedDate", "Description", "ImageURL", "Location", "Name", "Scale", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 2, 20, 16, 40, 48, 341, DateTimeKind.Local).AddTicks(7324), "None", "https://itviec.com/rails/active_storage/representations/proxy/eyJfcmFpbHMiOnsibWVzc2FnZSI6IkJBaHBBK1lxREE9PSIsImV4cCI6bnVsbCwicHVyIjoiYmxvYl9pZCJ9fQ==--d01c8dc918133b1b89705dc00ef4f490b78d20e3/eyJfcmFpbHMiOnsibWVzc2FnZSI6IkJBaDdCem9MWm05eWJXRjBTU0lJY0c1bkJqb0dSVlE2RkhKbGMybDZaVjkwYjE5c2FXMXBkRnNIYVFJc0FXa0NMQUU9IiwiZXhwIjpudWxsLCJwdXIiOiJ2YXJpYXRpb24ifX0=--492f60b9aac6e8159e50e72bb289c5feb47a79d4/mgm-technology-partners-vietnam-logo.png", "Da Nang", "MGM", "50 - 150 Employees", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2023, 2, 20, 16, 40, 48, 341, DateTimeKind.Local).AddTicks(7334), "None", "https://dsa.org.vn/wp-content/uploads/2018/01/Agility.jpg", "Da Nang", "Agility", "50 - 100 Employees", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2023, 2, 20, 16, 40, 48, 341, DateTimeKind.Local).AddTicks(7335), "None", "https://images.glints.com/unsafe/glints-dashboard.s3.amazonaws.com/company-logo/2c47b1dbc75d6d9184be768c39fb9ac7.png", "Da Nang", "Orient", "20 - 50 Employees", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2023, 2, 20, 16, 40, 48, 341, DateTimeKind.Local).AddTicks(7335), "None", "https://doanhnghiep.quocgiakhoinghiep.vn/wp-content/uploads/2020/08/images-7.png", "Da Nang", "KMS", "40 - 100 Employees", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new DateTime(2023, 2, 20, 16, 40, 48, 341, DateTimeKind.Local).AddTicks(7338), "None", "https://static.topcv.vn/company_logos/bUsiw4xpTeqvyGecqSrVW3Zuq0dayRwK_1656588043____83f8efe4285d433505271ef261179f41.jpeg", "Da Nang", "NFQ", "20 - 80 Employees", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}

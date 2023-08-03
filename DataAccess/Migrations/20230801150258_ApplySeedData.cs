using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ApplySeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Products",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedDate", "Description", "ModifiedDate", "Price", "Title", "Type" },
                values: new object[,]
                {
                    { new Guid("249c7e5c-caa7-4786-8865-7558cf439cbc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "7200 RPM 256MB Cache SATA 6.0Gb For Enterprise Storage", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 141.98m, "Seagate Exos X20 ST20000NM007D ", 4 },
                    { new Guid("4f70c07a-0f46-481f-a037-4c73b766b16f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Intel 7 Alder Lake Processor Base Power: 65W Maximum Turbo Power: 117W 18MB L3 Cache 7.5MB L2 Cache Windows 11 Supported Intel Laminar RM1 CPU Cooler", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 50.00m, "Intel Core i5-12400F", 2 },
                    { new Guid("8d9d8ab8-82c6-452f-b804-ecccb7f43163"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Intel LGA 1700 socket: Ready for 13th Gen Intel Core, and 12th Gen Intel Core, Pentium Gold, and Celeron® processors.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 137.73m, "ASUS ROG STRIX Z790-H Gaming", 1 },
                    { new Guid("92e03aa9-ab82-47bc-ab26-dc6bd5dffd68"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "8GB 128-Bit GDDR6", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1357m, "PELADN AMD Radeon RX6650XT 8GB GDDR6 PCI Express 4.0 Video Card", 5 },
                    { new Guid("af6b1042-e017-4b60-963e-58001220ebf6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "NVMe M.2 PCIe Gen4 x4 Interface. PCIe 4.0 Compliant / NVMe 1.3 Compliant.\r\nPower Management Support for APST / ASPM / L1.2.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 417.14m, "SABRENT 500GB Rocket Nvme PCIe 4.0 M.2 2280", 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("249c7e5c-caa7-4786-8865-7558cf439cbc"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("4f70c07a-0f46-481f-a037-4c73b766b16f"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("8d9d8ab8-82c6-452f-b804-ecccb7f43163"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("92e03aa9-ab82-47bc-ab26-dc6bd5dffd68"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("af6b1042-e017-4b60-963e-58001220ebf6"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Products",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }
    }
}

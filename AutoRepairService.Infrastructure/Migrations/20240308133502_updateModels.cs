using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoRepairService.Infrastructure.Migrations
{
    public partial class updateModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_AspNetUsers_OwnerId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairOffer_Offers_OfferId",
                table: "RepairOffer");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairOffer_Repairs_RepairId",
                table: "RepairOffer");

            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_AspNetUsers_OwnerId",
                table: "Repairs");

            migrationBuilder.AddColumn<string>(
                name: "OwnerName",
                table: "Repairs",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3a0002b7-e75a-41b9-9df8-a5c63cb6c437", "AQAAAAEAACcQAAAAEGfyylDNhTEi5yVGrKS57AKm9IW2equiCO1LaqO5oL4zeXEaXHzvBA27fDaX7u6k+w==", "9c0232ca-eb19-4d43-badd-a58ad6653a34" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d6b3ac1f-4fc8-d726-83d9-6d5800ce591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8e38dea3-bcbf-4f57-8974-a57d2acaba3a", "AQAAAAEAACcQAAAAEOTSTELe8nflLDvOcOz2GME02WBWBamjuvUwzTwkIA2af6E/HwMqarXA2x6AdxFaew==", "ad760639-b6ed-4ffc-b8cd-679c46b4f99a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0e45bed9-88d4-4b17-acb5-5fc31c78f23c", "AQAAAAEAACcQAAAAEKtRPvjTfwDNgYoN2NeC37+Cjt/qcNx3u5jbAKe1V5hR/E+kY+XyD5OeGejQm8wPDw==", "a008c50e-3a17-45a7-ad2e-a4fe81f59cf1" });

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_AspNetUsers_OwnerId",
                table: "Cars",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RepairOffer_Offers_OfferId",
                table: "RepairOffer",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RepairOffer_Repairs_RepairId",
                table: "RepairOffer",
                column: "RepairId",
                principalTable: "Repairs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Repairs_AspNetUsers_OwnerId",
                table: "Repairs",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_AspNetUsers_OwnerId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairOffer_Offers_OfferId",
                table: "RepairOffer");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairOffer_Repairs_RepairId",
                table: "RepairOffer");

            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_AspNetUsers_OwnerId",
                table: "Repairs");

            migrationBuilder.DropColumn(
                name: "OwnerName",
                table: "Repairs");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "784e3323-778d-49e5-b060-0b2b4ab09056", "AQAAAAEAACcQAAAAEKzsHlq4P29YDBxdKFSTG6ITnvmcLPTLEzs6gWL20bGhxjkufZWhFtQSkW4QvzxJiw==", "3da85e19-56db-460c-80da-dc1671ea3ab0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d6b3ac1f-4fc8-d726-83d9-6d5800ce591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "706e9c0e-8dc0-4a8c-9832-904d124b7d39", "AQAAAAEAACcQAAAAELc83umaBCNGzIt/SvPZZDt/K05JiRxC2sHGimIskr1sYlC0pXBWeRj5s557fnXohQ==", "3cd21c83-2bf3-4e75-bab4-1f7bcfd6b1e9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2fd7ed5b-2b5a-422a-9c37-bb5ba659d62f", "AQAAAAEAACcQAAAAEPomkGl4/M7palNiCgOOhmye9Sw62cUWZzwp2PMeRLSu1JMSb2i/jiPTKKPoJvs+KQ==", "0847398d-32fe-445a-8786-1a9d6939ad64" });

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_AspNetUsers_OwnerId",
                table: "Cars",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RepairOffer_Offers_OfferId",
                table: "RepairOffer",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RepairOffer_Repairs_RepairId",
                table: "RepairOffer",
                column: "RepairId",
                principalTable: "Repairs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Repairs_AspNetUsers_OwnerId",
                table: "Repairs",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

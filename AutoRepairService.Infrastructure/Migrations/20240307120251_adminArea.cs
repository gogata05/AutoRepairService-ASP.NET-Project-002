using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoRepairService.Infrastructure.Migrations
{
    public partial class adminArea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_AspNetUsers_OwnerId",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Repairs_RepairId",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairOffer_Offers_OfferId",
                table: "RepairOffer");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairOffer_Repairs_RepairId",
                table: "RepairOffer");

            migrationBuilder.DropIndex(
                name: "IX_Offers_OwnerId",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Offers_RepairId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "RepairId",
                table: "Offers");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Offers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<bool>(
                name: "IsAccepted",
                table: "Offers",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Offers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5f90b006-ea02-4227-9bd1-5d6d84959200", "AQAAAAEAACcQAAAAEMH1P0Cazyam3jrxAhMsR/9BWa6H1uzxgreDpC1WCElpWYvBcCL4QNpshPmxNuU+TA==", "be5c9114-3db3-4cb2-8a12-b1f52dd6b1f1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d6b3ac1f-4fc8-d726-83d9-6d5800ce591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8d66a36f-2a00-44ec-ac9e-1070dcc01856", "AQAAAAEAACcQAAAAECTvMP+rrlWxmM6Z+Br2RExyVh8RBmmxF94MceaaPcRdRFrepairtUJjPq5pMYQBIJyOg==", "45070994-c606-4c0d-9414-1236239323f9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1da0d1dd-2b03-4d63-b13b-74e7119775c2", "AQAAAAEAACcQAAAAEMexn5ziwoAFaFEArVAIdkxjODjdzlJ1rAS4g6EkhZJzFwKWhl0XWMWOuHDBDvd4EA==", "e54444df-fa83-4367-a6fb-8c43bde95b3f" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_OwnerId",
                table: "AspNetUsers",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Offers_OwnerId",
                table: "AspNetUsers",
                column: "OwnerId",
                principalTable: "Offers",
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Offers_OwnerId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairOffer_Offers_OfferId",
                table: "RepairOffer");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairOffer_Repairs_RepairId",
                table: "RepairOffer");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_OwnerId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsAccepted",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Offers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "RepairId",
                table: "Offers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "138e39fa-4e2a-4902-a16f-5d8a03523fc9", "AQAAAAEAACcQAAAAEJDtKpoSrfCPoN0JEW+ONF3DI/Cc90i6oZXpJ3JpYlM8F7GDff56zm4c0lmZSvmkgA==", "e8024ea0-e461-4c78-89f6-70cac77e0112" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d6b3ac1f-4fc8-d726-83d9-6d5800ce591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "75bb2500-6aa3-454c-9bf3-a3497657264d", "AQAAAAEAACcQAAAAEBfHXnd5fZJoaN3bg1joUggfGm+S7FiHKsvgm2s5mCjS616t8D6KrXYrKoG4GxtKxA==", "4c64db40-16b2-4534-b2af-7c8fecc4931e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "98001ff7-a436-46e1-8560-c882d5082f2b", "AQAAAAEAACcQAAAAEAeLiMIu3XdnfUfnuLQFnKJU3k3toin0hz7ltJ7CK6KjByyW83t3zEO9ZDeeDEvXpw==", "70c2a6e8-4a34-48de-b752-4ea69b1da7dc" });

            migrationBuilder.CreateIndex(
                name: "IX_Offers_OwnerId",
                table: "Offers",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_RepairId",
                table: "Offers",
                column: "RepairId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_AspNetUsers_OwnerId",
                table: "Offers",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Repairs_RepairId",
                table: "Offers",
                column: "RepairId",
                principalTable: "Repairs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
        }
    }
}

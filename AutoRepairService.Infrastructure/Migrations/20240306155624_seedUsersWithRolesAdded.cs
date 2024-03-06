using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoRepairService.Infrastructure.Migrations
{
    public partial class seedUsersWithRolesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_AspNetUsers_OwnerId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_AspNetUsers_OwnerId",
                table: "Repairs");

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RepairId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offers_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Offers_Repairs_RepairId",
                        column: x => x.RepairId,
                        principalTable: "Repairs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RepairOffer",
                columns: table => new
                {
                    RepairId = table.Column<int>(type: "int", nullable: false),
                    OfferId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairOffer", x => new { x.RepairId, x.OfferId });
                    table.ForeignKey(
                        name: "FK_RepairOffer_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RepairOffer_Repairs_RepairId",
                        column: x => x.RepairId,
                        principalTable: "Repairs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1e62f853-4a41-4652-b9a9-8e8b236e24c7", "1fa54d33-a8ba-47b1-be64-02d6d4836152", "Administrator", "ADMINISTRATOR" },
                    { "5d937746-9833-4886-83d1-3c125ad5294c", "80196750-3beb-42a1-b321-0aa9c9f41275", "Customer", "CUSTOMER" },
                    { "c8a8cf93-46b1-4e79-871a-1f4742a0db83", "292f8d0e-c368-4cf1-bb0a-b8cda6096ea9", "Mechanic", "MECHANIC" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsMechanic", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e", 0, "138e39fa-4e2a-4902-a16f-5d8a03523fc9", "customer@mail.com", false, false, false, null, "CUSTOMER@MAIL.COM", "CUSTOMER", "AQAAAAEAACcQAAAAEJDtKpoSrfCPoN0JEW+ONF3DI/Cc90i6oZXpJ3JpYlM8F7GDff56zm4c0lmZSvmkgA==", null, false, "e8024ea0-e461-4c78-89f6-70cac77e0112", false, "customer" },
                    { "d6b3ac1f-4fc8-d726-83d9-6d5800ce591e", 0, "75bb2500-6aa3-454c-9bf3-a3497657264d", "admin@mail.com", false, false, false, null, "ADMIN@MAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEBfHXnd5fZJoaN3bg1joUggfGm+S7FiHKsvgm2s5mCjS616t8D6KrXYrKoG4GxtKxA==", null, false, "4c64db40-16b2-4534-b2af-7c8fecc4931e", false, "admin" },
                    { "dea12856-c198-4129-b3f3-b893d8395082", 0, "98001ff7-a436-46e1-8560-c882d5082f2b", "mechanic@mail.com", false, false, false, null, "MECHANIC@MAIL.COM", "MECHANIC", "AQAAAAEAACcQAAAAEAeLiMIu3XdnfUfnuLQFnKJU3k3toin0hz7ltJ7CK6KjByyW83t3zEO9ZDeeDEvXpw==", null, false, "70c2a6e8-4a34-48de-b752-4ea69b1da7dc", false, "mechanic" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "5d937746-9833-4886-83d1-3c125ad5294c", "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1e62f853-4a41-4652-b9a9-8e8b236e24c7", "d6b3ac1f-4fc8-d726-83d9-6d5800ce591e" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "c8a8cf93-46b1-4e79-871a-1f4742a0db83", "dea12856-c198-4129-b3f3-b893d8395082" });

            migrationBuilder.CreateIndex(
                name: "IX_Offers_OwnerId",
                table: "Offers",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_RepairId",
                table: "Offers",
                column: "RepairId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairOffer_OfferId",
                table: "RepairOffer",
                column: "OfferId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_AspNetUsers_OwnerId",
                table: "Cars",
                column: "OwnerId",
                principalTable: "AspNetUsers",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_AspNetUsers_OwnerId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_AspNetUsers_OwnerId",
                table: "Repairs");

            migrationBuilder.DropTable(
                name: "RepairOffer");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "5d937746-9833-4886-83d1-3c125ad5294c", "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1e62f853-4a41-4652-b9a9-8e8b236e24c7", "d6b3ac1f-4fc8-d726-83d9-6d5800ce591e" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "c8a8cf93-46b1-4e79-871a-1f4742a0db83", "dea12856-c198-4129-b3f3-b893d8395082" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1e62f853-4a41-4652-b9a9-8e8b236e24c7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5d937746-9833-4886-83d1-3c125ad5294c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c8a8cf93-46b1-4e79-871a-1f4742a0db83");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d6b3ac1f-4fc8-d726-83d9-6d5800ce591e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_AspNetUsers_OwnerId",
                table: "Cars",
                column: "OwnerId",
                principalTable: "AspNetUsers",
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
    }
}

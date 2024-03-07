using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoRepairService.Infrastructure.Migrations
{
    public partial class layoutUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                values: new object[] { "8d66a36f-2a00-44ec-ac9e-1070dcc01856", "AQAAAAEAACcQAAAAECTvMP+rrlWxmM6Z+Br2RExyVh8RBmmxF94MceaaPcRdRFjObtUJjPq5pMYQBIJyOg==", "45070994-c606-4c0d-9414-1236239323f9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1da0d1dd-2b03-4d63-b13b-74e7119775c2", "AQAAAAEAACcQAAAAEMexn5ziwoAFaFEArVAIdkxjODjdzlJ1rAS4g6EkhZJzFwKWhl0XWMWOuHDBDvd4EA==", "e54444df-fa83-4367-a6fb-8c43bde95b3f" });
        }
    }
}

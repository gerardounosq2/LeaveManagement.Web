using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagement.Web.Data.Migrations
{
    public partial class AddPeriodToLeaveAllocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Period",
                table: "LeaveAllocations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "24bf64f3-b8e7-4e65-8033-38e2724341cb",
                column: "ConcurrencyStamp",
                value: "4410de69-6271-4fc3-86c7-3bc9d4926a27");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dceb4c48-51ed-42d0-a3af-b4978b74b770",
                column: "ConcurrencyStamp",
                value: "15a97fc6-0060-4fa2-8683-b58c0f81bdff");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3086a4c6-c709-4ae7-9434-5b945a6e8e81",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6bd4cc82-cc59-4207-ba72-85149e204b10", "AQAAAAEAACcQAAAAEMgzzqjIxOjnGDCFC1tnMen1KGYLWc42FlcnJ4OgSGuL1DA6ot3R8Xt45PpjxZAJuw==", "e215b1d1-3fad-4824-88ca-efa491ae8202" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "94fdc00f-e4f6-4c09-81bf-776862cad6eb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3bc9c035-0700-4842-9702-d9d4d64a6301", "AQAAAAEAACcQAAAAEKpDMJuf2bU7SafahCq4jcvxtYAFJKCq3+Ome9qZMkZID5CoSjqFm/gWkRMP/tLmOw==", "99f213fa-18e1-4bfd-8104-d386daf43963" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Period",
                table: "LeaveAllocations");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "24bf64f3-b8e7-4e65-8033-38e2724341cb",
                column: "ConcurrencyStamp",
                value: "a849d6b8-103d-4c71-a15f-139bd7c36376");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dceb4c48-51ed-42d0-a3af-b4978b74b770",
                column: "ConcurrencyStamp",
                value: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3086a4c6-c709-4ae7-9434-5b945a6e8e81",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "353cb2cb-3520-4496-8561-872a16f3e168", "AQAAAAEAACcQAAAAEMs+860VSTadpebA+R8HEJzH7htogTuKd91bUqLH5pgIr27hPolka8ZCCaoX6pBNDg==", "795c267e-8f82-42a9-b977-174de6db46a3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "94fdc00f-e4f6-4c09-81bf-776862cad6eb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "af2e526e-aa74-4610-8608-46a2073e30c5", "AQAAAAEAACcQAAAAEGai+iTWmatwLfGbHwLm/RYQjveqlpjqa0ftph/i0fxSoBCAXbrbERjPHpjHiSw1LQ==", "5a7643d9-6605-4690-9a40-f69027be7e8c" });
        }
    }
}

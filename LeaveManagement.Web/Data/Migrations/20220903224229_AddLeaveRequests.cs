using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagement.Web.Data.Migrations
{
    public partial class AddLeaveRequests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeaveRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LeaveTypeId = table.Column<int>(type: "int", nullable: false),
                    DateRequested = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequestComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Approved = table.Column<bool>(type: "bit", nullable: true),
                    Cancelled = table.Column<bool>(type: "bit", nullable: false),
                    RequestingEmployeeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveRequests_LeaveTypes_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "LeaveTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "24bf64f3-b8e7-4e65-8033-38e2724341cb",
                column: "ConcurrencyStamp",
                value: "4e640f06-e2bf-411b-aed7-2a695e93cdbf");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dceb4c48-51ed-42d0-a3af-b4978b74b770",
                column: "ConcurrencyStamp",
                value: "986f268a-ae05-4c3e-a783-8447e86d1a15");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3086a4c6-c709-4ae7-9434-5b945a6e8e81",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3c1dd6e7-8b25-45c2-aa4c-d5e51db834e5", "AQAAAAEAACcQAAAAEDgfTOP6HeomohLmebmqWGaUGhwg8Kfu+XXgvAdQlZ/g/5NvLzKfNwe4SI8Yiyn/jA==", "82a01b73-5be1-4c27-b230-2604754cff0e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "94fdc00f-e4f6-4c09-81bf-776862cad6eb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ab7b4aaf-a480-4b91-8974-af5020c07e92", "AQAAAAEAACcQAAAAEMDGK+cc0aM7cSS4w8RmR/4cA+mBkxlJioS7+0PNljB7biO8cIh96DaTcf5zfb9+GA==", "01050db3-2251-448b-892d-f0de90ba30ee" });

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_LeaveTypeId",
                table: "LeaveRequests",
                column: "LeaveTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaveRequests");

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
    }
}

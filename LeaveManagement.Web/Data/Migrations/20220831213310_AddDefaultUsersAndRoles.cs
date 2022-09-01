using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagement.Web.Data.Migrations
{
    public partial class AddDefaultUsersAndRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "24bf64f3-b8e7-4e65-8033-38e2724341cb", "a849d6b8-103d-4c71-a15f-139bd7c36376", "User", "USER" },
                    { "dceb4c48-51ed-42d0-a3af-b4978b74b770", "", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateJoined", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TaxId", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "3086a4c6-c709-4ae7-9434-5b945a6e8e81", 0, "353cb2cb-3520-4496-8561-872a16f3e168", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "user@localhost.com", true, "System", "User", false, null, "USER@LOCALHOST.COM", "USER@LOCALHOST.COM", "AQAAAAEAACcQAAAAEMs+860VSTadpebA+R8HEJzH7htogTuKd91bUqLH5pgIr27hPolka8ZCCaoX6pBNDg==", null, false, "795c267e-8f82-42a9-b977-174de6db46a3", null, false, "user@localhost.com" },
                    { "94fdc00f-e4f6-4c09-81bf-776862cad6eb", 0, "af2e526e-aa74-4610-8608-46a2073e30c5", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@localhost.com", true, "System", "Admin", false, null, "ADMIN@LOCALHOST.COM", "ADMIN@LOCALHOST.COM", "AQAAAAEAACcQAAAAEGai+iTWmatwLfGbHwLm/RYQjveqlpjqa0ftph/i0fxSoBCAXbrbERjPHpjHiSw1LQ==", null, false, "5a7643d9-6605-4690-9a40-f69027be7e8c", null, false, "admin@localhost.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "24bf64f3-b8e7-4e65-8033-38e2724341cb", "3086a4c6-c709-4ae7-9434-5b945a6e8e81" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "dceb4c48-51ed-42d0-a3af-b4978b74b770", "94fdc00f-e4f6-4c09-81bf-776862cad6eb" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "24bf64f3-b8e7-4e65-8033-38e2724341cb", "3086a4c6-c709-4ae7-9434-5b945a6e8e81" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "dceb4c48-51ed-42d0-a3af-b4978b74b770", "94fdc00f-e4f6-4c09-81bf-776862cad6eb" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "24bf64f3-b8e7-4e65-8033-38e2724341cb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dceb4c48-51ed-42d0-a3af-b4978b74b770");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3086a4c6-c709-4ae7-9434-5b945a6e8e81");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "94fdc00f-e4f6-4c09-81bf-776862cad6eb");
        }
    }
}

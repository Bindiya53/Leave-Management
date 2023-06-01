using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Leave_Management.Web.Migrations
{
    public partial class AddLeaveRequest : Migration
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
                    RequestComments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Approved = table.Column<bool>(type: "bit", nullable: true),
                    Cancelled = table.Column<bool>(type: "bit", nullable: false),
                    RequestingEmployeeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0e335630-5594-4d7c-90b7-9251eea7d874",
                column: "ConcurrencyStamp",
                value: "06b40079-9fc9-4088-a864-264585a35db5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0f775630-6694-5d7c-90g7-9251jja7d874",
                column: "ConcurrencyStamp",
                value: "48cd1cc6-5e2c-4a4a-9a48-63fad61ea23c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0e995630-3594-4d7c-90a7-9251eeb7d874",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7def10d2-05eb-44a3-8e21-0a9113ee416e", "AQAAAAEAACcQAAAAEBeDOCinNVcTii4vS5pnxPc/sUCZRkz5HY5D9Q0l8oybOB3o/PQXuPD+M7A5h9WwWA==", "412de85f-23cb-4271-b22f-a412ac351ce8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0f775630-3694-4f7c-90d7-9251ffb7d874",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "029b6f7c-d878-4222-b81b-1808a32285e1", "AQAAAAEAACcQAAAAEMtq4Qnt3a8WNFEGt6GJ8YbE5YPzBvfAHWiR+1z5O9nAt1vfM8t9ZZyO6LKkt2Onbg==", "dc1cbee9-1421-4d06-8c38-293dc289f2fd" });

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
                keyValue: "0e335630-5594-4d7c-90b7-9251eea7d874",
                column: "ConcurrencyStamp",
                value: "cf3ce69d-88f6-4356-98c9-16eeedab580b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0f775630-6694-5d7c-90g7-9251jja7d874",
                column: "ConcurrencyStamp",
                value: "e4dbf21d-980e-4bb7-9e4f-04694bf4cc4d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0e995630-3594-4d7c-90a7-9251eeb7d874",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "923ec5d8-041a-4b43-aeb0-58e06c392204", "AQAAAAEAACcQAAAAEEmSfWL6+QYsvVFywkMnVw/ava1KNJrXK9hy4P9rjy/wfRnTXbiPdcqnSd8C9ilMUg==", "21a73dea-04f2-4e03-a65d-a5e06dd350eb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0f775630-3694-4f7c-90d7-9251ffb7d874",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "95345b93-13c1-4494-adca-119379ada9fb", "AQAAAAEAACcQAAAAEM7TaiXiCPLb+GoahFwEpDdFZA4gH5BXzxLMj2gVtw2vwpBW1rY4fqrr0aNvV+NDhg==", "034f16fc-b8cc-435d-8873-71fa22172c3f" });
        }
    }
}

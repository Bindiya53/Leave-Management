using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Leave_Management.Web.Migrations
{
    public partial class AddPeriodInLeaveAllocation : Migration
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Period",
                table: "LeaveAllocations");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0e335630-5594-4d7c-90b7-9251eea7d874",
                column: "ConcurrencyStamp",
                value: "cca77db5-3b59-4994-a4ca-aecb9361ca80");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0f775630-6694-5d7c-90g7-9251jja7d874",
                column: "ConcurrencyStamp",
                value: "2848e7f6-212d-4f47-9104-83ce3357c042");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0e995630-3594-4d7c-90a7-9251eeb7d874",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "afdb0d96-b628-4987-9d63-c17c167a5bff", "AQAAAAEAACcQAAAAEAtNDGFUSLNYd4/fDnzbswq7eeitKCkzAgmCY3HdMYEhcn6LqP5C5CY6Vmuv/EIyEA==", "e2d87e54-947e-4e80-9736-12c371096984" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0f775630-3694-4f7c-90d7-9251ffb7d874",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fc6c25fd-5c31-4e96-9ccd-13bf6ce7e7ea", "AQAAAAEAACcQAAAAEOqR06gq1KoI6RE3DBgyoE0Jt1O2DR8cMlZekdQ6dEUca0oxJYzm0vMUsS9eVvAYtg==", "948a8620-b7dc-4247-898a-75eb94acadca" });
        }
    }
}

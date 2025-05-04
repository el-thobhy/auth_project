using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace daily_note_be.Migrations
{
    /// <inheritdoc />
    public partial class initDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "m_role_group",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Created_by = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Created_on = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified_by = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Modified_on = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_by = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Deleted_on = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Is_delete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_role_group", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "m_accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValue: "auriwanyasper@gmail.com"),
                    Otp = table.Column<string>(type: "char(6)", nullable: true),
                    OtpExpire = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Attempt = table.Column<int>(type: "int", nullable: false),
                    RoleGroupId = table.Column<int>(type: "int", nullable: false),
                    Created_by = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Created_on = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified_by = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Modified_on = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_by = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Deleted_on = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Is_delete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_accounts_m_role_group_RoleGroupId",
                        column: x => x.RoleGroupId,
                        principalTable: "m_role_group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_authorization_groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleGroupId = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Created_by = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Created_on = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified_by = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Modified_on = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_by = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Deleted_on = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Is_delete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_authorization_groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_m_authorization_groups_m_role_group_RoleGroupId",
                        column: x => x.RoleGroupId,
                        principalTable: "m_role_group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "m_role_group",
                columns: new[] { "Id", "Created_by", "Created_on", "Deleted_by", "Deleted_on", "GroupName", "Is_delete", "Modified_by", "Modified_on" },
                values: new object[,]
                {
                    { 1, "admin", new DateTime(2025, 5, 3, 22, 27, 59, 216, DateTimeKind.Local).AddTicks(3058), null, null, "Admin", false, null, null },
                    { 2, "admin", new DateTime(2025, 5, 3, 22, 27, 59, 216, DateTimeKind.Local).AddTicks(3090), null, null, "User", false, null, null }
                });

            migrationBuilder.InsertData(
                table: "m_accounts",
                columns: new[] { "Id", "Attempt", "Created_by", "Created_on", "Deleted_by", "Deleted_on", "Email", "FirstName", "Is_delete", "LastName", "Modified_by", "Modified_on", "Otp", "OtpExpire", "Password", "RoleGroupId", "UserName" },
                values: new object[,]
                {
                    { 1, 0, "admin", new DateTime(2025, 5, 3, 22, 27, 59, 216, DateTimeKind.Local).AddTicks(3345), null, null, "auriwanyasper@gmail.com", "Super", false, "Admin", null, null, null, null, "ac9689e2272427085e35b9d3e3e8bed88cb3434828b43b86fc0596cad4c6e270", 1, "admin" },
                    { 2, 0, "admin", new DateTime(2025, 5, 3, 22, 27, 59, 216, DateTimeKind.Local).AddTicks(3349), null, null, "user@gmail.com", "Regular", false, "User", null, null, null, null, "c431bffe6c2cf3b69ad2e9cbbe9806835dbced7c97b9d3f946387ee92eb17018", 2, "user" }
                });

            migrationBuilder.InsertData(
                table: "m_authorization_groups",
                columns: new[] { "Id", "Created_by", "Created_on", "Deleted_by", "Deleted_on", "Is_delete", "Modified_by", "Modified_on", "Role", "RoleGroupId" },
                values: new object[,]
                {
                    { 1, "admin", new DateTime(2025, 5, 3, 22, 27, 59, 216, DateTimeKind.Local).AddTicks(3379), null, null, false, null, null, "account", 1 },
                    { 2, "admin", new DateTime(2025, 5, 3, 22, 27, 59, 216, DateTimeKind.Local).AddTicks(3383), null, null, false, null, null, "role", 1 },
                    { 3, "admin", new DateTime(2025, 5, 3, 22, 27, 59, 216, DateTimeKind.Local).AddTicks(3385), null, null, false, null, null, "authorization", 1 },
                    { 4, "admin", new DateTime(2025, 5, 3, 22, 27, 59, 216, DateTimeKind.Local).AddTicks(3388), null, null, false, null, null, "home", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_m_accounts_RoleGroupId",
                table: "m_accounts",
                column: "RoleGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_m_accounts_UserName",
                table: "m_accounts",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_authorization_groups_RoleGroupId",
                table: "m_authorization_groups",
                column: "RoleGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_m_role_group_GroupName",
                table: "m_role_group",
                column: "GroupName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "m_accounts");

            migrationBuilder.DropTable(
                name: "m_authorization_groups");

            migrationBuilder.DropTable(
                name: "m_role_group");
        }
    }
}

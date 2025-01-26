using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealTimeTaskManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddChatEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_EnteredById1",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_ModifiedById1",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_EnteredById1",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_ModifiedById1",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "EnteredById1",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "ModifiedById1",
                table: "Tickets");

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedById",
                table: "Tickets",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "EnteredById",
                table: "Tickets",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateTable(
                name: "Chat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EnteredById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ModifiedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EnteredOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chat_AspNetUsers_EnteredById",
                        column: x => x.EnteredById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Chat_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_EnteredById",
                table: "Tickets",
                column: "EnteredById");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ModifiedById",
                table: "Tickets",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Chat_EnteredById",
                table: "Chat",
                column: "EnteredById");

            migrationBuilder.CreateIndex(
                name: "IX_Chat_ModifiedById",
                table: "Chat",
                column: "ModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_EnteredById",
                table: "Tickets",
                column: "EnteredById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_ModifiedById",
                table: "Tickets",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_EnteredById",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_ModifiedById",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "Chat");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_EnteredById",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_ModifiedById",
                table: "Tickets");

            migrationBuilder.AlterColumn<Guid>(
                name: "ModifiedById",
                table: "Tickets",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<Guid>(
                name: "EnteredById",
                table: "Tickets",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "EnteredById1",
                table: "Tickets",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedById1",
                table: "Tickets",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_EnteredById1",
                table: "Tickets",
                column: "EnteredById1");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ModifiedById1",
                table: "Tickets",
                column: "ModifiedById1");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_EnteredById1",
                table: "Tickets",
                column: "EnteredById1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_ModifiedById1",
                table: "Tickets",
                column: "ModifiedById1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}

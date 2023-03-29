using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KTechPassApp.Data.Migrations
{
    public partial class firstmedicalappdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRecords_Users_UserId",
                table: "UserRecords");

            migrationBuilder.DropColumn(
                name: "RecordDescription",
                table: "UserRecords");

            migrationBuilder.DropColumn(
                name: "RecordPassword",
                table: "UserRecords");

            migrationBuilder.RenameColumn(
                name: "RecordUrl",
                table: "UserRecords",
                newName: "RecordValue");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tckno",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserRecords",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "UserRecords",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImportanceLevel",
                table: "UserRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "UserRecords",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "UserRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRecords_Users_UserId",
                table: "UserRecords",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRecords_Users_UserId",
                table: "UserRecords");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Tckno",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "UserRecords");

            migrationBuilder.DropColumn(
                name: "ImportanceLevel",
                table: "UserRecords");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "UserRecords");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "UserRecords");

            migrationBuilder.RenameColumn(
                name: "RecordValue",
                table: "UserRecords",
                newName: "RecordUrl");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserRecords",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecordDescription",
                table: "UserRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RecordPassword",
                table: "UserRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRecords_Users_UserId",
                table: "UserRecords",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

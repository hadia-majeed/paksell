using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace paksell.Db.Migrations
{
    /// <inheritdoc />
    public partial class migration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // First drop existing foreign keys
            migrationBuilder.DropForeignKey(
                name: "FK_CityAreas_User_UserId",
                table: "CityAreas");

            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_User_PostedById",
                table: "Advertisements");

            // Change LoginId to string
            migrationBuilder.AlterColumn<string>(
                name: "LoginId",
                table: "User",
                type: "varchar(50)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            // Make UserId nullable in CityAreas
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "CityAreas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            // Re-add foreign keys
            migrationBuilder.AddForeignKey(
                name: "FK_CityAreas_User_UserId",
                table: "CityAreas",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_User_PostedById",
                table: "Advertisements",
                column: "PostedById",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CityAreas_User_UserId",
                table: "CityAreas");

            migrationBuilder.AlterColumn<string>(
                name: "LoginId",
                table: "User",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "CityAreas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CityAreas_User_UserId",
                table: "CityAreas",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

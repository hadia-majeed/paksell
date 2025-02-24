using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace paksell.Db.Migrations
{
    /// <inheritdoc />
    public partial class migration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "advertisementCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    Image = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_advertisementCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", nullable: true),
                    LoginId = table.Column<string>(type: "varchar(50)", nullable: false),
                    Password = table.Column<string>(type: "varchar(50)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(11)", nullable: true),
                    UserImage = table.Column<string>(type: "varchar(50)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SecurityQuestion = table.Column<string>(type: "varchar(100)", nullable: true),
                    SecurityAnswer = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CityAreas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityAreas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CityAreas_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "myTable1",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    Description = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    PostedById = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    startsOn = table.Column<DateOnly>(type: "date", nullable: false),
                    endsOn = table.Column<DateOnly>(type: "date", nullable: false),
                    CityAreaId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_myTable1", x => x.Id);
                    table.ForeignKey(
                        name: "FK_myTable1_CityAreas_CityAreaId",
                        column: x => x.CityAreaId,
                        principalTable: "CityAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_myTable1_User_PostedById",
                        column: x => x.PostedById,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_myTable1_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_myTable1_advertisementCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "advertisementCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "advertisementFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    value = table.Column<string>(type: "varchar(255)", nullable: true),
                    AdvertisementId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_advertisementFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_advertisementFeatures_myTable1_AdvertisementId",
                        column: x => x.AdvertisementId,
                        principalTable: "myTable1",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdvertisementImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rank = table.Column<int>(type: "int", nullable: false),
                    AdvertisementId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertisementImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdvertisementImages_myTable1_AdvertisementId",
                        column: x => x.AdvertisementId,
                        principalTable: "myTable1",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_advertisementFeatures_AdvertisementId",
                table: "advertisementFeatures",
                column: "AdvertisementId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvertisementImages_AdvertisementId",
                table: "AdvertisementImages",
                column: "AdvertisementId");

            migrationBuilder.CreateIndex(
                name: "IX_CityAreas_UserId",
                table: "CityAreas",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_myTable1_CategoryId",
                table: "myTable1",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_myTable1_CityAreaId",
                table: "myTable1",
                column: "CityAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_myTable1_PostedById",
                table: "myTable1",
                column: "PostedById");

            migrationBuilder.CreateIndex(
                name: "IX_myTable1_UserId",
                table: "myTable1",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "advertisementFeatures");

            migrationBuilder.DropTable(
                name: "AdvertisementImages");

            migrationBuilder.DropTable(
                name: "myTable1");

            migrationBuilder.DropTable(
                name: "CityAreas");

            migrationBuilder.DropTable(
                name: "advertisementCategories");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}

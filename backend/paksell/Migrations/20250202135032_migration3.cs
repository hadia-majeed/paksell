using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace paksell.Db.Migrations
{
    /// <inheritdoc />
    public partial class migration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_advertisementFeatures_myTable1_AdvertisementId",
                table: "advertisementFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_AdvertisementImages_myTable1_AdvertisementId",
                table: "AdvertisementImages");

            migrationBuilder.DropForeignKey(
                name: "FK_myTable1_CityAreas_CityAreaId",
                table: "myTable1");

            migrationBuilder.DropForeignKey(
                name: "FK_myTable1_User_PostedById",
                table: "myTable1");

            migrationBuilder.DropForeignKey(
                name: "FK_myTable1_User_UserId",
                table: "myTable1");

            migrationBuilder.DropForeignKey(
                name: "FK_myTable1_advertisementCategories_CategoryId",
                table: "myTable1");

            migrationBuilder.DropPrimaryKey(
                name: "PK_myTable1",
                table: "myTable1");

            migrationBuilder.RenameTable(
                name: "myTable1",
                newName: "Advertisements");

            migrationBuilder.RenameIndex(
                name: "IX_myTable1_UserId",
                table: "Advertisements",
                newName: "IX_Advertisements_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_myTable1_PostedById",
                table: "Advertisements",
                newName: "IX_Advertisements_PostedById");

            migrationBuilder.RenameIndex(
                name: "IX_myTable1_CityAreaId",
                table: "Advertisements",
                newName: "IX_Advertisements_CityAreaId");

            migrationBuilder.RenameIndex(
                name: "IX_myTable1_CategoryId",
                table: "Advertisements",
                newName: "IX_Advertisements_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Advertisements",
                table: "Advertisements",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_advertisementFeatures_Advertisements_AdvertisementId",
                table: "advertisementFeatures",
                column: "AdvertisementId",
                principalTable: "Advertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AdvertisementImages_Advertisements_AdvertisementId",
                table: "AdvertisementImages",
                column: "AdvertisementId",
                principalTable: "Advertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_CityAreas_CityAreaId",
                table: "Advertisements",
                column: "CityAreaId",
                principalTable: "CityAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_User_PostedById",
                table: "Advertisements",
                column: "PostedById",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_User_UserId",
                table: "Advertisements",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_advertisementCategories_CategoryId",
                table: "Advertisements",
                column: "CategoryId",
                principalTable: "advertisementCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_advertisementFeatures_Advertisements_AdvertisementId",
                table: "advertisementFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_AdvertisementImages_Advertisements_AdvertisementId",
                table: "AdvertisementImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_CityAreas_CityAreaId",
                table: "Advertisements");

            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_User_PostedById",
                table: "Advertisements");

            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_User_UserId",
                table: "Advertisements");

            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_advertisementCategories_CategoryId",
                table: "Advertisements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Advertisements",
                table: "Advertisements");

            migrationBuilder.RenameTable(
                name: "Advertisements",
                newName: "myTable1");

            migrationBuilder.RenameIndex(
                name: "IX_Advertisements_UserId",
                table: "myTable1",
                newName: "IX_myTable1_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Advertisements_PostedById",
                table: "myTable1",
                newName: "IX_myTable1_PostedById");

            migrationBuilder.RenameIndex(
                name: "IX_Advertisements_CityAreaId",
                table: "myTable1",
                newName: "IX_myTable1_CityAreaId");

            migrationBuilder.RenameIndex(
                name: "IX_Advertisements_CategoryId",
                table: "myTable1",
                newName: "IX_myTable1_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_myTable1",
                table: "myTable1",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_advertisementFeatures_myTable1_AdvertisementId",
                table: "advertisementFeatures",
                column: "AdvertisementId",
                principalTable: "myTable1",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AdvertisementImages_myTable1_AdvertisementId",
                table: "AdvertisementImages",
                column: "AdvertisementId",
                principalTable: "myTable1",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_myTable1_CityAreas_CityAreaId",
                table: "myTable1",
                column: "CityAreaId",
                principalTable: "CityAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_myTable1_User_PostedById",
                table: "myTable1",
                column: "PostedById",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_myTable1_User_UserId",
                table: "myTable1",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_myTable1_advertisementCategories_CategoryId",
                table: "myTable1",
                column: "CategoryId",
                principalTable: "advertisementCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

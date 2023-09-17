using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transportathon.Migrations.AddDbContext
{
    /// <inheritdoc />
    public partial class DriverReviewsCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreateJobs_DriverApplied_ApplyDriverId",
                table: "CreateJobs");

            migrationBuilder.DropIndex(
                name: "IX_CreateJobs_ApplyDriverId",
                table: "CreateJobs");

            migrationBuilder.DropColumn(
                name: "ApplyDriverId",
                table: "CreateJobs");

            migrationBuilder.CreateTable(
                name: "DriverReviews",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RatingFast = table.Column<int>(type: "int", nullable: false),
                    ProductDurability = table.Column<int>(type: "int", nullable: false),
                    ServiceSatisfaction = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DriverId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverReviews", x => x.ReviewId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DriverReviews");

            migrationBuilder.AddColumn<int>(
                name: "ApplyDriverId",
                table: "CreateJobs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreateJobs_ApplyDriverId",
                table: "CreateJobs",
                column: "ApplyDriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_CreateJobs_DriverApplied_ApplyDriverId",
                table: "CreateJobs",
                column: "ApplyDriverId",
                principalTable: "DriverApplied",
                principalColumn: "Id");
        }
    }
}

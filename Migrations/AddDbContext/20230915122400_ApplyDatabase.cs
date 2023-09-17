using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transportathon.Migrations.AddDbContext
{
    /// <inheritdoc />
    public partial class ApplyDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplyDriverId",
                table: "CreateJobs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DriverApplied",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DriverId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedJobId = table.Column<int>(type: "int", nullable: false),
                    isAccepted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverApplied", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreateJobs_DriverApplied_ApplyDriverId",
                table: "CreateJobs");

            migrationBuilder.DropTable(
                name: "DriverApplied");

            migrationBuilder.DropIndex(
                name: "IX_CreateJobs_ApplyDriverId",
                table: "CreateJobs");

            migrationBuilder.DropColumn(
                name: "ApplyDriverId",
                table: "CreateJobs");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraphReview.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_ManagedDepartmentId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Employees_RevieweeId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Employees_ReviewerId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_RevieweeId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ReviewerId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ManagedDepartmentId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "RevieweeId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ReviewerId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ManagedDepartmentId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Departments");

            migrationBuilder.CreateTable(
                name: "EmployeeReview",
                columns: table => new
                {
                    AttendeesId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    ReviewsId = table.Column<string>(type: "nvarchar(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeReview", x => new { x.AttendeesId, x.ReviewsId });
                    table.ForeignKey(
                        name: "FK_EmployeeReview_Employees_AttendeesId",
                        column: x => x.AttendeesId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeReview_Reviews_ReviewsId",
                        column: x => x.ReviewsId,
                        principalTable: "Reviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeReview_ReviewsId",
                table: "EmployeeReview",
                column: "ReviewsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeReview");

            migrationBuilder.AddColumn<string>(
                name: "RevieweeId",
                table: "Reviews",
                type: "nvarchar(36)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReviewerId",
                table: "Reviews",
                type: "nvarchar(36)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ManagedDepartmentId",
                table: "Employees",
                type: "nvarchar(36)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManagerId",
                table: "Departments",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_RevieweeId",
                table: "Reviews",
                column: "RevieweeId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ReviewerId",
                table: "Reviews",
                column: "ReviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ManagedDepartmentId",
                table: "Employees",
                column: "ManagedDepartmentId",
                unique: true,
                filter: "[ManagedDepartmentId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_ManagedDepartmentId",
                table: "Employees",
                column: "ManagedDepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Employees_RevieweeId",
                table: "Reviews",
                column: "RevieweeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Employees_ReviewerId",
                table: "Reviews",
                column: "ReviewerId",
                principalTable: "Employees",
                principalColumn: "Id");
        }
    }
}

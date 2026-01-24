using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repository_Pattern.Migrations
{
    /// <inheritdoc />
    public partial class fulldbsetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    departmanetName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    departmaneDesc = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "student",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    DepratmentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_student", x => x.Id);
                    table.ForeignKey(
                        name: "Fk_student_Department",
                        column: x => x.DepratmentId,
                        principalTable: "Department",
                        principalColumn: "id");
                });

            migrationBuilder.InsertData(
                table: "Department",
                columns: new[] { "id", "departmaneDesc", "departmanetName" },
                values: new object[,]
                {
                    { 1, "Esc Departmetn", "ESC" },
                    { 2, "CSE Departmetn", "CSE" }
                });

            migrationBuilder.InsertData(
                table: "student",
                columns: new[] { "Id", "Address", "DepratmentId", "Email", "Name", "Phone", "Status" },
                values: new object[,]
                {
                    { 1, "43243243432", null, "rohit@gmail.com", "Rohit", "4324324234", false },
                    { 2, "Ambala", null, "Mohit@gmail.com", "Mohit", "23424324324", false },
                    { 3, "Bilaspur", null, "Rajat@gmail.com", "Rajat", "345345345", false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_student_DepratmentId",
                table: "student",
                column: "DepratmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "employee");

            migrationBuilder.DropTable(
                name: "student");

            migrationBuilder.DropTable(
                name: "Department");
        }
    }
}

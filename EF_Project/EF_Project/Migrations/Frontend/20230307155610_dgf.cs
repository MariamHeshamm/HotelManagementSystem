using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EF_Project.Migrations.Frontend
{
    /// <inheritdoc />
    public partial class dgf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_CardTypes_CardTypeId",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "CardTypes");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_CardTypeId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "CardTypeId",
                table: "Reservations");

            migrationBuilder.AddColumn<string>(
                name: "Card_Type",
                table: "Reservations",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Card_Type",
                table: "Reservations");

            migrationBuilder.AddColumn<int>(
                name: "CardTypeId",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CardTypes",
                columns: table => new
                {
                    CardTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardTypes", x => x.CardTypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CardTypeId",
                table: "Reservations",
                column: "CardTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_CardTypes_CardTypeId",
                table: "Reservations",
                column: "CardTypeId",
                principalTable: "CardTypes",
                principalColumn: "CardTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

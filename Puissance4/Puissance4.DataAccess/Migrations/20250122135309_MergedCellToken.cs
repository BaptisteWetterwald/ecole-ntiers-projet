using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Puissance4.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class MergedCellToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cells_Tokens_Id",
                table: "Cells");

            migrationBuilder.DropTable(
                name: "Tokens");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Cells",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "TokenColor",
                table: "Cells",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TokenColor",
                table: "Cells");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Cells",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Color = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Cells_Tokens_Id",
                table: "Cells",
                column: "Id",
                principalTable: "Tokens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

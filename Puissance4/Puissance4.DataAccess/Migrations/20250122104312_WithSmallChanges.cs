using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Puissance4.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class WithSmallChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cells_Tokens_TokenId",
                table: "Cells");

            migrationBuilder.DropIndex(
                name: "IX_Cells_TokenId",
                table: "Cells");

            migrationBuilder.DropColumn(
                name: "TokenId",
                table: "Cells");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Players",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Cells",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cells_Tokens_Id",
                table: "Cells",
                column: "Id",
                principalTable: "Tokens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cells_Tokens_Id",
                table: "Cells");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Players",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Cells",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "TokenId",
                table: "Cells",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cells_TokenId",
                table: "Cells",
                column: "TokenId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cells_Tokens_TokenId",
                table: "Cells",
                column: "TokenId",
                principalTable: "Tokens",
                principalColumn: "Id");
        }
    }
}

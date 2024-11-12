using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogMBA.Data.Migrations
{
    /// <inheritdoc />
    public partial class AutorNoComentario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Autores_AutorId",
                table: "Comentarios");

            migrationBuilder.AlterColumn<Guid>(
                name: "AutorId",
                table: "Comentarios",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Autores_AutorId",
                table: "Comentarios",
                column: "AutorId",
                principalTable: "Autores",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Autores_AutorId",
                table: "Comentarios");

            migrationBuilder.AlterColumn<Guid>(
                name: "AutorId",
                table: "Comentarios",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Autores_AutorId",
                table: "Comentarios",
                column: "AutorId",
                principalTable: "Autores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

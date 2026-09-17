using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudioAgenda.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AtualizacaoTabAgenda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataHora",
                table: "agendas",
                newName: "HoraInicio");

            migrationBuilder.AddColumn<DateTime>(
                name: "Data",
                table: "agendas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DuracaoMinuto",
                table: "agendas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "HoraFim",
                table: "agendas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "agendas");

            migrationBuilder.DropColumn(
                name: "DuracaoMinuto",
                table: "agendas");

            migrationBuilder.DropColumn(
                name: "HoraFim",
                table: "agendas");

            migrationBuilder.RenameColumn(
                name: "HoraInicio",
                table: "agendas",
                newName: "DataHora");
        }
    }
}

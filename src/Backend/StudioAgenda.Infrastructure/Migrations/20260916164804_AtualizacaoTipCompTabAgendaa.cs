using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudioAgenda.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AtualizacaoTipCompTabAgendaa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DuracaoMinuto",
                table: "agendas");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "HoraInicio",
                table: "agendas",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Data",
                table: "agendas",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "HoraInicio",
                table: "agendas",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Data",
                table: "agendas",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<int>(
                name: "DuracaoMinuto",
                table: "agendas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

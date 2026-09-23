using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudioAgenda.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddComponenteTabAgendas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeOnly>(
                name: "HoraFim",
                table: "agendas",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HoraFim",
                table: "agendas");
        }
    }
}

﻿namespace Ordering.API.Infrastructure.IntegrationEventMigrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class IntegrationEventInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "IntegrationEventLog",
                table => new
                {
                    EventId = table.Column<Guid>(nullable: false),
                    Content = table.Column<string>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    EventTypeName = table.Column<string>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    TimesSent = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegrationEventLog", x => x.EventId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IntegrationEventLog");
        }
    }
}

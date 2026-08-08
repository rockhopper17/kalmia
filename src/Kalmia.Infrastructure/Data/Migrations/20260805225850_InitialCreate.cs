using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kalmia.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ActivityType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ActivityDate = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "CAST(GETUTCDATE() AS DATE)"),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false, defaultValue: new TimeOnly(0, 0, 0)),
                    DurationSeconds = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    DistanceMeters = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    ElevationGainMeters = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activities");
        }
    }
}

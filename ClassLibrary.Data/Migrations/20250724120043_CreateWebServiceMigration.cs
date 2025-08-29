using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClassLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateWebServiceMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tests");

            migrationBuilder.CreateTable(
                name: "Airports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameAirport = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Airline = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CodeAirports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AirportId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodeAirports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CodeAirports_Airports_AirportId",
                        column: x => x.AirportId,
                        principalTable: "Airports",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DeparturesAndArrivals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Departure = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Arrival = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Routing = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Distance = table.Column<int>(type: "int", nullable: false),
                    AirportId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeparturesAndArrivals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeparturesAndArrivals_Airports_AirportId",
                        column: x => x.AirportId,
                        principalTable: "Airports",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Informations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberFlight = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberBoard = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AirportId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Informations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Informations_Airports_AirportId",
                        column: x => x.AirportId,
                        principalTable: "Airports",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PitStops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PitStopCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PitStopDuration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AirportId = table.Column<int>(type: "int", nullable: true),
                    DepAndArrId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PitStops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PitStops_Airports_AirportId",
                        column: x => x.AirportId,
                        principalTable: "Airports",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PitStops_DeparturesAndArrivals_DepAndArrId",
                        column: x => x.DepAndArrId,
                        principalTable: "DeparturesAndArrivals",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CodeAirports_AirportId",
                table: "CodeAirports",
                column: "AirportId");

            migrationBuilder.CreateIndex(
                name: "IX_DeparturesAndArrivals_AirportId",
                table: "DeparturesAndArrivals",
                column: "AirportId");

            migrationBuilder.CreateIndex(
                name: "IX_Informations_AirportId",
                table: "Informations",
                column: "AirportId",
                unique: true,
                filter: "[AirportId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PitStops_AirportId",
                table: "PitStops",
                column: "AirportId");

            migrationBuilder.CreateIndex(
                name: "IX_PitStops_DepAndArrId",
                table: "PitStops",
                column: "DepAndArrId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CodeAirports");

            migrationBuilder.DropTable(
                name: "Informations");

            migrationBuilder.DropTable(
                name: "PitStops");

            migrationBuilder.DropTable(
                name: "DeparturesAndArrivals");

            migrationBuilder.DropTable(
                name: "Airports");

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.Id);
                });
        }
    }
}

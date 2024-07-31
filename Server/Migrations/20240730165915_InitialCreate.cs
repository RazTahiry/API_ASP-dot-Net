using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Itineraires",
                columns: table => new
                {
                    CodeItineraire = table.Column<string>(type: "TEXT", nullable: false),
                    LieuDepart = table.Column<string>(type: "TEXT", nullable: false),
                    LieuArrivee = table.Column<string>(type: "TEXT", nullable: false),
                    HeureDepart = table.Column<string>(type: "TEXT", nullable: false),
                    JourDepart = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Itineraires", x => x.CodeItineraire);
                });

            migrationBuilder.CreateTable(
                name: "Trains",
                columns: table => new
                {
                    Immatriculation = table.Column<string>(type: "TEXT", nullable: false),
                    CodeItineraire = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trains", x => x.Immatriculation);
                    table.ForeignKey(
                        name: "FK_Trains_Itineraires_CodeItineraire",
                        column: x => x.CodeItineraire,
                        principalTable: "Itineraires",
                        principalColumn: "CodeItineraire",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CodeCategorie = table.Column<string>(type: "TEXT", nullable: false),
                    LibelleCategorie = table.Column<string>(type: "TEXT", nullable: false),
                    NbPlaceSupporte = table.Column<int>(type: "INTEGER", nullable: false),
                    Frais = table.Column<double>(type: "REAL", nullable: false),
                    Immatriculation = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CodeCategorie);
                    table.ForeignKey(
                        name: "FK_Categories_Trains_Immatriculation",
                        column: x => x.Immatriculation,
                        principalTable: "Trains",
                        principalColumn: "Immatriculation",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Voyageurs",
                columns: table => new
                {
                    NumTicket = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmailVoyageur = table.Column<string>(type: "TEXT", nullable: false),
                    NomVoyageur = table.Column<string>(type: "TEXT", nullable: false),
                    DateDepart = table.Column<string>(type: "TEXT", nullable: false),
                    NbPlace = table.Column<int>(type: "INTEGER", nullable: false),
                    CodeCategorie = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voyageurs", x => x.NumTicket);
                    table.ForeignKey(
                        name: "FK_Voyageurs_Categories_CodeCategorie",
                        column: x => x.CodeCategorie,
                        principalTable: "Categories",
                        principalColumn: "CodeCategorie",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Immatriculation",
                table: "Categories",
                column: "Immatriculation");

            migrationBuilder.CreateIndex(
                name: "IX_Trains_CodeItineraire",
                table: "Trains",
                column: "CodeItineraire");

            migrationBuilder.CreateIndex(
                name: "IX_Voyageurs_CodeCategorie",
                table: "Voyageurs",
                column: "CodeCategorie");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Voyageurs");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Trains");

            migrationBuilder.DropTable(
                name: "Itineraires");
        }
    }
}

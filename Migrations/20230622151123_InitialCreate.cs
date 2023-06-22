using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjektProgramowanie.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dania",
                columns: table => new
                {
                    DaniaId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Opis = table.Column<string>(type: "TEXT", nullable: false),
                    Cena = table.Column<float>(type: "REAL", nullable: false),
                    Nazwa = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dania", x => x.DaniaId);
                });

            migrationBuilder.CreateTable(
                name: "lokale",
                columns: table => new
                {
                    LokaleId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nazwa = table.Column<string>(type: "TEXT", nullable: false),
                    Miasto = table.Column<string>(type: "TEXT", nullable: false),
                    Adres = table.Column<string>(type: "TEXT", nullable: false),
                    Kuchnia = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lokale", x => x.LokaleId);
                });

            migrationBuilder.CreateTable(
                name: "promocje",
                columns: table => new
                {
                    PromocjeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Opis = table.Column<string>(type: "TEXT", nullable: false),
                    DataRozpoczęcia = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataZakończenia = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_promocje", x => x.PromocjeId);
                });

            migrationBuilder.CreateTable(
                name: "oferta",
                columns: table => new
                {
                    LokaleId = table.Column<int>(type: "INTEGER", nullable: false),
                    DaniaId = table.Column<int>(type: "INTEGER", nullable: false),
                    OfertaId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_oferta", x => new { x.LokaleId, x.DaniaId });
                    table.ForeignKey(
                        name: "FK_oferta_dania_DaniaId",
                        column: x => x.DaniaId,
                        principalTable: "dania",
                        principalColumn: "DaniaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_oferta_lokale_LokaleId",
                        column: x => x.LokaleId,
                        principalTable: "lokale",
                        principalColumn: "LokaleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "opinie",
                columns: table => new
                {
                    OpinieId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Autor = table.Column<string>(type: "TEXT", nullable: false),
                    Opinia = table.Column<string>(type: "TEXT", nullable: false),
                    DataWystawienia = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Ocena = table.Column<int>(type: "INTEGER", nullable: false),
                    LokaleId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_opinie", x => x.OpinieId);
                    table.ForeignKey(
                        name: "FK_opinie_lokale_LokaleId",
                        column: x => x.LokaleId,
                        principalTable: "lokale",
                        principalColumn: "LokaleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "promocjelokalu",
                columns: table => new
                {
                    LokaleId = table.Column<int>(type: "INTEGER", nullable: false),
                    PromocjeId = table.Column<int>(type: "INTEGER", nullable: false),
                    PromocjeLokaluId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_promocjelokalu", x => new { x.LokaleId, x.PromocjeId });
                    table.ForeignKey(
                        name: "FK_promocjelokalu_lokale_LokaleId",
                        column: x => x.LokaleId,
                        principalTable: "lokale",
                        principalColumn: "LokaleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_promocjelokalu_promocje_PromocjeId",
                        column: x => x.PromocjeId,
                        principalTable: "promocje",
                        principalColumn: "PromocjeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_oferta_DaniaId",
                table: "oferta",
                column: "DaniaId");

            migrationBuilder.CreateIndex(
                name: "IX_opinie_LokaleId",
                table: "opinie",
                column: "LokaleId");

            migrationBuilder.CreateIndex(
                name: "IX_promocjelokalu_PromocjeId",
                table: "promocjelokalu",
                column: "PromocjeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "oferta");

            migrationBuilder.DropTable(
                name: "opinie");

            migrationBuilder.DropTable(
                name: "promocjelokalu");

            migrationBuilder.DropTable(
                name: "dania");

            migrationBuilder.DropTable(
                name: "lokale");

            migrationBuilder.DropTable(
                name: "promocje");
        }
    }
}

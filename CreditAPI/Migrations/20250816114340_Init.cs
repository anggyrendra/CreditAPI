using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CreditAPI.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kredits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Plafon = table.Column<decimal>(type: "numeric", nullable: false),
                    Bunga = table.Column<decimal>(type: "numeric", nullable: false),
                    Tenor = table.Column<int>(type: "integer", nullable: false),
                    Angsuran = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kredits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PengajuanKredits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Plafon = table.Column<decimal>(type: "numeric", nullable: false),
                    Bunga = table.Column<decimal>(type: "numeric", nullable: false),
                    Tenor = table.Column<int>(type: "integer", nullable: false),
                    AngsuranPerBulan = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PengajuanKredits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Angsurans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    KreditId = table.Column<Guid>(type: "uuid", nullable: false),
                    Jumlah = table.Column<decimal>(type: "numeric", nullable: false),
                    SudahDibayar = table.Column<decimal>(type: "numeric", nullable: false),
                    JatuhTempo = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Lunas = table.Column<bool>(type: "boolean", nullable: false),
                    PengajuanId = table.Column<Guid>(type: "uuid", nullable: false),
                    JumlahAngsuran = table.Column<decimal>(type: "numeric", nullable: false),
                    AngsuranKe = table.Column<int>(type: "integer", nullable: false),
                    JumlahBayar = table.Column<decimal>(type: "numeric", nullable: true),
                    TanggalBayar = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    StatusLunas = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Angsurans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Angsurans_Kredits_KreditId",
                        column: x => x.KreditId,
                        principalTable: "Kredits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Angsurans_PengajuanKredits_PengajuanId",
                        column: x => x.PengajuanId,
                        principalTable: "PengajuanKredits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Angsurans_KreditId",
                table: "Angsurans",
                column: "KreditId");

            migrationBuilder.CreateIndex(
                name: "IX_Angsurans_PengajuanId",
                table: "Angsurans",
                column: "PengajuanId");

            migrationBuilder.CreateIndex(
                name: "IX_Kredits_Plafon",
                table: "Kredits",
                column: "Plafon");

            migrationBuilder.CreateIndex(
                name: "IX_Kredits_Tenor",
                table: "Kredits",
                column: "Tenor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Angsurans");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Kredits");

            migrationBuilder.DropTable(
                name: "PengajuanKredits");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace G6.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoDoBanco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Ativos");

            migrationBuilder.CreateTable(
                name: "Ativos",
                schema: "Ativos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Currency = table.Column<string>(type: "text", nullable: true),
                    LogoUrl = table.Column<string>(type: "text", nullable: true),
                    Symbol = table.Column<string>(type: "text", nullable: true),
                    averageDailyVolume3Month = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    averageDailyVolume10Day = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    longName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ativos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DadosHistoricosAtivos",
                schema: "Ativos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<string>(type: "text", nullable: false),
                    Open = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    High = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    Low = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    Close = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    Volume = table.Column<int>(type: "integer", nullable: false),
                    AdjustedClose = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    AtivosId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DadosHistoricosAtivos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DadosHistoricosAtivos_Ativos_AtivosId",
                        column: x => x.AtivosId,
                        principalSchema: "Ativos",
                        principalTable: "Ativos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DadosHistoricosAtivos_AtivosId",
                schema: "Ativos",
                table: "DadosHistoricosAtivos",
                column: "AtivosId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DadosHistoricosAtivos",
                schema: "Ativos");

            migrationBuilder.DropTable(
                name: "Ativos",
                schema: "Ativos");
        }
    }
}

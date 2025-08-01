using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Postgres.Migrations
{
    /// <inheritdoc />
    public partial class AddUserWithRoleEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "code_first");

            migrationBuilder.CreateTable(
                name: "countries",
                schema: "code_first",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_countries", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tournaments",
                schema: "code_first",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tournaments", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "code_first",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    username = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    email = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    role = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "teams",
                schema: "code_first",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    country_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teams", x => x.id);
                    table.ForeignKey(
                        name: "FK_teams_countries_country_id",
                        column: x => x.country_id,
                        principalSchema: "code_first",
                        principalTable: "countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "players",
                schema: "code_first",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    team_id = table.Column<int>(type: "integer", nullable: false),
                    country_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_players", x => x.id);
                    table.ForeignKey(
                        name: "FK_players_countries_country_id",
                        column: x => x.country_id,
                        principalSchema: "code_first",
                        principalTable: "countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_players_teams_team_id",
                        column: x => x.team_id,
                        principalSchema: "code_first",
                        principalTable: "teams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "team_tournaments",
                schema: "code_first",
                columns: table => new
                {
                    team_id = table.Column<int>(type: "integer", nullable: false),
                    tournament_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_team_tournaments", x => new { x.team_id, x.tournament_id });
                    table.ForeignKey(
                        name: "FK_team_tournaments_teams_team_id",
                        column: x => x.team_id,
                        principalSchema: "code_first",
                        principalTable: "teams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_team_tournaments_tournaments_tournament_id",
                        column: x => x.tournament_id,
                        principalSchema: "code_first",
                        principalTable: "tournaments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_countries_name",
                schema: "code_first",
                table: "countries",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_players_country_id",
                schema: "code_first",
                table: "players",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_players_team_id",
                schema: "code_first",
                table: "players",
                column: "team_id");

            migrationBuilder.CreateIndex(
                name: "IX_team_tournaments_tournament_id",
                schema: "code_first",
                table: "team_tournaments",
                column: "tournament_id");

            migrationBuilder.CreateIndex(
                name: "IX_teams_country_id",
                schema: "code_first",
                table: "teams",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_teams_name_country_id",
                schema: "code_first",
                table: "teams",
                columns: new[] { "name", "country_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_username",
                schema: "code_first",
                table: "users",
                column: "username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "players",
                schema: "code_first");

            migrationBuilder.DropTable(
                name: "team_tournaments",
                schema: "code_first");

            migrationBuilder.DropTable(
                name: "users",
                schema: "code_first");

            migrationBuilder.DropTable(
                name: "teams",
                schema: "code_first");

            migrationBuilder.DropTable(
                name: "tournaments",
                schema: "code_first");

            migrationBuilder.DropTable(
                name: "countries",
                schema: "code_first");
        }
    }
}

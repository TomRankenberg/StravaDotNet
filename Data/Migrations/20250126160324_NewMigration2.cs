using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Athletes_AthleteId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_SummaryGears_GearId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Lap_Athletes_AthleteId",
                table: "Lap");

            migrationBuilder.DropTable(
                name: "SummaryGears");

            migrationBuilder.DropIndex(
                name: "IX_Activities_AthleteId",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_GearId",
                table: "Activities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Athletes",
                table: "Athletes");

            migrationBuilder.RenameTable(
                name: "Athletes",
                newName: "MetaAthlete");

            migrationBuilder.AddColumn<long>(
                name: "ActivityId",
                table: "PolylineMaps",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GearId",
                table: "Activities",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "MetaAthleteId",
                table: "Activities",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Polyline",
                table: "Activities",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MetaAthlete",
                table: "MetaAthlete",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_MetaAthleteId",
                table: "Activities",
                column: "MetaAthleteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_MetaAthlete_MetaAthleteId",
                table: "Activities",
                column: "MetaAthleteId",
                principalTable: "MetaAthlete",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lap_MetaAthlete_AthleteId",
                table: "Lap",
                column: "AthleteId",
                principalTable: "MetaAthlete",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_MetaAthlete_MetaAthleteId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Lap_MetaAthlete_AthleteId",
                table: "Lap");

            migrationBuilder.DropIndex(
                name: "IX_Activities_MetaAthleteId",
                table: "Activities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MetaAthlete",
                table: "MetaAthlete");

            migrationBuilder.DropColumn(
                name: "ActivityId",
                table: "PolylineMaps");

            migrationBuilder.DropColumn(
                name: "MetaAthleteId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "Polyline",
                table: "Activities");

            migrationBuilder.RenameTable(
                name: "MetaAthlete",
                newName: "Athletes");

            migrationBuilder.AlterColumn<string>(
                name: "GearId",
                table: "Activities",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Athletes",
                table: "Athletes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "SummaryGears",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Distance = table.Column<float>(type: "REAL", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Primary = table.Column<bool>(type: "INTEGER", nullable: true),
                    ResourceState = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SummaryGears", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_AthleteId",
                table: "Activities",
                column: "AthleteId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_GearId",
                table: "Activities",
                column: "GearId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Athletes_AthleteId",
                table: "Activities",
                column: "AthleteId",
                principalTable: "Athletes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_SummaryGears_GearId",
                table: "Activities",
                column: "GearId",
                principalTable: "SummaryGears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lap_Athletes_AthleteId",
                table: "Lap",
                column: "AthleteId",
                principalTable: "Athletes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

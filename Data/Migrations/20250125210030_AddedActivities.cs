using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedActivities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MetaActivity",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetaActivity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MetaAthlete",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetaAthlete", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PolylineMap",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Polyline = table.Column<string>(type: "TEXT", nullable: false),
                    SummaryPolyline = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolylineMap", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SummaryGear",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    ResourceState = table.Column<int>(type: "INTEGER", nullable: true),
                    Primary = table.Column<bool>(type: "INTEGER", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Distance = table.Column<float>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SummaryGear", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ExternalId = table.Column<string>(type: "TEXT", nullable: false),
                    UploadId = table.Column<long>(type: "INTEGER", nullable: true),
                    AthleteId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Distance = table.Column<float>(type: "REAL", nullable: true),
                    MovingTime = table.Column<int>(type: "INTEGER", nullable: true),
                    ElapsedTime = table.Column<int>(type: "INTEGER", nullable: true),
                    TotalElevationGain = table.Column<float>(type: "REAL", nullable: true),
                    ElevHigh = table.Column<float>(type: "REAL", nullable: true),
                    ElevLow = table.Column<float>(type: "REAL", nullable: true),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    StartDateLocal = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Timezone = table.Column<string>(type: "TEXT", nullable: false),
                    StartLatlng = table.Column<string>(type: "TEXT", nullable: false),
                    EndLatlng = table.Column<string>(type: "TEXT", nullable: false),
                    AchievementCount = table.Column<int>(type: "INTEGER", nullable: true),
                    KudosCount = table.Column<int>(type: "INTEGER", nullable: true),
                    CommentCount = table.Column<int>(type: "INTEGER", nullable: true),
                    AthleteCount = table.Column<int>(type: "INTEGER", nullable: true),
                    PhotoCount = table.Column<int>(type: "INTEGER", nullable: true),
                    TotalPhotoCount = table.Column<int>(type: "INTEGER", nullable: true),
                    MapId = table.Column<string>(type: "TEXT", nullable: true),
                    Trainer = table.Column<bool>(type: "INTEGER", nullable: true),
                    Commute = table.Column<bool>(type: "INTEGER", nullable: true),
                    Manual = table.Column<bool>(type: "INTEGER", nullable: true),
                    _Private = table.Column<bool>(type: "INTEGER", nullable: true),
                    Flagged = table.Column<bool>(type: "INTEGER", nullable: true),
                    WorkoutType = table.Column<int>(type: "INTEGER", nullable: true),
                    AverageSpeed = table.Column<float>(type: "REAL", nullable: true),
                    MaxSpeed = table.Column<float>(type: "REAL", nullable: true),
                    HasKudoed = table.Column<bool>(type: "INTEGER", nullable: true),
                    GearId = table.Column<string>(type: "TEXT", nullable: false),
                    Kilojoules = table.Column<float>(type: "REAL", nullable: true),
                    AverageWatts = table.Column<float>(type: "REAL", nullable: true),
                    DeviceWatts = table.Column<bool>(type: "INTEGER", nullable: true),
                    MaxWatts = table.Column<int>(type: "INTEGER", nullable: true),
                    WeightedAverageWatts = table.Column<int>(type: "INTEGER", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Calories = table.Column<float>(type: "REAL", nullable: true),
                    DeviceName = table.Column<string>(type: "TEXT", nullable: false),
                    EmbedToken = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Activities_MetaAthlete_AthleteId",
                        column: x => x.AthleteId,
                        principalTable: "MetaAthlete",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Activities_PolylineMap_MapId",
                        column: x => x.MapId,
                        principalTable: "PolylineMap",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Activities_SummaryGear_GearId",
                        column: x => x.GearId,
                        principalTable: "SummaryGear",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lap",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ActivityId = table.Column<long>(type: "INTEGER", nullable: false),
                    AthleteId = table.Column<int>(type: "INTEGER", nullable: false),
                    AverageCadence = table.Column<float>(type: "REAL", nullable: true),
                    AverageSpeed = table.Column<float>(type: "REAL", nullable: true),
                    Distance = table.Column<float>(type: "REAL", nullable: true),
                    ElapsedTime = table.Column<int>(type: "INTEGER", nullable: true),
                    StartIndex = table.Column<int>(type: "INTEGER", nullable: true),
                    EndIndex = table.Column<int>(type: "INTEGER", nullable: true),
                    LapIndex = table.Column<int>(type: "INTEGER", nullable: true),
                    MaxSpeed = table.Column<float>(type: "REAL", nullable: true),
                    MovingTime = table.Column<int>(type: "INTEGER", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    PaceZone = table.Column<int>(type: "INTEGER", nullable: true),
                    Split = table.Column<int>(type: "INTEGER", nullable: true),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    StartDateLocal = table.Column<DateTime>(type: "TEXT", nullable: true),
                    TotalElevationGain = table.Column<float>(type: "REAL", nullable: true),
                    DetailedActivityId = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lap", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lap_Activities_DetailedActivityId",
                        column: x => x.DetailedActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Lap_MetaActivity_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "MetaActivity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lap_MetaAthlete_AthleteId",
                        column: x => x.AthleteId,
                        principalTable: "MetaAthlete",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_AthleteId",
                table: "Activities",
                column: "AthleteId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_GearId",
                table: "Activities",
                column: "GearId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_MapId",
                table: "Activities",
                column: "MapId");

            migrationBuilder.CreateIndex(
                name: "IX_Lap_ActivityId",
                table: "Lap",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Lap_AthleteId",
                table: "Lap",
                column: "AthleteId");

            migrationBuilder.CreateIndex(
                name: "IX_Lap_DetailedActivityId",
                table: "Lap",
                column: "DetailedActivityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lap");

            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "MetaActivity");

            migrationBuilder.DropTable(
                name: "MetaAthlete");

            migrationBuilder.DropTable(
                name: "PolylineMap");

            migrationBuilder.DropTable(
                name: "SummaryGear");
        }
    }
}

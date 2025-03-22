using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
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
                name: "MetaAthletes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetaAthletes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PolylineMaps",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    ActivityId = table.Column<long>(type: "INTEGER", nullable: true),
                    Polyline = table.Column<string>(type: "TEXT", nullable: true),
                    SummaryPolyline = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolylineMaps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SummarySegmentEffort",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ElapsedTime = table.Column<int>(type: "INTEGER", nullable: true),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    StartDateLocal = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Distance = table.Column<float>(type: "REAL", nullable: true),
                    IsKom = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SummarySegmentEffort", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    StravaId = table.Column<int>(type: "INTEGER", nullable: false),
                    Secret = table.Column<string>(type: "TEXT", nullable: false),
                    AccessToken = table.Column<string>(type: "TEXT", nullable: false),
                    RefreshToken = table.Column<string>(type: "TEXT", nullable: false),
                    AccessTokenExpiresAt = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AthleteId = table.Column<int>(type: "INTEGER", nullable: false),
                    ExternalId = table.Column<string>(type: "TEXT", nullable: false),
                    UploadId = table.Column<long>(type: "INTEGER", nullable: true),
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
                    Polyline = table.Column<string>(type: "TEXT", nullable: true),
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
                    GearId = table.Column<string>(type: "TEXT", nullable: true),
                    Kilojoules = table.Column<float>(type: "REAL", nullable: true),
                    AverageWatts = table.Column<float>(type: "REAL", nullable: true),
                    DeviceWatts = table.Column<bool>(type: "INTEGER", nullable: true),
                    MaxWatts = table.Column<int>(type: "INTEGER", nullable: true),
                    WeightedAverageWatts = table.Column<int>(type: "INTEGER", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Calories = table.Column<float>(type: "REAL", nullable: true),
                    DeviceName = table.Column<string>(type: "TEXT", nullable: true),
                    EmbedToken = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Activities_MetaAthletes_AthleteId",
                        column: x => x.AthleteId,
                        principalTable: "MetaAthletes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Activities_PolylineMaps_MapId",
                        column: x => x.MapId,
                        principalTable: "PolylineMaps",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SummarySegment",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ActivityType = table.Column<string>(type: "TEXT", nullable: false),
                    Distance = table.Column<float>(type: "REAL", nullable: true),
                    AverageGrade = table.Column<float>(type: "REAL", nullable: true),
                    MaximumGrade = table.Column<float>(type: "REAL", nullable: true),
                    ElevationHigh = table.Column<float>(type: "REAL", nullable: true),
                    ElevationLow = table.Column<float>(type: "REAL", nullable: true),
                    StartLatlng = table.Column<string>(type: "TEXT", nullable: false),
                    EndLatlng = table.Column<string>(type: "TEXT", nullable: false),
                    ClimbCategory = table.Column<int>(type: "INTEGER", nullable: true),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    State = table.Column<string>(type: "TEXT", nullable: false),
                    Country = table.Column<string>(type: "TEXT", nullable: false),
                    _Private = table.Column<bool>(type: "INTEGER", nullable: true),
                    AthletePrEffortId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SummarySegment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SummarySegment_SummarySegmentEffort_AthletePrEffortId",
                        column: x => x.AthletePrEffortId,
                        principalTable: "SummarySegmentEffort",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lap",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DetailedActivityId = table.Column<long>(type: "INTEGER", nullable: false),
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
                    TotalElevationGain = table.Column<float>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lap", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lap_Activities_DetailedActivityId",
                        column: x => x.DetailedActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SegmentEfforts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ActivityId = table.Column<long>(type: "INTEGER", nullable: true),
                    ElapsedTime = table.Column<int>(type: "INTEGER", nullable: true),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    StartDateLocal = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Distance = table.Column<float>(type: "REAL", nullable: true),
                    IsKom = table.Column<bool>(type: "INTEGER", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    MovingTime = table.Column<int>(type: "INTEGER", nullable: true),
                    StartIndex = table.Column<int>(type: "INTEGER", nullable: true),
                    EndIndex = table.Column<int>(type: "INTEGER", nullable: true),
                    AverageCadence = table.Column<float>(type: "REAL", nullable: true),
                    AverageWatts = table.Column<float>(type: "REAL", nullable: true),
                    DeviceWatts = table.Column<bool>(type: "INTEGER", nullable: true),
                    AverageHeartrate = table.Column<float>(type: "REAL", nullable: true),
                    MaxHeartrate = table.Column<float>(type: "REAL", nullable: true),
                    SegmentId = table.Column<long>(type: "INTEGER", nullable: false),
                    KomRank = table.Column<int>(type: "INTEGER", nullable: true),
                    PrRank = table.Column<int>(type: "INTEGER", nullable: true),
                    Hidden = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SegmentEfforts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SegmentEfforts_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SegmentEfforts_SummarySegment_SegmentId",
                        column: x => x.SegmentId,
                        principalTable: "SummarySegment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_AthleteId",
                table: "Activities",
                column: "AthleteId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_MapId",
                table: "Activities",
                column: "MapId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lap_DetailedActivityId",
                table: "Lap",
                column: "DetailedActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_SegmentEfforts_ActivityId",
                table: "SegmentEfforts",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_SegmentEfforts_SegmentId",
                table: "SegmentEfforts",
                column: "SegmentId");

            migrationBuilder.CreateIndex(
                name: "IX_SummarySegment_AthletePrEffortId",
                table: "SummarySegment",
                column: "AthletePrEffortId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lap");

            migrationBuilder.DropTable(
                name: "MetaActivity");

            migrationBuilder.DropTable(
                name: "SegmentEfforts");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "SummarySegment");

            migrationBuilder.DropTable(
                name: "MetaAthletes");

            migrationBuilder.DropTable(
                name: "PolylineMaps");

            migrationBuilder.DropTable(
                name: "SummarySegmentEffort");
        }
    }
}

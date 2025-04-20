using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class StreamTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Streams",
                columns: table => new
                {
                    StreamSetId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ActivityId = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Streams", x => x.StreamSetId);
                    table.ForeignKey(
                        name: "FK_Streams_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AltitudeStreams",
                columns: table => new
                {
                    AltitudeStreamId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StreamSetId = table.Column<int>(type: "INTEGER", nullable: true),
                    OriginalSize = table.Column<int>(type: "INTEGER", nullable: true),
                    Resolution = table.Column<string>(type: "TEXT", nullable: false),
                    SeriesType = table.Column<string>(type: "TEXT", nullable: false),
                    Data = table.Column<string>(type: "TEXT", nullable: false),
                    StreamSetId1 = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AltitudeStreams", x => x.AltitudeStreamId);
                    table.ForeignKey(
                        name: "FK_AltitudeStreams_Streams_StreamSetId",
                        column: x => x.StreamSetId,
                        principalTable: "Streams",
                        principalColumn: "StreamSetId");
                    table.ForeignKey(
                        name: "FK_AltitudeStreams_Streams_StreamSetId1",
                        column: x => x.StreamSetId1,
                        principalTable: "Streams",
                        principalColumn: "StreamSetId");
                });

            migrationBuilder.CreateTable(
                name: "CadenceStreams",
                columns: table => new
                {
                    CadenceStreamId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StreamSetId = table.Column<int>(type: "INTEGER", nullable: true),
                    OriginalSize = table.Column<int>(type: "INTEGER", nullable: true),
                    Resolution = table.Column<string>(type: "TEXT", nullable: false),
                    SeriesType = table.Column<string>(type: "TEXT", nullable: false),
                    Data = table.Column<string>(type: "TEXT", nullable: false),
                    StreamSetId1 = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CadenceStreams", x => x.CadenceStreamId);
                    table.ForeignKey(
                        name: "FK_CadenceStreams_Streams_StreamSetId",
                        column: x => x.StreamSetId,
                        principalTable: "Streams",
                        principalColumn: "StreamSetId");
                    table.ForeignKey(
                        name: "FK_CadenceStreams_Streams_StreamSetId1",
                        column: x => x.StreamSetId1,
                        principalTable: "Streams",
                        principalColumn: "StreamSetId");
                });

            migrationBuilder.CreateTable(
                name: "DistanceStreams",
                columns: table => new
                {
                    DistanceStreamId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StreamSetId = table.Column<int>(type: "INTEGER", nullable: true),
                    OriginalSize = table.Column<int>(type: "INTEGER", nullable: true),
                    Resolution = table.Column<string>(type: "TEXT", nullable: false),
                    SeriesType = table.Column<string>(type: "TEXT", nullable: false),
                    Data = table.Column<string>(type: "TEXT", nullable: false),
                    StreamSetId1 = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistanceStreams", x => x.DistanceStreamId);
                    table.ForeignKey(
                        name: "FK_DistanceStreams_Streams_StreamSetId",
                        column: x => x.StreamSetId,
                        principalTable: "Streams",
                        principalColumn: "StreamSetId");
                    table.ForeignKey(
                        name: "FK_DistanceStreams_Streams_StreamSetId1",
                        column: x => x.StreamSetId1,
                        principalTable: "Streams",
                        principalColumn: "StreamSetId");
                });

            migrationBuilder.CreateTable(
                name: "HeartrateStreams",
                columns: table => new
                {
                    HeartrateStreamId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StreamSetId = table.Column<int>(type: "INTEGER", nullable: true),
                    OriginalSize = table.Column<int>(type: "INTEGER", nullable: true),
                    Resolution = table.Column<string>(type: "TEXT", nullable: false),
                    SeriesType = table.Column<string>(type: "TEXT", nullable: false),
                    Data = table.Column<string>(type: "TEXT", nullable: false),
                    StreamSetId1 = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeartrateStreams", x => x.HeartrateStreamId);
                    table.ForeignKey(
                        name: "FK_HeartrateStreams_Streams_StreamSetId",
                        column: x => x.StreamSetId,
                        principalTable: "Streams",
                        principalColumn: "StreamSetId");
                    table.ForeignKey(
                        name: "FK_HeartrateStreams_Streams_StreamSetId1",
                        column: x => x.StreamSetId1,
                        principalTable: "Streams",
                        principalColumn: "StreamSetId");
                });

            migrationBuilder.CreateTable(
                name: "LatLngStreams",
                columns: table => new
                {
                    LatLngStreamId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StreamSetId = table.Column<int>(type: "INTEGER", nullable: true),
                    OriginalSize = table.Column<int>(type: "INTEGER", nullable: true),
                    Resolution = table.Column<string>(type: "TEXT", nullable: false),
                    SeriesType = table.Column<string>(type: "TEXT", nullable: false),
                    Data = table.Column<string>(type: "TEXT", nullable: false),
                    StreamSetId1 = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LatLngStreams", x => x.LatLngStreamId);
                    table.ForeignKey(
                        name: "FK_LatLngStreams_Streams_StreamSetId",
                        column: x => x.StreamSetId,
                        principalTable: "Streams",
                        principalColumn: "StreamSetId");
                    table.ForeignKey(
                        name: "FK_LatLngStreams_Streams_StreamSetId1",
                        column: x => x.StreamSetId1,
                        principalTable: "Streams",
                        principalColumn: "StreamSetId");
                });

            migrationBuilder.CreateTable(
                name: "MovingStreams",
                columns: table => new
                {
                    MovingStreamId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StreamSetId = table.Column<int>(type: "INTEGER", nullable: true),
                    OriginalSize = table.Column<int>(type: "INTEGER", nullable: true),
                    Resolution = table.Column<string>(type: "TEXT", nullable: false),
                    SeriesType = table.Column<string>(type: "TEXT", nullable: false),
                    Data = table.Column<string>(type: "TEXT", nullable: false),
                    StreamSetId1 = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovingStreams", x => x.MovingStreamId);
                    table.ForeignKey(
                        name: "FK_MovingStreams_Streams_StreamSetId",
                        column: x => x.StreamSetId,
                        principalTable: "Streams",
                        principalColumn: "StreamSetId");
                    table.ForeignKey(
                        name: "FK_MovingStreams_Streams_StreamSetId1",
                        column: x => x.StreamSetId1,
                        principalTable: "Streams",
                        principalColumn: "StreamSetId");
                });

            migrationBuilder.CreateTable(
                name: "PowerStreams",
                columns: table => new
                {
                    PowerStreamId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StreamSetId = table.Column<int>(type: "INTEGER", nullable: true),
                    OriginalSize = table.Column<int>(type: "INTEGER", nullable: true),
                    Resolution = table.Column<string>(type: "TEXT", nullable: false),
                    SeriesType = table.Column<string>(type: "TEXT", nullable: false),
                    Data = table.Column<string>(type: "TEXT", nullable: false),
                    StreamSetId1 = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PowerStreams", x => x.PowerStreamId);
                    table.ForeignKey(
                        name: "FK_PowerStreams_Streams_StreamSetId",
                        column: x => x.StreamSetId,
                        principalTable: "Streams",
                        principalColumn: "StreamSetId");
                    table.ForeignKey(
                        name: "FK_PowerStreams_Streams_StreamSetId1",
                        column: x => x.StreamSetId1,
                        principalTable: "Streams",
                        principalColumn: "StreamSetId");
                });

            migrationBuilder.CreateTable(
                name: "SmoothGradeStreams",
                columns: table => new
                {
                    SmoothGradeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StreamSetId = table.Column<int>(type: "INTEGER", nullable: true),
                    OriginalSize = table.Column<int>(type: "INTEGER", nullable: true),
                    Resolution = table.Column<string>(type: "TEXT", nullable: false),
                    SeriesType = table.Column<string>(type: "TEXT", nullable: false),
                    Data = table.Column<string>(type: "TEXT", nullable: false),
                    StreamSetId1 = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmoothGradeStreams", x => x.SmoothGradeId);
                    table.ForeignKey(
                        name: "FK_SmoothGradeStreams_Streams_StreamSetId",
                        column: x => x.StreamSetId,
                        principalTable: "Streams",
                        principalColumn: "StreamSetId");
                    table.ForeignKey(
                        name: "FK_SmoothGradeStreams_Streams_StreamSetId1",
                        column: x => x.StreamSetId1,
                        principalTable: "Streams",
                        principalColumn: "StreamSetId");
                });

            migrationBuilder.CreateTable(
                name: "SmoothVelocityStreams",
                columns: table => new
                {
                    SmoothVelocityStreamId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StreamSetId = table.Column<int>(type: "INTEGER", nullable: true),
                    OriginalSize = table.Column<int>(type: "INTEGER", nullable: true),
                    Resolution = table.Column<string>(type: "TEXT", nullable: false),
                    SeriesType = table.Column<string>(type: "TEXT", nullable: false),
                    Data = table.Column<string>(type: "TEXT", nullable: false),
                    StreamSetId1 = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmoothVelocityStreams", x => x.SmoothVelocityStreamId);
                    table.ForeignKey(
                        name: "FK_SmoothVelocityStreams_Streams_StreamSetId",
                        column: x => x.StreamSetId,
                        principalTable: "Streams",
                        principalColumn: "StreamSetId");
                    table.ForeignKey(
                        name: "FK_SmoothVelocityStreams_Streams_StreamSetId1",
                        column: x => x.StreamSetId1,
                        principalTable: "Streams",
                        principalColumn: "StreamSetId");
                });

            migrationBuilder.CreateTable(
                name: "TemperatureStreams",
                columns: table => new
                {
                    TemperatureStreamId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StreamSetId = table.Column<int>(type: "INTEGER", nullable: true),
                    OriginalSize = table.Column<int>(type: "INTEGER", nullable: true),
                    Resolution = table.Column<string>(type: "TEXT", nullable: false),
                    SeriesType = table.Column<string>(type: "TEXT", nullable: false),
                    Data = table.Column<string>(type: "TEXT", nullable: false),
                    StreamSetId1 = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemperatureStreams", x => x.TemperatureStreamId);
                    table.ForeignKey(
                        name: "FK_TemperatureStreams_Streams_StreamSetId",
                        column: x => x.StreamSetId,
                        principalTable: "Streams",
                        principalColumn: "StreamSetId");
                    table.ForeignKey(
                        name: "FK_TemperatureStreams_Streams_StreamSetId1",
                        column: x => x.StreamSetId1,
                        principalTable: "Streams",
                        principalColumn: "StreamSetId");
                });

            migrationBuilder.CreateTable(
                name: "TimeStreams",
                columns: table => new
                {
                    TimeStreamId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StreamSetId = table.Column<int>(type: "INTEGER", nullable: true),
                    OriginalSize = table.Column<int>(type: "INTEGER", nullable: true),
                    Resolution = table.Column<string>(type: "TEXT", nullable: false),
                    SeriesType = table.Column<string>(type: "TEXT", nullable: false),
                    Data = table.Column<string>(type: "TEXT", nullable: false),
                    StreamSetId1 = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeStreams", x => x.TimeStreamId);
                    table.ForeignKey(
                        name: "FK_TimeStreams_Streams_StreamSetId",
                        column: x => x.StreamSetId,
                        principalTable: "Streams",
                        principalColumn: "StreamSetId");
                    table.ForeignKey(
                        name: "FK_TimeStreams_Streams_StreamSetId1",
                        column: x => x.StreamSetId1,
                        principalTable: "Streams",
                        principalColumn: "StreamSetId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AltitudeStreams_StreamSetId",
                table: "AltitudeStreams",
                column: "StreamSetId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AltitudeStreams_StreamSetId1",
                table: "AltitudeStreams",
                column: "StreamSetId1",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CadenceStreams_StreamSetId",
                table: "CadenceStreams",
                column: "StreamSetId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CadenceStreams_StreamSetId1",
                table: "CadenceStreams",
                column: "StreamSetId1",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DistanceStreams_StreamSetId",
                table: "DistanceStreams",
                column: "StreamSetId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DistanceStreams_StreamSetId1",
                table: "DistanceStreams",
                column: "StreamSetId1",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HeartrateStreams_StreamSetId",
                table: "HeartrateStreams",
                column: "StreamSetId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HeartrateStreams_StreamSetId1",
                table: "HeartrateStreams",
                column: "StreamSetId1",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LatLngStreams_StreamSetId",
                table: "LatLngStreams",
                column: "StreamSetId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LatLngStreams_StreamSetId1",
                table: "LatLngStreams",
                column: "StreamSetId1",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MovingStreams_StreamSetId",
                table: "MovingStreams",
                column: "StreamSetId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MovingStreams_StreamSetId1",
                table: "MovingStreams",
                column: "StreamSetId1",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PowerStreams_StreamSetId",
                table: "PowerStreams",
                column: "StreamSetId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PowerStreams_StreamSetId1",
                table: "PowerStreams",
                column: "StreamSetId1",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SmoothGradeStreams_StreamSetId",
                table: "SmoothGradeStreams",
                column: "StreamSetId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SmoothGradeStreams_StreamSetId1",
                table: "SmoothGradeStreams",
                column: "StreamSetId1",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SmoothVelocityStreams_StreamSetId",
                table: "SmoothVelocityStreams",
                column: "StreamSetId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SmoothVelocityStreams_StreamSetId1",
                table: "SmoothVelocityStreams",
                column: "StreamSetId1",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Streams_ActivityId",
                table: "Streams",
                column: "ActivityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TemperatureStreams_StreamSetId",
                table: "TemperatureStreams",
                column: "StreamSetId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TemperatureStreams_StreamSetId1",
                table: "TemperatureStreams",
                column: "StreamSetId1",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimeStreams_StreamSetId",
                table: "TimeStreams",
                column: "StreamSetId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimeStreams_StreamSetId1",
                table: "TimeStreams",
                column: "StreamSetId1",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AltitudeStreams");

            migrationBuilder.DropTable(
                name: "CadenceStreams");

            migrationBuilder.DropTable(
                name: "DistanceStreams");

            migrationBuilder.DropTable(
                name: "HeartrateStreams");

            migrationBuilder.DropTable(
                name: "LatLngStreams");

            migrationBuilder.DropTable(
                name: "MovingStreams");

            migrationBuilder.DropTable(
                name: "PowerStreams");

            migrationBuilder.DropTable(
                name: "SmoothGradeStreams");

            migrationBuilder.DropTable(
                name: "SmoothVelocityStreams");

            migrationBuilder.DropTable(
                name: "TemperatureStreams");

            migrationBuilder.DropTable(
                name: "TimeStreams");

            migrationBuilder.DropTable(
                name: "Streams");
        }
    }
}

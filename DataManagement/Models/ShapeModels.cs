namespace DataManagement.Models
{
    public class Point(double x, double y)
    {
        public double X { get; set; } = x;
        public double Y { get; set; } = y;
    }

    public class RecognitionResult(string name, double score)
    {
        public string Name { get; set; } = name;
        public double Score { get; set; } = score;
    }

    public class BoundingBox(double minX, double minY, double maxX, double maxY)
    {
        public double MinX { get; set; } = minX;
        public double MinY { get; set; } = minY;
        public double MaxX { get; set; } = maxX;
        public double MaxY { get; set; } = maxY;
    }
}

using DataManagement.Models;

namespace DataManagement.BusinessLogic
{
    public class ShapeRecognizer
    {
        private const int NumPoints = 64;
        private const double SquareSize = 250.0;

        private readonly List<List<Point>> Templates = [];
        private readonly List<string> TemplateNames = [];

        // Add a template shape for recognition
        public void AddTemplate(string name, List<Point> points)
        {
            points = Resample(points, NumPoints);
            points = RotateToZero(points);
            points = ScaleToSquare(points, SquareSize);
            points = TranslateToOrigin(points);

            Templates.Add(points);
            TemplateNames.Add(name);
        }

        // Compare an input shape against the templates and return recognition results
        public List<RecognitionResult> Recognize(List<Point> points)
        {
            List<RecognitionResult> results = [];
            points = Resample(points, NumPoints);
            points = RotateToZero(points);
            points = ScaleToSquare(points, SquareSize);
            points = TranslateToOrigin(points);

            for (int i = 0; i < Templates.Count; i++)
            {
                double distance = CalcPathDistance(points, Templates[i]);
                double score = 1.0 - (distance / (0.5 * Math.Sqrt(SquareSize * SquareSize + SquareSize * SquareSize)));

                results.Add(new RecognitionResult(TemplateNames[i], Math.Max(score, 0.0)));
            }

            return results;
        }

        private static List<Point> Resample(List<Point> points, int n)
        {
            double pathLength = CalcPathLength(points);
            double interval = pathLength / (n - 1);
            double accumulatedDistance = 0;

            List<Point> newPoints = [new Point(points[0].X, points[0].Y)];

            for (int i = 1; i < points.Count; i++)
            {
                double segmentLength = CalcDistance(points[i - 1], points[i]);

                if (accumulatedDistance + segmentLength >= interval)
                {
                    double remainingDistance = interval - accumulatedDistance;
                    double ratio = remainingDistance / segmentLength;

                    Point newPoint = new(
                        points[i - 1].X + ratio * (points[i].X - points[i - 1].X),
                        points[i - 1].Y + ratio * (points[i].Y - points[i - 1].Y)
                    );

                    newPoints.Add(newPoint);
                    points.Insert(i, newPoint);
                    accumulatedDistance = 0;
                }
                else
                {
                    accumulatedDistance += segmentLength;
                }
            }

            // Sometimes we fall short due to rounding errors
            if (newPoints.Count == n - 1)
            {
                newPoints.Add(new Point(points[^1].X, points[^1].Y));
            }

            return newPoints;
        }

        private static List<Point> RotateToZero(List<Point> points)
        {
            Point centroid = CalcCentroid(points);
            double angle = Math.Atan2(centroid.Y - points[0].Y, centroid.X - points[0].X);
            return RotateBy(points, -angle);
        }

        private static List<Point> RotateBy(List<Point> points, double angle)
        {
            Point centroid = CalcCentroid(points);
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            List<Point> newPoints = [];
            foreach (Point p in points)
            {
                double qx = (p.X - centroid.X) * cos - (p.Y - centroid.Y) * sin + centroid.X;
                double qy = (p.X - centroid.X) * sin + (p.Y - centroid.Y) * cos + centroid.Y;
                newPoints.Add(new Point(qx, qy));
            }

            return newPoints;
        }

        private static List<Point> ScaleToSquare(List<Point> points, double size)
        {
            BoundingBox box = GetBoundingBox(points);
            double width = box.MaxX - box.MinX;
            double height = box.MaxY - box.MinY;

            List<Point> newPoints = [];
            foreach (Point p in points)
            {
                double qx = p.X * (size / width);
                double qy = p.Y * (size / height);
                newPoints.Add(new Point(qx, qy));
            }

            return newPoints;
        }

        private static List<Point> TranslateToOrigin(List<Point> points)
        {
            Point centroid = CalcCentroid(points);

            List<Point> newPoints = [];
            foreach (var p in points)
            {
                newPoints.Add(new Point(p.X - centroid.X, p.Y - centroid.Y));
            }

            return newPoints;
        }

        private static double CalcPathLength(List<Point> points)
        {
            double length = 0;
            for (int i = 1; i < points.Count; i++)
            {
                length += CalcDistance(points[i - 1], points[i]);
            }
            return length;
        }

        private static double CalcDistance(Point p1, Point p2)
        {
            double dx = p2.X - p1.X;
            double dy = p2.Y - p1.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        private static Point CalcCentroid(List<Point> points)
        {
            double sumX = 0, sumY = 0;
            foreach (Point p in points)
            {
                sumX += p.X;
                sumY += p.Y;
            }
            return new Point(sumX / points.Count, sumY / points.Count);
        }

        private static BoundingBox GetBoundingBox(List<Point> points)
        {
            double minX = double.MaxValue, minY = double.MaxValue;
            double maxX = double.MinValue, maxY = double.MinValue;

            foreach (Point p in points)
            {
                minX = Math.Min(minX, p.X);
                minY = Math.Min(minY, p.Y);
                maxX = Math.Max(maxX, p.X);
                maxY = Math.Max(maxY, p.Y);
            }

            BoundingBox box = new(minX, minY, maxX, maxY);

            return box;
        }

        private static double CalcPathDistance(List<Point> path1, List<Point> path2)
        {
            double distance = 0;
            for (int i = 0; i < path1.Count; i++)
            {
                distance += CalcDistance(path1[i], path2[i]);
            }
            return distance / path1.Count;
        }
    }
}
using DataManagement.BusinessLogic;
using DataManagement.Models;

namespace DataManagementTests
{
    public class ShapeManagerTests
    {
        private readonly ShapeRecognizer Sut;
        public ShapeManagerTests()
        {
            Sut = new ShapeRecognizer();
            List<Point> triangle =
            [
                new(0, 0),
                new(100, 0),
                new(50, 100),
                new(0, 0)
            ];
            Sut.AddTemplate("triangle", triangle);
        }

        [Fact]
        public void RecognizeTest()
        {
            // Arrange
            List<Point> testShape =
            [
                new(10, 10),
                new(110, 10),
                new(60, 110),
                new(10, 10)
            ];

            // Act
            RecognitionResult result = Sut.Recognize(testShape);

            // Assert
            Assert.Equal("triangle", result.Name);
            Assert.True(result.Score > 0.8);
        }
    }
}

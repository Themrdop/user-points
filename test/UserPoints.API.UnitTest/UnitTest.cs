using Xunit;
using Moq;
using UserPoints.Models;
using UserPoints.DataAccess;
using UserPoints.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using System;

namespace UserPoints.API.UnitTest
{
    public class UnitTest
    {
        private Mock<IUserPointsProvider> userPointsProvider = new Mock<IUserPointsProvider>();

        [Fact]
        public async Task AddPoints_ReturnTrue_WhenPointsAdded()
        {
            //Arrange
            userPointsProvider.Setup(x => x.AddPoints(It.IsAny<Points>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Points { points = 4, UserId = "test"});

            //Act
            var endPointResult = await UserPointsEndPoints.AddPointsAsync(TestData.TestPoints,
                                                              userPointsProvider.Object,
                                                              default(CancellationToken));

            //Assert
            
            endPointResult.GetOkObjectResultValue().As<Points>().points.Should().Be(4);
            endPointResult.GetOkObjectResultValue().As<Points>().UserId.Should().Be("test");
            endPointResult.GetOkObjectResultStatusCode().Should().Be(200);
        }

        [Fact]
        public async Task RemovePoints_ReturnTrue_WhenPointsAdded()
        {
            //Arrange
            userPointsProvider.Setup(x => x.RemovePoints(It.IsAny<Points>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Points { points = 8, UserId = "test" });

            //Act
            var endPointResult = await UserPointsEndPoints.RemovePointsAsync(TestData.TestPoints,
                                                              userPointsProvider.Object,
                                                              default(CancellationToken));

            //Assert
            endPointResult.GetOkObjectResultValue().As<Points>().points.Should().Be(8);
            endPointResult.GetOkObjectResultValue().As<Points>().UserId.Should().Be("test");
            endPointResult.GetOkObjectResultStatusCode().Should().Be(200);
        }

        [Fact]
        public async Task PointsByUser_ReturnTen_WhenPointsRemoved()
        {
            //Arrange
            userPointsProvider.Setup(x => x.RemovePoints(It.IsAny<Points>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Points { points = 10, UserId = "test" });

            //Act
            var endPointResult = await UserPointsEndPoints.RemovePointsAsync(TestData.TestPoints,
                                                              userPointsProvider.Object,
                                                              default(CancellationToken));

            //Assert
            endPointResult.GetOkObjectResultValue().As<Points>().points.Should().Be(10);
            endPointResult.GetOkObjectResultValue().As<Points>().UserId.Should().Be("test");
            endPointResult.GetOkObjectResultStatusCode().Should().Be(200);
        }

        [Fact]
        public void AddPoints_ThrowsException_WhenPointsAdded()
        {
            //Arrange
            userPointsProvider.Setup(x => x.AddPoints(It.IsAny<Points>(), It.IsAny<CancellationToken>())).ThrowsAsync(new Exception());

            //Act & Assert
            Assert.ThrowsAsync<Exception>(() => UserPointsEndPoints.AddPointsAsync(TestData.TestPoints,
                                                                                   userPointsProvider.Object,
                                                                                   default(CancellationToken)));
        }

        [Fact]
        public void RemovePoints_ThrowsException_WhenPointsAdded()
        {
            //Arrange
            userPointsProvider.Setup(x => x.RemovePoints(It.IsAny<Points>(), It.IsAny<CancellationToken>())).ThrowsAsync(new Exception()); ;

            //Act & Assert
            Assert.ThrowsAsync<Exception>(() => UserPointsEndPoints.RemovePointsAsync(TestData.TestPoints,
                                                                                      userPointsProvider.Object,
                                                                                      default(CancellationToken)));
        }

        [Fact]
        public void PointsByUser_ThrowsException_WhenREquestPointsByUser()
        {
            //Arrange
            userPointsProvider.Setup(x => x.PointsByUser(It.IsAny<string>(), It.IsAny<CancellationToken>())).ThrowsAsync(new Exception());

            //Act & Assert
            Assert.ThrowsAsync<Exception>(() => UserPointsEndPoints.PointsByUserAsync(TestData.TestPoints.UserId,
                                                                                      userPointsProvider.Object,
                                                                                      default(CancellationToken)));
        }
    }
}
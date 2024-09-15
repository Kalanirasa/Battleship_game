using Application.Interfaces;
using BattelShipBackEnd.Controllers;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipTest
{
    public class BattleshipControllerTests
    {
        private readonly Mock<IBattleshipService> _battleshipServiceMock;
        private readonly BattleshipController _controller;

        public BattleshipControllerTests()
        {
            _battleshipServiceMock = new Mock<IBattleshipService>();
            _controller = new BattleshipController(_battleshipServiceMock.Object);
        }

        [Fact]
        public void StartNewGame_ShouldReturnOkWithGame()
        {
            // Arrange
            var game = new Board { HumanShips = new List<Ship>(), MachineShips = new List<Ship>() };
            _battleshipServiceMock.Setup(service => service.StartNewGame()).Returns(game);

            // Act
            var result = _controller.StartNewGame();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(game, okResult.Value);
        }

        [Fact]
        public void TakeTurn_ShouldReturnOkWithResult()
        {
            // Arrange
            var moveRequest = new MoveRequest();
            var moveResult = new MoveResult();
            _battleshipServiceMock.Setup(service => service.TakeTurn(moveRequest)).Returns(moveResult);

            // Act
            var result = _controller.TakeTurn(moveRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(moveResult, okResult.Value);
        }
    }
}

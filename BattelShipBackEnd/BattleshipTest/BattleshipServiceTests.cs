using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipTest
{
    public class BattleshipServiceTests
    {
        private readonly BattleshipService _battleshipService;

        public BattleshipServiceTests()
        {
            _battleshipService = new BattleshipService();
        }

        [Fact]
        public void StartNewGame_ShouldInitializeBoardWithShips()
        {
            // Act
            var board = _battleshipService.StartNewGame();

            // Assert
            Assert.NotNull(board.MachineShips);
            Assert.Equal(3, board.HumanShips.Count);  // Assuming 3 ships are placed
            Assert.Equal(3, board.MachineShips.Count);
        }

        public MoveResult TakeTurn(MoveRequest request)
        {
            try
            {
                var ships = request.IsHuman ? request.MachineShips : request.HumanShips;

                if (request.HumanWaterPositions == null)
                    request.HumanWaterPositions = new List<GridMissPosition>();
                if (request.MachineWaterPositions == null)
                    request.MachineWaterPositions = new List<GridMissPosition>();

                // Find the position that was targeted in the current move
                var position = ships.SelectMany(s => s.Positions ?? new List<GridPosition>())
                                    .FirstOrDefault(p => p.Row == request.Row && p.Column == request.Column);

                if (position != null)
                {
                    position.IsHit = true;

                    // Update the ship's hit/sunk status
                    foreach (var ship in ships.Where(s => s.Positions != null))
                    {
                        ship.SunkSize = ship.Positions.Count(p => p.IsHit);
                        ship.SunkPercentage = (int)(((double)ship.SunkSize / ship.Size) * 100);
                    }
                }
                else
                {
                    if (request.IsHuman)
                    {
                        request.MachineWaterPositions.Add(new GridMissPosition { Column = request.Column, Row = request.Row, IsMiss = true });
                    }
                    else
                    {
                        request.HumanWaterPositions.Add(new GridMissPosition { Column = request.Column, Row = request.Row, IsMiss = true });
                    }
                }

                return new MoveResult
                {
                    updatedHumanShips = request.HumanShips,
                    updatedMachineShips = request.MachineShips,
                    updatedMissMachinePositions = request.MachineWaterPositions,
                    updatedMissHumanPositions = request.HumanWaterPositions,
                    IsGameOver = ships.All(s => s.IsSunk)
                };
            }
            catch (Exception ex)
            {
                return new MoveResult
                {
                    updatedHumanShips = request.HumanShips,
                    updatedMachineShips = request.MachineShips,
                    updatedMissMachinePositions = request.MachineWaterPositions,
                    updatedMissHumanPositions = request.HumanWaterPositions,
                    IsGameOver = false
                };
            }
        }




        [Fact]
        public void TakeTurn_ShouldReturnMiss_IfNoShipAtPosition()
        {
            // Arrange
            var moveRequest = new MoveRequest
            {
                Row = 'A',
                Column = 5,
                IsHuman = true,  // Human player is taking the turn
                HumanShips = new List<Ship>(),
                MachineShips = new List<Ship>(), // No ships placed at this position
                HumanWaterPositions = new List<GridMissPosition>(), // Miss positions for human player
                MachineWaterPositions = new List<GridMissPosition>() // Miss positions for machine
            };

            // Act
            var result = _battleshipService.TakeTurn(moveRequest);

            // Assert
            Assert.Single(result.updatedMissMachinePositions); // Check machine's miss positions (not human)
            Assert.Equal('A', result.updatedMissMachinePositions[0].Row); // The miss should be recorded at 'A'
            Assert.Equal(5, result.updatedMissMachinePositions[0].Column); // The miss should be recorded at column 5
        }

    }
}

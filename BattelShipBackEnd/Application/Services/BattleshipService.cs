using Application.Interfaces;
using Domain.Entities;
using System.Xml.Linq;

public class BattleshipService : IBattleshipService
{
    private Board _gameBoard;
    private static readonly Random _random = new Random();

    public Board StartNewGame()
    {
        _gameBoard = new Board
        {
            HumanShips = PlaceShips(),
            MachineShips = PlaceShips()
        };
        return _gameBoard;
    }

    private List<Ship> PlaceShips()
    {
        List<Ship> ships = new List<Ship>
        {
            new Ship { Name = "Battleship", Size = 5, Positions = new List<GridPosition>() },
            new Ship { Name = "Destroyers1", Size = 4, Positions = new List<GridPosition>() },
            new Ship { Name = "Destroyers2", Size = 4, Positions = new List<GridPosition>() }
        };

        foreach (var ship in ships)
        {
            bool placed = false;

            while (!placed)
            {
                // Generate random starting position
                char startRow = (char)('A' + _random.Next(0, 10)); 
                int startCol = _random.Next(1, 11);

                bool horizontal = _random.Next(0, 2) == 0;

                if (CanPlaceShip(ships, ship, startRow, startCol, horizontal))
                {
                    PlaceShip(ship, startRow, startCol, horizontal);
                    placed = true;
                }
            }
        }

        return ships;
    }

    private bool CanPlaceShip(List<Ship> ships, Ship ship, char startRow, int startCol, bool horizontal)
    {
        for (int i = 0; i < ship.Size; i++)
        {
            char row = horizontal ? startRow : (char)(startRow + i);
            int col = horizontal ? startCol + i : startCol;

            
            if (row > 'J' || col > 10)
                return false;

            // Check if any ship is already placed at this position
            foreach (var s in ships)
            {
                if (s.Positions.Any(p => p.Row == row && p.Column == col))
                    return false;
            }
        }

        return true;
    }

    private void PlaceShip(Ship ship, char startRow, int startCol, bool horizontal)
    {
        for (int i = 0; i < ship.Size; i++)
        {
            char row = horizontal ? startRow : (char)(startRow + i);
            int col = horizontal ? startCol + i : startCol;
            ship.Positions.Add(new GridPosition { Row = row, Column = col, IsHit = false });
        }
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

            // Machine's logic to hit unsunk ships
            if (!request.IsHuman)
            {
                // Find a ship with at least one hit position but not fully sunk
                var targetShip = ships.FirstOrDefault(s => s.Positions.Any(p => p.IsHit) && !s.IsSunk);

                if (targetShip != null)
                {
                    // Find the next position to hit on this ship
                    var nextPosition = targetShip.Positions.FirstOrDefault(p => !p.IsHit);

                    if (nextPosition != null)
                    {
                        request.Row = nextPosition.Row;
                        request.Column = nextPosition.Column;
                    }
                }
            }

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

            // Return the result with the updated ships and miss positions
            return new MoveResult
            {
                updatedHumanShips = request.HumanShips,
                updatedMachineShips = request.MachineShips,
                updatedMissMachinePositions = request.MachineWaterPositions,
                updatedMissHumanPositions = request.HumanWaterPositions,
                IsGameOver = ships.All(s => s.IsSunk)
            };
        } catch(Exception ex)
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


}

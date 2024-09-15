using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MoveResult
    {
        public List<Ship> updatedHumanShips { get; set; }  // Pass Human Ships from frontend
        public List<Ship> updatedMachineShips { get; set; } // Pass Machine Ships from frontend
        public List<GridMissPosition> updatedMissHumanPositions { get; set; }
        public List<GridMissPosition> updatedMissMachinePositions { get; set; }
        public bool IsGameOver { get; set; }
    }
}

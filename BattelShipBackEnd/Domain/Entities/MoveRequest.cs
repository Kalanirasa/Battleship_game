using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MoveRequest
    {
        public char Row { get; set; }
        public int Column { get; set; }
        public bool IsHuman { get; set; }
        public List<Ship> HumanShips { get; set; }  // Pass Human Ships from frontend
        public List<Ship> MachineShips { get; set; } // Pass Machine Ships from frontend
        public List<GridMissPosition>? HumanWaterPositions { get; set; }
        public List<GridMissPosition>? MachineWaterPositions { get; set; } 
       
    }
}

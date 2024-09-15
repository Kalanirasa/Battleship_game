using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Board
    {
        public List<Ship> HumanShips { get; set; }
        public List<Ship> MachineShips { get; set; }
        public bool IsHumanTurn { get; set; } = true;

    }
}

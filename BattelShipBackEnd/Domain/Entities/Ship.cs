using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Ship
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public int SunkSize { get; set; }
        public int SunkPercentage { get; set; }
        public List<GridPosition> Positions { get; set; }
        public bool IsSunk => Positions.All(p => p.IsHit);
    }
}

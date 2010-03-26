using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLifeActors
{
    public class WorldStateEvolution
    {
        public void NextMoment(IEnumerable<Cell> cells)
        {
            
        }
    }

    public class Cell
    {
        public bool IsAlive{ get; private set;}
        public int[] DimensionVectorIndex { get; private set; }

        public Cell(bool isAlive, IEnumerable<int> dimensionIndices)
        {
            IsAlive = isAlive;
            DimensionVectorIndex = dimensionIndices.ToArray();
        }
    }
}

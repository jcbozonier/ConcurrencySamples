using System;

namespace GameOfLife
{
    public class Cell
    {
        private int NeighborCount;
        private bool IsAlive;

        private Cell(bool isAlive)
        {
            IsAlive = isAlive;
        }

        public static Cell CreateDeadCell()
        {
            return new Cell(false);
        }

        public static Cell CreateLiveCell()
        {
            return new Cell(true);
        }

        public void NeighborRevived()
        {
            NeighborCount++;
        }

        public void MomentPassed(Action deathMessage, Action birthMessage)
        {
            if (NeighborCount == 2)
                return;
            else if (NeighborCount == 3 && !IsAlive)
                birthMessage();
            else if(IsAlive)
                deathMessage();
        }
    }
}
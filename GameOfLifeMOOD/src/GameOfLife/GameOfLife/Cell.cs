using System;

namespace GameOfLife
{
    public class Cell
    {
        private int NeighborCount;

        public static Cell CreateDeadCell()
        {
            return new Cell();
        }

        public static Cell CreateLiveCell()
        {
            return new Cell();
        }

        public void MomentPassed(Action deathMessage)
        {
            if(NeighborCount != 3)
                deathMessage();
        }

        public void NeighborRevived()
        {
            NeighborCount++;
        }

        public void MomentPassed(Action deathMessage, Action birthMessage)
        {
            birthMessage();
        }
    }
}
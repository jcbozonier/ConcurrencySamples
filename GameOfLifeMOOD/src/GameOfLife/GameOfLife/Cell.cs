using System;

namespace GameOfLife
{
    public class Cell
    {
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
            deathMessage();
        }
    }
}
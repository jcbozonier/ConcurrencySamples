using System;

namespace GameOfLife
{
    public class Cell
    {
        private int NeighborCount;
        private bool IsAlive;

        private Action BirthMessage;
        private Action DeathMessage;

        private Cell(bool isAlive, Action birthMessage, Action deathMessage)
        {
            IsAlive = isAlive;
            BirthMessage = birthMessage;
            DeathMessage = deathMessage;
        }

        public static Cell CreateDeadCell(Action birthMessage, Action deathMessage)
        {
            return new Cell(false, birthMessage, deathMessage);
        }

        public static Cell CreateLiveCell(Action birthMessage, Action deathMessage)
        {
            return new Cell(true, birthMessage, deathMessage);
        }

        public void NeighborRevived()
        {
            NeighborCount++;
        }

        public void MomentPassed()
        {
            if (NeighborCount == 2)
                return;
            else if (NeighborCount == 3 && !IsAlive)
            {
                IsAlive = true;
                BirthMessage();
            }
            else if(IsAlive)
            {
                IsAlive = false;
                DeathMessage();
            }
        }

        public void ToggleLife()
        {
            IsAlive = !IsAlive;

            if (IsAlive)
                BirthMessage();
            else
                DeathMessage();
        }
    }
}
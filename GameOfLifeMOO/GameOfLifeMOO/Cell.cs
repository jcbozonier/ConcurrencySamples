namespace GameOfLifeMOO
{
    public class Cell : ICellLifeEventChannel, IGodCommands
    {
        private readonly ICellLifeEventChannel LifeEventChannel;
        private int NeighborCount = 0;
        private bool AmIAlive = false;

        public Cell(ICellLifeEventChannel lifeEventChannel)
        {
            LifeEventChannel = lifeEventChannel;
        }

        public void TimePulse()
        {
            if (NeighborCount == 3)
            {
                AmIAlive = true;
                LifeEventChannel.SomeCellWasBorn(this);
                return;
            }

            if ((NeighborCount == 2 && AmIAlive))
            {
                AmIAlive = true;
                LifeEventChannel.SomeCellIsStillAlive(this);
                return;
            }

            AmIAlive = false;
            LifeEventChannel.SomeCellDied(this);
        }

        public void ComeToLife()
        {
            AmIAlive = true;
            LifeEventChannel.SomeCellWasBorn(this);
        }

        public void SomeCellDied(Cell cell)
        {
            NeighborCount--;
        }

        public void SomeCellWasBorn(Cell cell)
        {
            NeighborCount++;
        }

        public void SomeCellIsStillAlive(Cell cell)
        {
            NeighborCount--;
        }
    }
}
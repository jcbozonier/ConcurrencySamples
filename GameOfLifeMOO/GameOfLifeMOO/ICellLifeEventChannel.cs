namespace GameOfLifeMOO
{
    public interface ICellLifeEventChannel
    {
        void SomeCellDied(Cell cell);
        void SomeCellWasBorn(Cell cell);
        void SomeCellIsStillAlive(Cell cell);
    }
}
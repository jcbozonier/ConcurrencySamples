namespace GameOfLifeMOO
{
    public interface IWorldEventChannel
    {
        void DeadCellAt(int x, int y);
        void LiveCellAt(int x, int y);
    }
}
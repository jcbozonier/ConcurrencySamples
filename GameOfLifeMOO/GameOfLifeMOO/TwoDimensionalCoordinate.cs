namespace GameOfLifeMOO
{
    public class TwoDimensionalCoordinate
    {
        private readonly int XIndex;
        private readonly int YIndex;

        public TwoDimensionalCoordinate(int xIndex, int yIndex)
        {
            XIndex = xIndex;
            YIndex = yIndex;
        }

        public int GetXCoordinate()
        {
            return XIndex;
        }

        public int GetYCoordinate()
        {
            return YIndex;
        }
    }
}
using System;
using System.Linq;

namespace GameOfLifeMOO
{
    public class GameOfLifeDemoRunner : IWorldEventChannel
    {
        private bool[,] Grid;
        private int GridColumnCount = 30;
        private int GridRowCount = 30;

        public GameOfLifeDemoRunner()
        {
            Grid = new bool[GridRowCount, GridRowCount];
        }

        public void Start()
        {
            var theWorld = new WorldOfLife(GridRowCount, GridRowCount, this);

            var randomDecider = new Random();
            foreach(var rowIndex in Enumerable.Range(0, GridRowCount))
            {
                foreach(var columnIndex in Enumerable.Range(0, GridColumnCount))
                {
                    var decidingFactor = randomDecider.Next(100);

                    if(decidingFactor <33)
                    {
                        theWorld.CreateLifeAt(rowIndex, columnIndex);
                    }
                }
            }

            while (true)
            {
                theWorld.TimePulse();

                for(var row=0; row<GridRowCount; row++)
                {
                    var line = "";

                    for(var column=0; column<GridColumnCount; column++)
                    {
                        var cellString = Grid[row, column] ? "O" : ".";
                        line += cellString;
                    }

                    Console.WriteLine(line);
                }
            }
        }

        public void DeadCellAt(int x, int y)
        {
            Grid[x, y] = false;
        }

        public void LiveCellAt(int x, int y)
        {
            Grid[x, y] = true;
        }
    }
}
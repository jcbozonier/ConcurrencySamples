using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLifeMOO
{
    public class WorldOfLife : ICellLifeEventChannel
    {
        private readonly IWorldEventChannel IWorldObserverChannel;
        private readonly IEnumerable<Cell> Cells;
        private readonly Dictionary<Cell, TwoDimensionalCoordinate> CoordinateLookup;
        private readonly Dictionary<TwoDimensionalCoordinate, Cell> CellLookup;
        private readonly List<TwoDimensionalCoordinate> CoordinateList;

        public WorldOfLife(int xMax, int yMax, IWorldEventChannel worldObserverChannel)
        {
            IWorldObserverChannel = worldObserverChannel;

            var cells = _GetCells(xMax, yMax);
            CoordinateList = _GetCoordinateList(xMax, yMax);

            CoordinateLookup = new Dictionary<Cell, TwoDimensionalCoordinate>();
            CellLookup = new Dictionary<TwoDimensionalCoordinate, Cell>();
            foreach(var index in Enumerable.Range(0, cells.Count))
            {
                CoordinateLookup.Add(cells[index], CoordinateList[index]);
                CellLookup.Add(CoordinateList[index], cells[index]);
            }

            Cells = cells;

            //_RandomlySeedWorld();
        }

        public void CreateLifeAt(int x, int y)
        {

            var coordinateToCreate =
                CoordinateList.Single(coordinate => coordinate.GetXCoordinate() == x && coordinate.GetYCoordinate() == y);

            CellLookup[coordinateToCreate].ComeToLife();
        }

        public void TimePulse()
        {
            foreach (var cell in Cells)
            {
                cell.TimePulse();
            }
        }

        public void SomeCellWasBorn(Cell cell)
        {
            var coordinates = CoordinateLookup[cell];
            IWorldObserverChannel.LiveCellAt(coordinates.GetXCoordinate(), coordinates.GetYCoordinate());

            var neighboringCells = _GetNeighboringCells(cell);
            foreach (var neighboringCell in neighboringCells)
            {
                neighboringCell.SomeCellWasBorn(cell);
            }
        }

        public void SomeCellIsStillAlive(Cell cell)
        {
            var coordinates = CoordinateLookup[cell];
            IWorldObserverChannel.LiveCellAt(coordinates.GetXCoordinate(), coordinates.GetYCoordinate());

            var neighboringCells = _GetNeighboringCells(cell);
            foreach (var neighboringCell in neighboringCells)
            {
                neighboringCell.SomeCellIsStillAlive(cell);
            }
        }

        public void SomeCellDied(Cell cell)
        {
            var coordinates = CoordinateLookup[cell];
            IWorldObserverChannel.DeadCellAt(coordinates.GetXCoordinate(), coordinates.GetYCoordinate());

            var neighboringCells = _GetNeighboringCells(cell);
            foreach (var neighboringCell in neighboringCells)
            {
                neighboringCell.SomeCellDied(cell);
            }
        }

        private IEnumerable<Cell> _GetNeighboringCells(Cell cell)
        {
            var neighboringCells = new List<Cell>();

            var cellCoordinate = CoordinateLookup[cell];
            foreach (var twoDimensionalCoordinate in CoordinateLookup)
            {
                var potentialNeighbor = twoDimensionalCoordinate.Key;

                if(potentialNeighbor == cell)
                    continue;

                var potentialNeighborLocation = CoordinateLookup[potentialNeighbor];

                var xCoordinateDistance = Math.Abs(potentialNeighborLocation.GetXCoordinate() - cellCoordinate.GetXCoordinate());
                var yCoordinateDistance = Math.Abs(potentialNeighborLocation.GetYCoordinate() - cellCoordinate.GetYCoordinate());

                if(xCoordinateDistance <= 1 && yCoordinateDistance <= 1)
                {
                    neighboringCells.Add(potentialNeighbor);
                }
            }

            return neighboringCells;
        }

        private List<TwoDimensionalCoordinate> _GetCoordinateList(int xMax, int yMax)
        {
            var coordinates = new List<TwoDimensionalCoordinate>();

            for(var xIndex = 0; xIndex < xMax; xIndex++)
            {
                for(var yIndex = 0; yIndex < yMax; yIndex++)
                {
                    coordinates.Add(new TwoDimensionalCoordinate(xIndex, yIndex));
                }
            }

            return coordinates;
        }

        private List<Cell> _GetCells(int xMax, int yMax)
        {
            var totalCellCount = xMax * yMax;
            var randomThing = new Random();
            return Enumerable.Range(0, totalCellCount).Select(index => new Cell(this)).ToList();
        }
    }
}
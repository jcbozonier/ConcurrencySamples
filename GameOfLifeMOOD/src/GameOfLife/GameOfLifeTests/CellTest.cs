using System;
using NUnit.Framework;

namespace GameOfLifeTests
{
    [TestFixture]
    public class CellTest
    {
        [Test]
        public void Given_a_dead_cell_with_no_neighbors_when_a_moment_passes()
        {
            var cellDied = false;
            var cell = Cell.CreateDeadCell();

            cell.MomentPassed(() => cellDied = true);

            //It should have died
            Assert.IsTrue(cellDied, "The cell should have died.");
        }
    }

    public class Cell
    {
        public static Cell CreateDeadCell()
        {
            return new Cell();
        }

        public void MomentPassed(Action deathMessage)
        {
            deathMessage();
        }
    }
}

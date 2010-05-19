using GameOfLife;
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

            Assert.IsTrue(cellDied, "The cell should have died.");
        }

        [Test]
        public void Given_a_live_cell_with_no_neighbors_when_a_moment_passes()
        {
            var cellDied = false;
            var cell = Cell.CreateLiveCell();

            cell.MomentPassed(() => cellDied = true);

            Assert.IsTrue(cellDied, "The cell should have died.");
        }
    }
}

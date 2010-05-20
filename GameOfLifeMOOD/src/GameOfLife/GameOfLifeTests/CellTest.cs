using System;
using System.Linq;
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
            var cell = Cell.CreateDeadCell(()=>{}, () => { });

            cell.MomentPassed();

            Assert.IsFalse(cellDied, "The cell should not have died because it's already dead.");
        }

        [Test]
        public void Given_a_live_cell_with_no_neighbors_when_a_moment_passes()
        {
            var cellDied = false;
            var cell = Cell.CreateLiveCell(()=>{}, () => cellDied = true);

            cell.MomentPassed();

            Assert.IsTrue(cellDied, "The cell should have died.");
        }

        [Test]
        public void Given_a_dead_cell_with_exactly_three_neighbors_when_a_moment_passes()
        {
            var cellDied = false;
            var cellBorn = false;
            var cell = Cell.CreateDeadCell(()=>cellBorn = true, () => { });

            Call(cell.NeighborRevived, 3);

            cell.MomentPassed();

            Assert.IsFalse(cellDied, "The cell should not die.");
            Assert.IsTrue(cellBorn, "The cell should be born.");
        }

        [Test]
        public void Given_a_dead_cell_with_two_neighbors_when_a_moment_passes()
        {
            var cellBorn = false;
            var cellDied = false;
            var cell = Cell.CreateDeadCell(()=>{}, () => { });

            Call(cell.NeighborRevived, 2);

            cell.MomentPassed();

            Assert.IsFalse(cellBorn, "It should not be born.");
            Assert.IsFalse(cellDied, "It should not die.");
        }

        [Test]
        public void Given_a_live_cell_with_one_neighbor_when_a_moment_passes()
        {
            var cellDied = false;
            var cellBorn = false;
            var cell = Cell.CreateLiveCell(()=>cellBorn = true, () => cellDied = true);

            cell.NeighborRevived();

            cell.MomentPassed();

            Assert.IsTrue(cellDied, "The cell should die.");
            Assert.IsFalse(cellBorn, "The cell should not be born.");
        }

        [Test]
        public void Given_a_dead_cell_with_one_neighbor_when_a_moment_passes()
        {
            var cellDied = false;
            var cell = Cell.CreateDeadCell(()=>{}, () => { });

            cell.NeighborRevived();

            cell.MomentPassed();

            Assert.IsFalse(cellDied, "The cell should not die because it's already dead.");
        }

        [Test]
        public void Given_a_live_cell_with_two_neighbors_when_a_moment_passes()
        {
            var cellDied = false;
            var cell = Cell.CreateLiveCell(()=>{}, () => { });

            Call(cell.NeighborRevived, 2);

            cell.MomentPassed();

            Assert.IsFalse(cellDied, "The cell should not die.");
        }

        [Test]
        public void Given_a_live_cell_with_three_neighbors_when_a_moment_passes()
        {
            var cellBorn = false;
            var cell = Cell.CreateLiveCell(()=>{}, () => { });

            Call(cell.NeighborRevived, 3);

            cell.MomentPassed();

            Assert.IsFalse(cellBorn, "It should not be born because it is already alive.");
        }

        [Test]
        public void Given_a_live_cell_with_more_than_three_neighbors_when_a_moment_passes()
        {
            var cellDied = false;
            var cell = Cell.CreateLiveCell(()=>{}, () => cellDied = true);

            Call(cell.NeighborRevived, 4);

            cell.MomentPassed();

            Assert.IsTrue(cellDied, "The cell should die from overcrowding.");
        }

        [Test]
        public void Given_a_dead_cell_with_more_than_three_neighbors_when_a_moment_passes()
        {
            var cellBorn = false;
            var cell = Cell.CreateDeadCell(()=>{}, () => { });

            Call(cell.NeighborRevived, 4);

            cell.MomentPassed();

            Assert.IsFalse(cellBorn, "The cell should not be born.");
        }

        [Test]
        public void Given_a_dead_cell_when_revived_and_then_killed()
        {
            var cellDied = false;
            var cellBorn = false;

            var cell = Cell.CreateDeadCell(() => cellBorn = true, () => cellDied = true);

            Call(cell.NeighborRevived, 3);

            cell.MomentPassed();

            cell.NeighborRevived();

            cell.MomentPassed();

            Assert.IsTrue(cellBorn, "The cell should have been born.");
            Assert.IsTrue(cellDied, "The cell should have died.");
        }

        [Test]
        public void Given_a_live_cell_when_killed_and_then_revived()
        {
            var cellDied = false;
            var cellBorn = false;

            var cell = Cell.CreateLiveCell(()=>cellBorn = true, () => cellDied = true);

            cell.MomentPassed();

            Call(cell.NeighborRevived, 3);

            cell.MomentPassed();

            Assert.IsTrue(cellDied, "The cell should have died.");
            Assert.IsTrue(cellBorn, "The cell should have been born.");
        }

        [Test]
        public void Given_a_dead_cell_when_life_is_toggled()
        {
            var cellBorn = false;

            var cell = Cell.CreateDeadCell(() => cellBorn = true, () => { });
            cell.NeighborRevived();
            cell.NeighborRevived();

            cell.ToggleLife();

            cell.MomentPassed();

            Assert.IsTrue(cellBorn, "The cell should come back to life.");
        }

        [Test]
        public void Given_a_live_cell_when_life_is_toggled()
        {
            var cellDied = false;
            var cellBorn = false;
            var cell = Cell.CreateLiveCell(() => cellBorn = true, () => cellDied = true);
            cell.NeighborRevived();
            cell.NeighborRevived();

            cell.ToggleLife();

            cell.MomentPassed();

            Assert.IsTrue(cellDied, "The cell should be killed.");
        }

        public void Call(Action action, int timesToCall)
        {
            foreach (var index in Enumerable.Range(0, timesToCall))
                action();
        }
    }
}

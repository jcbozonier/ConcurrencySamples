﻿using GameOfLife;
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

            cell.MomentPassed(() => cellDied = true, ()=> { });

            Assert.IsFalse(cellDied, "The cell should not have died because it's already dead.");
        }

        [Test]
        public void Given_a_live_cell_with_no_neighbors_when_a_moment_passes()
        {
            var cellDied = false;
            var cell = Cell.CreateLiveCell();

            cell.MomentPassed(() => cellDied = true, () => { });

            Assert.IsTrue(cellDied, "The cell should have died.");
        }

        [Test]
        public void Given_a_dead_cell_with_exactly_three_neighbors_when_a_moment_passes()
        {
            var cellDied = false;
            var cellBorn = false;
            var cell = Cell.CreateDeadCell();

            cell.NeighborRevived();
            cell.NeighborRevived();
            cell.NeighborRevived();

            cell.MomentPassed(() => cellDied = true, () => cellBorn = true);

            Assert.IsFalse(cellDied, "The cell should not die.");
            Assert.IsTrue(cellBorn, "The cell should be born.");
        }

        [Test]
        public void Given_a_dead_cell_with_two_neighbors_when_a_moment_passes()
        {
            var cellBorn = false;
            var cellDied = false;
            var cell = Cell.CreateDeadCell();

            cell.NeighborRevived();
            cell.NeighborRevived();

            cell.MomentPassed(()=> cellDied = true, ()=>cellBorn = true);

            Assert.IsFalse(cellBorn, "It should not be born.");
            Assert.IsFalse(cellDied, "It should not die.");
        }

        [Test]
        public void Given_a_live_cell_with_one_neighbor_when_a_moment_passes()
        {
            var cellDied = false;
            var cellBorn = false;
            var cell = Cell.CreateLiveCell();

            cell.NeighborRevived();

            cell.MomentPassed(() => cellDied = true, () => cellBorn = true);

            Assert.IsTrue(cellDied, "The cell should die.");
            Assert.IsFalse(cellBorn, "The cell should not be born.");
        }

        [Test]
        public void Given_a_dead_cell_with_one_neighbor_when_a_moment_passes()
        {
            var cellDied = false;
            var cell = Cell.CreateDeadCell();

            cell.NeighborRevived();

            cell.MomentPassed(() => cellDied = true, () => { });

            Assert.IsFalse(cellDied, "The cell should not die because it's already dead.");
        }

        [Test]
        public void Given_a_live_cell_with_two_neighbors_when_a_moment_passes()
        {
            var cellDied = false;
            var cell = Cell.CreateLiveCell();

            cell.NeighborRevived();
            cell.NeighborRevived();

            cell.MomentPassed(() => cellDied = true, () => { });

            Assert.IsFalse(cellDied, "The cell should not die.");
        }

        [Test]
        public void Given_a_live_cell_with_three_neighbors_when_a_moment_passes()
        {
            var cellBorn = false;
            var cell = Cell.CreateLiveCell();

            cell.NeighborRevived();
            cell.NeighborRevived();
            cell.NeighborRevived();

            cell.MomentPassed(()=>{}, ()=>cellBorn = true);

            Assert.IsFalse(cellBorn, "It should not be born because it is already alive.");
        }

        [Test]
        public void Given_a_live_cell_with_more_than_three_neighbors()
        {
            var cellDied = false;
            var cell = Cell.CreateLiveCell();

            cell.NeighborRevived();
            cell.NeighborRevived();
            cell.NeighborRevived();
            cell.NeighborRevived();

            cell.MomentPassed(() => cellDied = true, () => { });

            Assert.IsTrue(cellDied, "The cell should die from overcrowding.");
        }
    }
}

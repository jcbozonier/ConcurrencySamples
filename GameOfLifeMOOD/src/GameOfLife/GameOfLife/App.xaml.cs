using System.Windows;

namespace GameOfLife
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Startup += (sndr, evt) =>
            {
                var cell = Cell.CreateDeadCell(()=>{}, () => { });
                var board = new BoardViewer(()=>cell.ToggleLife());
                board.Show();

                cell.NeighborRevived();
                cell.NeighborRevived();
                cell.NeighborRevived();

                cell.MomentPassed();

                cell.NeighborRevived();
                cell.NeighborRevived();

                cell.MomentPassed();
            };
        }
    }
}

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
                var cell = Cell.CreateDeadCell();
                var board = new BoardViewer();
                board.Show();

                cell.NeighborRevived();
                cell.NeighborRevived();
                cell.NeighborRevived();

                cell.MomentPassed(()=> board.LightenButton() , ()=>board.DarkenButton());

                cell.NeighborRevived();
                cell.NeighborRevived();

                cell.MomentPassed(() => board.LightenButton(), () => board.DarkenButton());
            };
        }
    }
}

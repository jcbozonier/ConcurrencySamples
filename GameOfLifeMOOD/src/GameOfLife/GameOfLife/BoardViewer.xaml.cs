using System;
using System.Windows;
using System.Windows.Media;

namespace GameOfLife
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class BoardViewer : Window
    {
        public BoardViewer()
        {
            InitializeComponent();
        }

        public void DarkenButton()
        {
            this.CellField.Background = Brushes.Black;
        }

        public void LightenButton()
        {
            this.CellField.Background = Brushes.AntiqueWhite;
        }
    }
}

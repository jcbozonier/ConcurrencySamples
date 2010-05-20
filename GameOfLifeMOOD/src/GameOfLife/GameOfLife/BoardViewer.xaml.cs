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
        private Action OnCellClick;

        public BoardViewer()
        {
            InitializeComponent();
        }

        public void OnCellToggle(Action onCellClick)
        {
            OnCellClick = onCellClick;
        }

        public void DarkenButton()
        {
            this.CellField.Background = Brushes.Black;
        }

        public void LightenButton()
        {
            this.CellField.Background = Brushes.AntiqueWhite;
        }

        private void CellField_Click(object sender, RoutedEventArgs e)
        {
            OnCellClick();
        }
    }
}

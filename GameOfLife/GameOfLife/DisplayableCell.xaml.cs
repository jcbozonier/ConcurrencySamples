using System;
using System.Windows;
using System.Windows.Input;

namespace GameOfLife
{
    public class DisplayableCell
    {
        public DisplayableCell(bool isAlive)
        {
            IsAlive = isAlive;
            Diameter = 15;
            CellTouched = new CellTouchedCommand();
        }

        public int Diameter { get; private set; }
        public bool IsAlive { get; private set; }
        public ICommand CellTouched { get; private set; }
    }

    public class CellTouchedCommand : ICommand
    {
        public void Execute(object parameter)
        {
            MessageBox.Show("Works!");
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }
}
using System.Collections.Generic;
using System.ComponentModel;

namespace GameOfLife
{
    public class LifeDemoViewModel : INotifyPropertyChanged
    {
        public DisplayableCell FakeDisplayableCell
        { get
        {
            return new DisplayableCell(false);
        } }

        public List<List<DisplayableCell>> Cells
        {
            get
            {
                return new List<List<DisplayableCell>>()
                           {
                               new List<DisplayableCell>()
                                   {
                                       new DisplayableCell(true), new DisplayableCell(false), new DisplayableCell(true)
                                   },
                               new List<DisplayableCell>()
                                   {
                                       new DisplayableCell(false), new DisplayableCell(true), new DisplayableCell(false)
                                   }
                           };
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
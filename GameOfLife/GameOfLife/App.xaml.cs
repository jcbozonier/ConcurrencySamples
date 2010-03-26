using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using GameOfLife.Timeline;

namespace GameOfLife
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            this.Startup += (x, y) =>
                                {
                                    var window = new TimeLineViewer();
                                    var vm = new LifeDemoViewModel();
                                    window.DataContext = vm;
                                    window.Show();
                                };
        }
    }
}

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Walpaper
{
    /// <summary>
    /// Interaction logic for AddWallpaper.xaml
    /// </summary>
    public partial class AddWallpaper : Window
    {
        public bool Success { get;set; }
        public Wallpaper wallpaper { get; set; }

        public AddWallpaper(Window parentWindow)
        {
            wallpaper = new Wallpaper();
            DataContext = wallpaper;
            Owner = parentWindow;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                wallpaper.Path = openFileDialog.FileName;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Success = true;
            Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

using Microsoft.Win32;
using Org.BouncyCastle.Asn1.BC;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Walpaper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const int SPI_SETDESKWALLPAPER = 20;
        public const int SPIF_UPDATEINFILE = 1;
        public const int SPIF_SENDCHANGE = 2;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        public ObservableCollection<Wallpaper> wallpapers { get; set; }
        public MainWindow()
        {
            DataContext = this;
            wallpapers = new ObservableCollection<Wallpaper>();
            InitializeComponent();
            string currentPath = GetPathOfWallpaper();
            UpdateTable();
        }

        public void ChangeWallpaper(string imagePath)
        {
            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, imagePath, SPIF_UPDATEINFILE | SPIF_SENDCHANGE);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            AddWallpaper addWallpaperWindow = new AddWallpaper(this);
            addWallpaperWindow.ShowDialog();
            if (addWallpaperWindow.Success)
            {
                Wallpaper newWallpaper = addWallpaperWindow.wallpaper;
                AddWallpaper(newWallpaper);
                UpdateTable();
            }
        }
        private string GetPathOfWallpaper()
        {
            string pathWallpaper = "";
            RegistryKey regKey = Registry.CurrentUser.OpenSubKey("Control Panel\\Desktop", false);
            if (regKey != null)
            {
                pathWallpaper = regKey.GetValue("WallPaper").ToString();
                regKey.Close();
            }
            return pathWallpaper;
        }

        private void listWallpapers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Wallpaper toSet = (Wallpaper)listWallpapers.SelectedItem;
            if (toSet != null)
            {
                CurrentImage.Source = new BitmapImage(new Uri(toSet.Path));
                borderImage.Visibility = Visibility.Visible;
                ChangeWallpaper(toSet.Path);
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Wallpaper toDelete = (Wallpaper)listWallpapers.SelectedItem;
            DeleteWallpaper(toDelete);
            UpdateTable();
            borderImage.Visibility = Visibility.Hidden;
        }

        private void AddWallpaper(Wallpaper newWallpaper)
        {
            WallpaperContext context = new WallpaperContext();
            context.Wallpapers.Add(newWallpaper);
            context.SaveChanges();
        }

        private void DeleteWallpaper(Wallpaper toDeleteWallpaper)
        {
            WallpaperContext context = new WallpaperContext();
            context.Wallpapers.Remove(toDeleteWallpaper);
            context.SaveChanges();
        }

        private void UpdateTable()
        {
            WallpaperContext context = new WallpaperContext();
            List<Wallpaper> newList = context.Wallpapers.ToList();
            wallpapers.Clear();
            foreach(Wallpaper wallpaper in newList) 
            {
                wallpapers.Add(wallpaper);
            }
        }
    }
}

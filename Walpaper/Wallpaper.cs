using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Walpaper
{
    public class Wallpaper : INotifyPropertyChanged
    {
        public string Name { get; set; }

        private string _path;
        public string Path
        {
            get { return _path; }
            set
            {
                _path = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Path"));
            }
        }

        public Wallpaper() { }

        public Wallpaper(string name, string path)
        {
            Name = name;
            Path = path;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public override string ToString()
        {
            return Name;
        }

        public void setPath()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string filePath = "";
            if (openFileDialog.ShowDialog() == true)
            {
                Path = openFileDialog.FileName;
            }
        }
    }
}

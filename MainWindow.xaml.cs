using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FakeNews
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonOpen_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private bool OpenFile()
        {
            // Open file dialog
            OpenFileDialog Dlg = new OpenFileDialog();

            // Return if user cancels selection
            if (Dlg.ShowDialog() != true)
            {
                return false;
            }

            // Read file contents
            FileStream File = new FileStream(Dlg.FileName, FileMode.Open);
            string FileString = String.Empty;
            StreamReader Reader = new StreamReader(File);

            try
            {
                FileString = Reader.ReadToEnd();
            }
            catch
            {
                MessageBox.Show("Die Datei konnte nicht eingelesen werden.");
                return false;
            }
            finally
            {
                File.Close();
                Reader.Close();
            }

            // Split file into rows
            string[] Rows = FileString.Split('\n');

            try
            {
                // Parse file
                string[] Substrings = Rows[0].Split(' ');
                int n = int.Parse(Substrings[2]);
                int m = int.Parse(Substrings[3]);
                for(int I = 0; I < n; I++)
                {
                    string[] EdgeSubstrings = Rows[I + 1].Split(' ');

                }
            }
            catch
            {
                MessageBox.Show("Die Datei konnte nicht eingelesen werden!", "Operation fehlgeschlagen", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }
    }
}

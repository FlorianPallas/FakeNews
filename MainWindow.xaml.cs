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
            OpenFile();
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

                List<int>[] _AdjacencyList = new List<int>[n];
                for(int I = 0; I < n; I++)
                {
                    _AdjacencyList[I] = new List<int>();
                }

                for (int I = 0; I < n; I++)
                {
                    string[] EdgeSubstrings = Rows[I + 1].Split(' ');
                    int U = int.Parse(EdgeSubstrings[0]);
                    int V = int.Parse(EdgeSubstrings[1]);
                    _AdjacencyList[U].Add(V);
                    _AdjacencyList[V].Add(U);
                }

                int[][] AdjacencyList = new int[n][];
                int[,] GradArray = new int[n, 2];
                for (int I = 0; I < n; I++)
                {
                    int Count = _AdjacencyList[I].Count;

                    GradArray[I, 0] = I;
                    GradArray[I, 1] = Count;

                    AdjacencyList[I] = new int[Count];
                    for(int J = 0; J < Count; J++)
                    {
                        AdjacencyList[I][J] = _AdjacencyList[I][J];
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Die Datei konnte nicht eingelesen werden!", "Operation fehlgeschlagen", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void SortGradArray()
        {
            int[,] Dummy = new int[10, 2];
            //Array.Sort(Dummy,)
        }

        public struct Vertex
        {
            int Index;
            int UnwatchedDegree;

            Vertex(int _Index, int _UnwatchedDegree)
            {
                Index = _Index;
                UnwatchedDegree = _UnwatchedDegree;
            }
        }
    }
}

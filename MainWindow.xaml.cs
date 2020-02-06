﻿using Microsoft.Win32;
using System;
using System.Collections;
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
        private string FileName;
        private int VertexCount;
        private int EdgeCount;
        private int[][] AdjacencyList;
        private Vertex[] Vertices;
        private bool[] WatchedVertices;

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
            string Result = GetResult();
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
            FileName = Dlg.FileName;
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

            // Parse file
            string[] Substrings = Rows[0].Split(' ');
            VertexCount = int.Parse(Substrings[2]);
            EdgeCount = int.Parse(Substrings[3]);

            List<int>[] _AdjacencyList = new List<int>[VertexCount];
            for(int I = 0; I < VertexCount; I++)
            {
                _AdjacencyList[I] = new List<int>();
            }

            for (int I = 0; I < EdgeCount; I++)
            {
                string[] EdgeSubstrings = Rows[I + 1].Split(' ');
                int U = int.Parse(EdgeSubstrings[0]) - 1;
                int V = int.Parse(EdgeSubstrings[1]) - 1;
                _AdjacencyList[U].Add(V);
                _AdjacencyList[V].Add(U);
            }

            AdjacencyList = new int[VertexCount][];
            Vertices = new Vertex[VertexCount];
            for (int I = 0; I < VertexCount; I++)
            {
                int Count = _AdjacencyList[I].Count;

                Vertices[I] = new Vertex(I, Count);

                AdjacencyList[I] = new int[Count];
                for(int J = 0; J < Count; J++)
                {
                    AdjacencyList[I][J] = _AdjacencyList[I][J];
                }
            }

            Array.Sort(Vertices, new DegreeComparer());

            return true;
        }

        public struct Vertex
        {
            public int Index;
            public int UnwatchedDegree;

            public Vertex(int _Index, int _UnwatchedDegree)
            {
                Index = _Index;
                UnwatchedDegree = _UnwatchedDegree;
            }
        }

        public class DegreeComparer : IComparer<Vertex>
        {
            int IComparer<Vertex>.Compare(Vertex x, Vertex y)
            {
                if(x.UnwatchedDegree > y.UnwatchedDegree)
                {
                    return -1;
                }
                if (x.UnwatchedDegree < y.UnwatchedDegree)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        public string GetResult()
        {
            string Line1 = string.Empty;
            string Line2 = string.Empty;
            int NumberWatched = 0;
            
            for (int i = 0; i < WatchedVertices.Length; i++)
            {
                if (WatchedVertices[i])
                {
                    NumberWatched++;
                    Line2 += " " + i;
                }
            }
            Line1 = NumberWatched.ToString() + "/n";
            return Line1 + Line2;
        }
    }
}

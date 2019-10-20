using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace mkvSubFixer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public class GridColumns
        {
            public string name { get; set; }
        }

        public MainWindow()
        {
            InitializeComponent();
            InitDataGrid();
        }

        private void InitDataGrid()
        {
            DataGridTextColumn col1 = new DataGridTextColumn();

            dgItems.Columns.Add(col1);

            col1.Binding = new Binding("name");

            col1.Header = "Name";
        }


        private void FileBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Matroska Files (*.mkv)|*.mkv",
                Multiselect = true
            };

            if (openFileDialog.ShowDialog() == true)
            {
                var z = openFileDialog.FileNames.ToList().Distinct();

                foreach (var item in z)
                {
                    dgItems.Items.Add(new GridColumns { name = item });
                }
            }

            RemoveDuplicateItems();
        }

        private void RemoveDuplicateItems()
        {
            ItemCollection ic = dgItems.Items;

            List<GridColumns> x = dgItems.Items.Cast<GridColumns>()
                                               .GroupBy(x => x.name)
                                               .SelectMany(x => x.Take(1))
                                               .OrderByDescending(x => x.name)
                                               .ToList();

            dgItems.Items.Clear();

            foreach (var i in x)
            {
                dgItems.Items.Add(new GridColumns { name = i.name });
            }


        }

        private void dgItems_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            e.Cancel = true;
        }

        private void DeleteItems_Click(object sender, RoutedEventArgs e)
        {
            if (dgItems.SelectedItems.Count == 0)
            {
                MessageBox.Show("No items selected");
                return;
            }

            for (int i = dgItems.SelectedItems.Count - 1; i >= 0; i--)
            {
                dgItems.Items.Remove(dgItems.SelectedItems[i]);
            }
        }

        private void StartExtractionProcess_Click(object sender, RoutedEventArgs e)
        {
            if (dgItems.Items.Count == 0)
            {
                MessageBox.Show("No items selected for processing");
                return;
            }

            if (!File.Exists("ffmpeg.exe"))
            {
                MessageBox.Show("Can not find ffmpeg.exe converter");
                return;
            }

            foreach (GridColumns gc in dgItems.Items)
            {
                var fileNamePath = gc.name;
                var fileDir = Path.GetDirectoryName(gc.name);
                var fileNameNoExt = Path.GetFileNameWithoutExtension(gc.name);
                var subtitleFileNamePathExt = fileDir + "\\" + fileNameNoExt + ".srt";
                var ffmpegPath = "ffmpeg.exe";


                var proc = new Process();
                proc.StartInfo.FileName = ffmpegPath;
                proc.StartInfo.Arguments = "-y -i \"" + fileNamePath + "\" -map 0:s:0 \"" + subtitleFileNamePathExt + "\"";
                proc.Start();
                proc.WaitForExit();
                var exitCode = proc.ExitCode;
                proc.Close();
            }
        }

        private void dgItems_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] droppedFiles = (string[])e.Data.GetData(DataFormats.FileDrop);

                AddFilesToGrid(droppedFiles);
            }
            else
            {
                MessageBox.Show("No files dropped");
            }

           
        }

        private void AddFilesToGrid(string[] droppedFiles)
        {
            foreach (var item in droppedFiles)
            {
                
            }
        }
    }
}

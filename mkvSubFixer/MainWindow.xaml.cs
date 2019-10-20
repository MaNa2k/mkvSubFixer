using Microsoft.Win32;
using System;
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
        public int counter { get; set; } = 1;
        public MainWindow()
        {
            InitializeComponent();

            DataGridTextColumn col1 = new DataGridTextColumn();
            DataGridTextColumn col2 = new DataGridTextColumn();

            dgItems.Columns.Add(col1);
            dgItems.Columns.Add(col2);

            col1.Binding = new Binding("count");
            col2.Binding = new Binding("name");

            col1.Header = "Count";
            col2.Header = "Name";
        }

        public class GridColumns
        {
            public int count { get; set; }
            public string name { get; set; }
        }

        private void FileBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Matroska Files (*.mkv)|*.mkv";
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == true)
            {
                var z = openFileDialog.FileNames.ToList().Distinct();

                foreach (var item in z)
                {
                    dgItems.Items.Add(new GridColumns { count = counter++, name = item });
                }
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

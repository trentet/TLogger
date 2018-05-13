using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;

namespace TLogger.Controller
{
    public class LogHandler : INotifyPropertyChanged
    {
        private bool _pauseExports;
        private int _lastExportedIndex = -1;
        public LogHandler(string saveDirectory, string fileName)
        {
            SaveDirectory = saveDirectory ?? "";

            FileName = fileName ?? "";

            if (!_pauseExports)
            {
                if (!Directory.Exists(SaveDirectory) && SaveDirectory.Length > 3)
                {
                    Directory.CreateDirectory(SaveDirectory);
                    using (File.Create(SavePath)) { }
                }
            }

            Logs.CollectionChanged += Logs_Changed;
        }

        private string _saveDirectory = "";
        public string SaveDirectory { get => _saveDirectory; set { _saveDirectory = value; UpdateExportStatus(); } }

        private string _fileName = "";
        public string FileName { get => _fileName; set { _fileName = value; UpdateExportStatus(); } }

        public string SavePath { get => Path.Combine(SaveDirectory, FileName + ".txt"); }
        public ObservableCollection<string> Logs { get; set; } = new ObservableCollection<string>();

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void Clear()
        {
            Logs.Clear();
        }

        void UpdateExportStatus()
        {
            if (string.IsNullOrEmpty(SaveDirectory) || string.IsNullOrEmpty(FileName))
            {
                _pauseExports = true;
            }
            else
            {
                _pauseExports = false;
            }
        }

        private void Logs_Changed(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (_pauseExports) return;
            if (Logs.Count - 1 <= _lastExportedIndex) return;
            var countOfPendingLogEntries = Logs.Count - 1 - _lastExportedIndex;
            var startIndex = _lastExportedIndex+1;
            for (var x = startIndex; x < countOfPendingLogEntries+startIndex; x++)
            {
                if (!File.Exists(SavePath))
                    Directory.CreateDirectory(Path.GetDirectoryName(SavePath));
                using (var sw = File.AppendText(SavePath))
                {
                    sw.WriteLine("[" + DateTime.Now + "] " + Logs[x]);
                }
                _lastExportedIndex = x;
            }
        }
    }
}
using System;
using System.IO;
using System.Threading;

namespace FolderService
{
    class Logger
    {
        FileSystemWatcher watcher;
        object obj = new object();
        bool enabled = true;

        public Logger()
        {
            watcher = new FileSystemWatcher("D:\\Temp");
            watcher.Deleted += Watcher_Deleted;
            watcher.Created += Watcher_Created;
            watcher.Changed += Watcher_Changed;
            watcher.Renamed += Watcher_Renamed;
        }

        public void Start()
        {
            watcher.EnableRaisingEvents = true;
            while (enabled)
            {
                Thread.Sleep(1000);
            }
        }

        public void Stop()
        {
            watcher.EnableRaisingEvents = false;
            enabled = false;
        }

        private void Watcher_Renamed(object sender, RenamedEventArgs e)
        {
            string filePath = e.OldFullPath;
            RecordEntry(LogCategory.Rename, filePath, e.FullPath);
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            string filePath = e.FullPath;
            RecordEntry(LogCategory.Update, filePath);
        }

        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            string filePath = e.FullPath;
            RecordEntry(LogCategory.Create, filePath);
        }

        private void Watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            string filePath = e.FullPath;
            RecordEntry(LogCategory.Delete, filePath);
        }

        private void RecordEntry(LogCategory logCategory, string filePath, string newName = "")
        {
            lock (obj)
            {
                using (StreamWriter writer = new StreamWriter("D:\\folgerlog.txt", true))
                {
                    string logString = $"{DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss")} file {filePath} was {logCategory.Value} {(string.IsNullOrEmpty(newName) ? null : newName)}";
                    writer.WriteLine(logString);
                    writer.Flush();
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using IO = System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileTag.Models;

namespace FileTag.Infrastacture.Watcher
{
    public class Watcher
    {
        public Watcher(IDataProvider dataProvider)
        {
            Dirs = new List<string>();
            Files = new List<File>();
            _dataProvider = dataProvider;
        }

        private IDataProvider _dataProvider;
        public ICollection<string> Dirs { get; set; }
        public List<File> Files { get; set; }

        public event Action<Watcher> ScanComplete;
        public event Action<Watcher> SyncComplete;

        public void Scan(bool syncAfterScan = false)
        {
            Files.Clear();

            foreach (var dir in Dirs)
            {
                if (!IO.Directory.Exists(dir))
                    continue;

                foreach (var path in IO.Directory.EnumerateFiles(dir))
                {
                    AddFile(path);
                }
            }

            ScanComplete?.Invoke(this);

            if (syncAfterScan)
                SyncFiles();
        }

        private void AddFile(string path)
        {
            var fileInfo = new IO.FileInfo(path);
            Files.Add(new File
            {
                Name = fileInfo.Name,
                Path = fileInfo.FullName
            });
        }

        public void SyncFiles()
        {
            var filesNeedAddToDB = new List<File>();
            var filesNeedAddToLocal = new List<File>();

            foreach (var localFile in Files)
            {
                if (!_dataProvider.FileExists(localFile.Path))
                    filesNeedAddToDB.Add(localFile);
            }

            foreach (var dbFilePath in _dataProvider.GetFilesPaths())
            {
                var localFile = Files.FirstOrDefault(f => f.Path == dbFilePath);

                if (localFile == null)
                    filesNeedAddToLocal.Add(_dataProvider.GetFile(dbFilePath));
            }

            Files.AddRange(filesNeedAddToLocal);
            _dataProvider.AddFiles(filesNeedAddToDB.ToArray());

            SyncComplete?.Invoke(this);
        }
    }
}
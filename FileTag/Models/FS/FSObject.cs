using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTag.Models.FS
{
    public class FSObject
    {
        public FSObject()
        {

        }

        public FSObject(DirectoryInfo dirInfo)
        {
            Type = FSObjectType.Dir;
            Name = dirInfo.Name;
            FullPath = dirInfo.FullName;
        }

        public FSObject(FileInfo fileInfo)
        {
            Type = FSObjectType.File;
            Name = fileInfo.Name;
            FullPath = fileInfo.FullName;
        }

        public FSObject(DriveInfo driveInfo)
        {
            Type = FSObjectType.Dir;
            Name = driveInfo.Name;
            FullPath = driveInfo.Name;
        }

        public static FSObject Root()
        {
            return new FSObject()
            {
                Name = "root",
                Type = FSObjectType.Dir,
                FullPath = ""
            };
        }

        public FSObjectType Type { get; set; }
        public string Name { get; set; }
        public string FullPath { get; set; }

        public bool IsDir()
        {
            return Type == FSObjectType.Dir;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}

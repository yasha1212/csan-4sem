using System;
using System.Collections.Generic;
using System.Text;

namespace FileStorage
{
    public class FileAttributes
    {
        public long Size { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        
        public FileAttributes(long size, string fileName, string path)
        {
            Size = size;
            Name = fileName;
            Path = path;
        }
    }
}

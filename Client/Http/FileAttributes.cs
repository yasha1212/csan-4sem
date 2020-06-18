using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Http
{
    public class FileAttributes
    {
        public long Size { get; set; }
        public string Name { get; set; }

        public FileAttributes(long size, string name)
        {
            Size = size;
            Name = name;
        }
    }
}

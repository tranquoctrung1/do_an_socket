using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterServer.ULT
{
    public static class ReadFile
    {
        public static byte[] ReadFileAction(string path)
        {
            return File.ReadAllBytes(path);
        }
    }
}

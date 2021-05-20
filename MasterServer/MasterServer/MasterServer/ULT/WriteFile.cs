using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterServer.ULT
{
    public static class WriteFile
    {
        public static void WriteFileAction(string path, string content)
        {
            if(content.IndexOf("<EOF>") > -1)
            {
                content =  content.Replace("<EOF>", "");
            }
            content = "\n" + content;
            File.AppendAllText(path, content);           
        }
    }
}

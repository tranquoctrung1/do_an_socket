using MasterServer.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MasterServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread fileServer = new Thread(new ThreadStart(AsynchronousSocketListener.StartListeningForFileServer));
            fileServer.Start();

            Thread client = new Thread(new ThreadStart(AsynchronousSocketListener.StartListeningForClient));
            client.Start();

        }
    }
}

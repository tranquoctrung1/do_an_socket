using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        public static void StartClient()
        {
            // Data buffer for incoming data.  
            byte[] bytes = new byte[4096];

            // Connect to a remote device.  
            try
            {
                // Establish the remote endpoint for the socket.  
                // This example uses port 11000 on the local computer.  
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                // change host to connect to master server
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 1230);

                // Create a TCP/IP  socket.  
                Socket sender = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect the socket to the remote endpoint. Catch any errors.  
                try
                {
                    sender.Connect(remoteEP);

                    Console.WriteLine("Socket connected to {0}",
                        sender.RemoteEndPoint.ToString());

                    // Encode the data string into a byte array.  
                    byte[] msg = Encoding.ASCII.GetBytes("127.0.0.1;4444;text.txt\n127.0.0.1;4443;text2.txt<EOF>");

                    // Send the data through the socket.  
                    int bytesSent = sender.Send(msg);

                    // Receive the response from the remote device.  
                    int bytesRec = sender.Receive(bytes);
                    //Console.WriteLine("Echoed test = {0}",
                    //    Encoding.ASCII.GetString(bytes, 0, bytesRec));
                    string str = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    string[] lines = str.Split('\n');
                    Console.WriteLine("List data server and file");
                    for (int i = 0; i < lines.Length; i++)
                    {
                        string[] line = lines[i].Split(';');
                            Console.WriteLine($"{i + 1}. Server: {line[0]}:{line[1]} . Name file: {line[2]}");
                    }

                    // Release the socket.  
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();

                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        static void Main(string[] args)
        {
            StartClient();
            Console.ReadLine();
        }
    }
    
}

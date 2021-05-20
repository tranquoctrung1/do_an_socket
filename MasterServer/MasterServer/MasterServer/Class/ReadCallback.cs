using MasterServer.ULT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MasterServer.Class
{
    public static class ReadCallback
    {
        public static void ReadCallbackAction(IAsyncResult ar)
        {
            String content = String.Empty;

            // Retrieve the state object and the handler socket  
            // from the asynchronous state object.  
            State state = (State)ar.AsyncState;
            Socket handler = state.workSocket;

            // Read data from the client socket.
            int bytesRead = handler.EndReceive(ar);

            if (bytesRead > 0)
            {
                // There  might be more data, so store the data received so far.  
                state.sb.Append(Encoding.ASCII.GetString(
                    state.buffer, 0, bytesRead));

                // Check for end-of-file tag. If it is not there, read
                // more data.  
                content = state.sb.ToString();
                if (content.IndexOf("<EOF>") > -1)
                {
                    // All the data has been read from the
                    // client. Display it on the console.  
                    Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                        content.Length, content);

                    // Write to text file
                    WriteFile.WriteFileAction("./listFile.txt", content);

                    // Echo the data back to the client.  
                    Send.SendAction(handler, content);
                }
                else
                {
                    // Not all data received. Get more.  
                    handler.BeginReceive(state.buffer, 0, State.BufferSize, 0,
                    new AsyncCallback(ReadCallback.ReadCallbackAction), state);
                }
            }
        }

        public static void NoReadAndSendCallbackAction(IAsyncResult ar)
        {
            String content = String.Empty;

            // Retrieve the state object and the handler socket  
            // from the asynchronous state object.  
            State state = (State)ar.AsyncState;
            Socket handler = state.workSocket;

            state.buffer =  ReadFile.ReadFileAction("./listFile.txt");

            if (state.buffer.Length <= 4096)
            {
                state.sb.Append(Encoding.ASCII.GetString(
                 state.buffer, 0, state.buffer.Length));

                content = state.sb.ToString();
                Send.SendAction(handler, content);

            }
            else
            {
                handler.BeginSend(state.buffer, 0, State.BufferSize, 0, new AsyncCallback(ReadCallback.NoReadAndSendCallbackAction), state);
            }


        }
    }
}

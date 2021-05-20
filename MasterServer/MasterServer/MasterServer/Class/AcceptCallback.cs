using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MasterServer.Class
{
    public static class AcceptCallback
    {
        public static void AcceptCallbackAction(IAsyncResult ar)
        {
            // Signal the main thread to continue.  
            AsynchronousSocketListener.allDone.Set();

            // Get the socket that handles the client request.  
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            // Create the state object.  
            State state = new State();
            state.workSocket = handler;
            handler.BeginReceive(state.buffer, 0, State.BufferSize, 0,
                new AsyncCallback(ReadCallback.ReadCallbackAction), state);
        }

        public static void AcceptCallbackSendAction(IAsyncResult ar)
        {
            // Signal the main thread to continue.  
            AsynchronousSocketListener.allDone.Set();

            // Get the socket that handles the client request.  
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            // Create the state object.  
            State state = new State();
            state.workSocket = handler;
            handler.BeginReceive(state.buffer, 0, State.BufferSize, 0,
                new AsyncCallback(ReadCallback.NoReadAndSendCallbackAction), state);
        }
    }
}

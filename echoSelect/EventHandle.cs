using System;

namespace echoSelect
{
    class EventHandle
    {
        public static void OnDisconnect(ClientState state)
        {
            Console.WriteLine("onDisconnect");
            string desc = state.socket.RemoteEndPoint.ToString();
            string sendStr = "Leave|" + desc + ",";
            foreach (ClientState client in Program.clients.Values)
            {
                byte[] sendBytes = System.Text.Encoding.Default.GetBytes(sendStr);
                client.socket.Send(sendBytes);
            }
        }
    }
}

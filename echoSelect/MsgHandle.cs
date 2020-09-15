using System;

namespace echoSelect
{
    class MsgHandle
    {
        public static void MsgEnter(ClientState state, string msg)
        {
            Console.WriteLine("MsgEnter" + msg);
            string[] split = msg.Split(',');
            string desc = split[0];
            float x = float.Parse(split[1]);
            float y = float.Parse(split[2]);
            float z = float.Parse(split[3]);
            float eulY = float.Parse(split[4]);
            state.hp = 100;
            state.x = x;
            state.y = y;
            state.z = z;
            state.eulY = eulY;
            string sendStr = "Enter|" + msg;
            foreach (ClientState client in Program.clients.Values)
            {
                byte[] sendBytes = System.Text.Encoding.Default.GetBytes(sendStr);
                client.socket.Send(sendBytes);
            }
        }

        public static void MsgList(ClientState state, string msg)
        {
            Console.WriteLine("MsgList" + msg);
            string sendStr = "List|";
            foreach (ClientState client in Program.clients.Values)
            {
                sendStr += client.socket.RemoteEndPoint.ToString() + ",";
                sendStr += client.x.ToString() + ",";
                sendStr += client.y.ToString() + ",";
                sendStr += client.z.ToString() + ",";
                sendStr += client.eulY.ToString() + ",";
                sendStr += client.hp.ToString() + ",";
            }
            byte[] sendBytes = System.Text.Encoding.Default.GetBytes(sendStr);
            state.socket.Send(sendBytes);

        }
        public static void MsgMove(ClientState state, string msg)
        {
            Console.WriteLine("MsgList" + msg);
            string[] split = msg.Split(',');
            string desc = split[0];
            float x = float.Parse(split[1]);
            float y = float.Parse(split[2]);
            float z = float.Parse(split[3]);
 
            state.x = x;
            state.y = y;
            state.z = z;
      
            string sendStr = "Move|" + msg;
            foreach (ClientState client in Program.clients.Values)
            {
                byte[] sendBytes = System.Text.Encoding.Default.GetBytes(sendStr);
                client.socket.Send(sendBytes);
            }


        }

    }
}

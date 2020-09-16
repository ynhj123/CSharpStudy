
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace GameServer.script.net
{
    public class ClientState
    {
        public Socket socket;
        public ByteArray readBuff = new ByteArray();

        public long lastPingTime = 0;

    }
}

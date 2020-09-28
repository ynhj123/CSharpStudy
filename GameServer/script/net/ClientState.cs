using GameServer.script.logic;
using System.Net.Sockets;

namespace GameServer.script.net
{
    public class ClientState
    {
        public Socket socket;
        public ByteArray readBuff = new ByteArray();

        public long lastPingTime = 0;
        public Player player;

    }
}

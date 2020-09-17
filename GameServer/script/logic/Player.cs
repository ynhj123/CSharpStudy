using GameServer.script.net;

namespace GameServer.script.logic
{
    class Player
    {
        public string id = "";
        public ClientState state;
        public int x;
        public int y;
        public int z;
        public PlayerData data;
        public Player(ClientState state)
        {
            this.state = state;
        }
        public void Send(MsgBase msg)
        {
            NetManager.Send(state, msg);
        }
    }
}

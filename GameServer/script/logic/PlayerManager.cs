using System.Collections.Generic;

namespace GameServer.script.logic
{
    class PlayerManager
    {
        public static Dictionary<string, Player> players = new Dictionary<string, Player>();
        public static bool IsOnline(string id)
        {
            return players.ContainsKey(id);
        }
        public static Player GetPlayer(string id)
        {
            if (IsOnline(id))
            {
                return players[id];
            }
            return null;
        }
        public static void AddPlayer(Player player)
        {
            players.Add(player.id, player);
        }
        public static void Remove(string id)
        {
            players.Remove(id);
        }
        public static void Broadcast(MsgBase msg)
        {
            foreach (Player player in players.Values)
            {

                player.Send(msg);
            }
        }
    }
}

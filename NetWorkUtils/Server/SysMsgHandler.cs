using NetWorkUtils.Server;
using System;


public partial class MsgHandler
{
    public static void MsgPing(ClientState c, MsgBase msgBase)
    {
   
        c.lastPingTime = NetManager.GetTimeStamp();
        MsgPong msgPong = new MsgPong();
        NetManager.Send(c, msgPong);
    }

    public static void MsgEnter(ClientState c, MsgBase msgBase)
    {
        MsgEnter msgEnter = (MsgEnter)msgBase;
        Player player = PlayerManager.GetPlayer(msgEnter.id);
        if(player == null)
        {
            player = new Player(c);
            player.id = msgEnter.id;
            player.name = msgEnter.name;
            PlayerManager.AddPlayer(player.id,player);
        }
        else
        {
            player.name = msgEnter.name;

        }
        c.player = player;
        player.Send(msgEnter);
    }
}



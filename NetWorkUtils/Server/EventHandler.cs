﻿using NetWorkUtils.Server;
using System;

public partial class EventHandler
{
    public static void OnDisconnect(ClientState c)
    {
        Console.WriteLine("Close");
        //Player下线
        if (c.player != null)
        {
            //离开战场
            int roomId = c.player.roomId;
            if (roomId >= 0)
            {
                Room room = RoomManager.GetRoom(roomId);
                room.RemovePlayer(c.player.id);
            }

            //移除
            PlayerManager.RemovePlayer(c.player.id);
        }
    }


    public static void OnTimer()
    {
        CheckPing();
        RoomManager.Update();
    }

    //Ping检查
    public static void CheckPing()
    {
        //现在的时间戳
        long timeNow = NetManager.GetTimeStamp();
        //遍历，删除
        foreach (ClientState s in NetManager.clients.Values)
        {
            if (timeNow - s.lastPingTime > NetManager.pingInterval * 4)
            {
                Console.WriteLine("Ping Close " + s.socket.RemoteEndPoint.ToString());
                NetManager.Close(s);
                return;
            }
        }
    }


}


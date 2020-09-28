using GameServer.script.logic;
using System;
using System.Collections.Generic;

public class RoomManager
{
    //最大id
    private static int maxId = 1;
    //房间列表
    public static Dictionary<int, Room> rooms = new Dictionary<int, Room>();

    public static void InitRoom(int max = 10)
    {
        for (int i = 0; i < max; i++)
        {
            AddRoom();
        }
    }


    //创建房间
    private static Room AddRoom()
    {
        maxId++;
        Room room = new Room();
        room.RoomId = maxId;
        
        rooms.Add(room.RoomId, room);
        return room;
    }

    //删除房间
    public static bool RemoveRoom(int id)
    {
        rooms.Remove(id);
        return true;
    }

    //获取房间
    public static Room GetRoom(int id)
    {
        if (rooms.ContainsKey(id))
        {
            return rooms[id];
        }
        return null;
    }

    //生成MsgGetRoomList协议


    //Update
    public static void Update()
    {

    }
}



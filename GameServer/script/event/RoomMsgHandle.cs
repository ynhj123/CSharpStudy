using GameServer.script.logic;
using GameServer.script.net;
using Newtonsoft.Json;
using System;
using System.Net;

public partial class MsgHandler
{
	//请求房间列表
	public static void MsgListRoom(ClientState c, MsgBase msgBase)
	{
		MsgListRoom msg = (MsgListRoom)msgBase;
		User user = c.user;
		if (user == null)
        {
			msg.code = HttpStatusCode.Unauthorized;
			msg.result = "请先登录";
			NetManager.Send(c, msg);
        }
        else
        {
			msg.code = HttpStatusCode.OK;
			msg.result = JsonConvert.SerializeObject(RoomManager.rooms);
			NetManager.Send(c, msg);

		}

	}
	//进入房间
	public static void MsgEnterRoom(ClientState c, MsgBase msgBase)
	{
		
		MsgEnterRoom msg = (MsgEnterRoom)msgBase;
		User user = c.user;
		if (user == null)
        {
			msg.code = HttpStatusCode.Unauthorized;
			msg.result = "请先登录";
			NetManager.Send(c, msg);
			return;
		}
		//已经在房间里
		if (user.RoomId >= 0)
		{
			msg.code = HttpStatusCode.InternalServerError;
			msg.result = "已经在房间里";
			NetManager.Send(c, msg);
			return;
		}
		//获取房间
		Room room = RoomManager.GetRoom(msg.roomId);
		if (room == null)
		{
			msg.code = HttpStatusCode.NotFound;
			msg.result = "房间不存在";
			return;
		}
		//进入
		if (!room.AddUser(user.Userid))
		{
			msg.code = HttpStatusCode.InternalServerError;
			msg.result = "加入房间失败";
			NetManager.Send(c, msg);
			return;
		}
		user.RoomId = msg.roomId;
		//返回协议	
		msg.code = HttpStatusCode.OK;
		msg.result = "进入成功";
		NetManager.Send(c, msg);
	}

	public static void MsgLeaveRoom(ClientState c, MsgBase msgBase)
	{
		MsgLeaveRoom msg = (MsgLeaveRoom)msgBase;
		User user = c.user;
		if (user == null) return;

		Room room = RoomManager.GetRoom(user.RoomId);
		if (room == null)
		{
			return;
		}

        bool delete = room.RemoveUser(user.Userid);
        if (!delete)
        {
			Console.WriteLine("离开房间异常");
			return;
        }
		//返回协议
		user.RoomId = -1;
		NetManager.Send(c, msg);
	}
	public static void MsgGetRoomInfo(ClientState c, MsgBase msgBase)
	{
		MsgGetRoomInfo msg = (MsgGetRoomInfo)msgBase;
		User user = c.user;
		if (user == null)
		{
			msg.code = HttpStatusCode.Unauthorized;
			msg.result = "请先登录";
			NetManager.Send(c, msg);
			return;
		}

		Room room = RoomManager.GetRoom(user.RoomId);
		if (room == null)
		{
			msg.code = HttpStatusCode.NotFound;
			msg.result = "房间不存在";
			return;
		}
		//获取玩家列表
	
		msg.code = HttpStatusCode.OK;
		msg.result = JsonConvert.SerializeObject(room);
		NetManager.Send(c, msg);
	}
	/*public static void MsgStartBattle(ClientState c, MsgBase msgBase)
	{
		MsgStartBattle msg = (MsgStartBattle)msgBase;
		Player player = c.user;
		if (player == null) return;
		//room
		Room room = RoomManager.GetRoom(player.roomId);
		if (room == null)
		{
			msg.result = 1;
			player.Send(msg);
			return;
		}
		//是否是房主
		if (!room.isOwner(player))
		{
			msg.result = 1;
			player.Send(msg);
			return;
		}
		//开战
		if (!room.StartBattle())
		{
			msg.result = 1;
			player.Send(msg);
			return;
		}
		//成功
		msg.result = 0;
		player.Send(msg);
	}*/

	/*//查询战绩
	public static void MsgGetAchieve(ClientState c, MsgBase msgBase){
		MsgGetAchieve msg = (MsgGetAchieve)msgBase;
		Player player = c.player;
		if(player == null) return;

		msg.win = player.data.win;
		msg.lost = player.data.lost;

		player.Send(msg);
	}


	

	//创建房间
	public static void MsgCreateRoom(ClientState c, MsgBase msgBase){
		MsgCreateRoom msg = (MsgCreateRoom)msgBase;
		Player player = c.player;
		if(player == null) return;
		//已经在房间里
		if(player.roomId >=0 ){
			msg.result = 1;
			player.Send(msg);
			return;
		}
		//创建
		Room room = RoomManager.AddRoom();
		room.AddPlayer(player.id);

		msg.result = 0;
		player.Send(msg);
	}

	


	//获取房间信息
	

	//离开房间
	


	//请求开始战斗
	*/

}



using GameServer.script.net;
using GameServer.script.wrapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace GameServer.script.logic
{
    class RoomMsgHandler
    {
    }
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
            if (user.RoomId > 0)
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
            if (!room.AddUser(user))
            {
                msg.code = HttpStatusCode.InternalServerError;
                msg.result = "加入房间失败";
                NetManager.Send(c, msg);
                return;
            }

            //返回协议	
            msg.code = HttpStatusCode.OK;
            msg.result = JsonConvert.SerializeObject(room);
            room.Broadcast(msg);
        }
        public static void MsgUnprepare(ClientState c, MsgBase msgBase)
        {

            MsgUnprepare msg = (MsgUnprepare)msgBase;
            User user = c.user;
            if (user == null)
            {
                msg.code = HttpStatusCode.Unauthorized;
                msg.result = "请先登录";
                NetManager.Send(c, msg);
                return;
            }

            //获取房间
            Room room = RoomManager.GetRoom(user.RoomId);
            if (room == null)
            {
                msg.code = HttpStatusCode.NotFound;
                msg.result = "房间不存在";
                return;
            }
            //准备
            if (!room.Unprepare(user))
            {
                msg.code = HttpStatusCode.InternalServerError;
                msg.result = "取消准备失败";
                NetManager.Send(c, msg);
                return;
            }

            //返回协议	
            msg.code = HttpStatusCode.OK;
            msg.result = user.Userid;
            room.Broadcast(msg);
        }
        public static void MsgPrepare(ClientState c, MsgBase msgBase)
        {

            MsgPrepare msg = (MsgPrepare)msgBase;
            User user = c.user;
            if (user == null)
            {
                msg.code = HttpStatusCode.Unauthorized;
                msg.result = "请先登录";
                NetManager.Send(c, msg);
                return;
            }
            
            //获取房间
            Room room = RoomManager.GetRoom(user.RoomId);
            if (room == null)
            {
                msg.code = HttpStatusCode.NotFound;
                msg.result = "房间不存在";
                return;
            }
            //准备
            if (!room.Prepare(user))
            {
                msg.code = HttpStatusCode.InternalServerError;
                msg.result = "准备失败";
                NetManager.Send(c, msg);
                return;
            }

            //返回协议	
            msg.code = HttpStatusCode.OK;
            msg.result = user.Userid;
            room.Broadcast(msg);
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
            msg.code = HttpStatusCode.OK;
            msg.result = JsonConvert.SerializeObject(room);
            room.Broadcast(msg);
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
        public static void MsgStartBattle(ClientState c, MsgBase msgBase)
        {
            MsgStartBattle msg = (MsgStartBattle)msgBase;
            User user = c.user;
            if (user == null) return;
            //room
            Room room = RoomManager.GetRoom(user.RoomId);
            if (room == null)
            {
                msg.code = HttpStatusCode.NotFound;
                msg.result = "房间不存在";
                NetManager.Send(c, msg);
                return;
            }
            //是否是房主
            if (!room.IsOwner(user))
            {
                msg.code = HttpStatusCode.InternalServerError;
                msg.result = "你不是房主";
                NetManager.Send(c, msg);
                return;
            }
            //开战
            /* if (!room.StartBattle())
             {
                 msg.code = HttpStatusCode.InternalServerError;
                 NetManager.Send(c, msg);
                 msg.result = "无法开始";
                 return;
             }*/
            msg.startPlays = new List<MsgStartBattle.StartPlay>();
            for (int i = 0; i < room.Users.Count; i++)
            {
                MsgStartBattle.StartPlay startPlay = UserWrapper.toStartPlay(i, room.Users[i]);
                msg.startPlays.Add(startPlay);
            }
            msg.code = HttpStatusCode.OK;
            //成功
            room.Broadcast(msg);
        }

        /*//查询战绩
		public static void MsgGetAchieve(ClientState c, MsgBase msgBase){
			MsgGetAchieve msg = (MsgGetAchieve)msgBase;
			Player player = c.player;
			if(player == null) return;

			msg.win = player.data.win;
			msg.lost = player.data.lost;

			player.Send(msg);
		}

		*/

    }
}


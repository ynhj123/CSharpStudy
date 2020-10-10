using NetWorkUtils.client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using static NetWorkUtils.client.NetManager;

namespace NetWorkUtils.Client
{
   public class NetUtils
    {
        /// <summary>
        /// 连接服务
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public static void Start(string ip , int port)
        {
            //网络协议监听
            NetManager.AddMsgListener("MsgEnter", OnMsgEnter);
            //room
            NetManager.AddMsgListener("MsgGetRoomList", NetRoomHandler.OnMsgGetRoomList);
            NetManager.AddMsgListener("MsgCreateRoom", NetRoomHandler.OnMsgCreateRoom);
            NetManager.AddMsgListener("MsgEnterRoom", NetRoomHandler.OnMsgEnterRoom);
            NetManager.AddMsgListener("MsgGetRoomInfo", NetRoomHandler.OnMsgGetRoomInfo);
            NetManager.AddMsgListener("MsgLeaveRoom", NetRoomHandler.OnMsgLeaveRoom);
            NetManager.AddMsgListener("MsgLeaveGame", NetRoomHandler.OnMsgLeaveGame);
            //网络事件监听
            NetManager.AddEventListener(NetManager.NetEvent.ConnectSucc, NetEventHandler.OnConnectSucc);
            NetManager.AddEventListener(NetManager.NetEvent.ConnectFail, NetEventHandler.OnConnectFail);
            NetManager.AddEventListener(NetManager.NetEvent.Close, NetEventHandler.OnClose);
            NetManager.Connect(ip, port);
          
            
        }

        private static void OnMsgEnter(MsgBase msgBase)
        {
            Debug.WriteLine(msgBase.protoName);
        }

        /// <summary>
        /// 使用player接入服务
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public static void Enter(string id,string name)
        {
            MsgEnter msgEnter = new MsgEnter();
            msgEnter.id = id;
            msgEnter.name = name;
            NetManager.Send(msgEnter);
        }
        /// <summary>
        /// 处理收到的消息 需要不停的更新
        /// </summary>
        public static void Update()
        {
            NetManager.Update();
        }
        /// <summary>
        /// 添加自己的协议及处理时间
        /// </summary>
        /// <param name="protoname"></param>
        /// <param name="handle"></param>
        public static void AddMsgListener(string protoname, MsgListener handle)
        {
            NetManager.AddMsgListener(protoname, handle);
        }
        /// <summary>
        /// 向服务端发送消息 ，如果用户已经加入房间会自动将消息广播给所有房间用户
        /// </summary>
        /// <param name="msgBase"></param>
        public static void Send(MsgBase msgBase)
        {
            NetManager.Send(msgBase);
        }

    }
}

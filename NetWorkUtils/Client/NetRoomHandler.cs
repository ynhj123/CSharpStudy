using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace NetWorkUtils.Client
{
    class NetRoomHandler
    {
        internal static void OnMsgGetRoomList(MsgBase msgBase)
        {
            Debug.WriteLine(msgBase.protoName);
        }

        internal static void OnMsgCreateRoom(MsgBase msgBase)
        {
            Debug.WriteLine(msgBase.protoName);
        }

        internal static void OnMsgEnterRoom(MsgBase msgBase)
        {
            Debug.WriteLine(msgBase.protoName);
        }

        internal static void OnMsgGetRoomInfo(MsgBase msgBase)
        {
            Debug.WriteLine(msgBase.protoName);
        }

        internal static void OnMsgLeaveRoom(MsgBase msgBase)
        {
            Debug.WriteLine(msgBase.protoName);
        }

        internal static void OnMsgLeaveGame(MsgBase msgBase)
        {
            Debug.WriteLine(msgBase.protoName);
        }
    }
}

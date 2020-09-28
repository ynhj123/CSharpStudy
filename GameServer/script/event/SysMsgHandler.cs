using GameCommon.Utils;
using GameServer.script.db;
using GameServer.script.net;
using GameServer.script.wrapper;
using System.Diagnostics;

namespace GameServer.script.logic
{
    class SysMsgHandler
    {
    }
    public partial class MsgHandler
    {
        public static void MsgPing(ClientState c, MsgBase msg)
        {
            Debug.WriteLine("MsgPing");
            c.lastPingTime = NetManager.GetTimeStamp();
            MsgPong msgPong = new MsgPong();
            NetManager.Send(c, msgPong);
        }
        public static void MsgRegistry(ClientState c, MsgBase msg)
        {
            Debug.WriteLine("MsgRegistry");
            MsgRegistry msgResgistory = (MsgRegistry)msg;
            User user = UserWrapper.FromMsg(msgResgistory);

            bool checkOut = UserManager.Check(user, out msgResgistory.result);
            if (!checkOut)
            {
                msgResgistory.isSuccess = false;
                NetManager.Send(c, msgResgistory);
            }
            else
            {
                user.Score = 0;
                user.Userid = UUIDUtils.GetUUID();
                UserManager.Add(user);
                msgResgistory.isSuccess = true;
                msgResgistory.result = "注册成功";
                NetManager.Send(c, msgResgistory);
            }

        }
        public static void MsgLogin(ClientState c, MsgBase msg)
        {
            Debug.WriteLine("MsgLogin");
            c.lastPingTime = NetManager.GetTimeStamp();
            MsgPong msgPong = new MsgPong();
            NetManager.Send(c, msgPong);
        }
    }
}

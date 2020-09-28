using GameCommon.Utils;
using GameServer.script.db;
using GameServer.script.net;
using GameServer.script.wrapper;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;

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
                msgResgistory.code = HttpStatusCode.InternalServerError;
                NetManager.Send(c, msgResgistory);
            }
            else
            {
                user.Score = 0;
                user.Userid = UUIDUtils.GetUUID();
                UserManager.Add(user);
                msgResgistory.code = HttpStatusCode.OK;
                msgResgistory.result = "注册成功";
                NetManager.Send(c, msgResgistory);
            }

        }
        public static void MsgLogin(ClientState c, MsgBase msg)
        {
            Debug.WriteLine("MsgLogin");
            MsgLogin msgLogin = (MsgLogin)msg;
            User user = UserManager.Login(msgLogin.username, msgLogin.password, out msgLogin.result);
            if (user == null)
            {
                msgLogin.code = HttpStatusCode.NotFound;
            }
            else
            {
                msgLogin.code = HttpStatusCode.OK;
                Player player = UserWrapper.ToPlayer(user, c);
                c.user = player;
                PlayerManager.AddPlayer(player);
                msgLogin.result = JsonConvert.SerializeObject(user);
            }
            NetManager.Send(c, msgLogin);
        }
    }
}

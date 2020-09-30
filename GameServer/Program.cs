using GameServer.script.db;
using GameServer.script.net;

namespace GameServer
{
    class Program
    {
        static void Main(string[] args)
        {

            /* User user = new User();
             user.Userid = "fe46662cd0d94d0e84bd37227d8604f2";
             user.Username = "user";
             user.Password = "pwd";
             user.Score = 0;
             UserManager.Add(user);
             User user1 = new User();
             user1.Userid = "1a3292830aef45b695d371de265538e3";
             user1.Username = "ccw";
             user1.Password = "aas";
             user1.Score = 0;
             UserManager.Add(user1);
             User user2 = new User();
             user2.Userid = "b670c1a134b54c96a7f2354dc8a47e6d";
             user2.Username = "aas";
             user2.Password = "pwd";
             user2.Score = 0;
             UserManager.Add(user2);
             User user3 = new User();
             user3.Userid = "c0ddf4c46f42475ea58c017bbaa7129c";
             user3.Username = "bin";
             user3.Password = "123";
             user3.Score = 0;
             UserManager.Add(user3);
             User user4 = new User();
             user4.Userid = "20c21bd793284645b614a57a4856220d";
             user4.Username = "ydwj";
             user4.Password = "123";
             user4.Score = 0;
             UserManager.Add(user4);*/
            UserManager.Load();
            RoomManager.InitRoom();
            NetManager.StartLoop(8888);
        }
    }
}

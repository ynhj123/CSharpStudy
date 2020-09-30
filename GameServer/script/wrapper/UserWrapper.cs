using GameServer.script.logic;
using System;

namespace GameServer.script.wrapper
{
    class UserWrapper
    {
        static Random random = new Random();
        public static User FromMsg(MsgRegistry msg)
        {
            User user = new User();
            user.Password = msg.password;
            user.Username = msg.username;
            return user;
        }

        internal static Player ToPlayer(User user, net.ClientState c)
        {
            Player player = new Player(c);
            player.id = user.Userid;
            return player;
        }



        internal static MsgStartBattle.StartPlay toStartPlay(int index, User user)
        {
            MsgStartBattle.StartPlay startPlay = new MsgStartBattle.StartPlay();
            startPlay.Id = user.Userid;
            startPlay.X = random.Next(1, 20);
            startPlay.Y = random.Next(1, 70);
            startPlay.Index = index;
            return startPlay;
        }
    }
}

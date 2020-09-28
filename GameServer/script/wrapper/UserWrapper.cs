using GameServer.script.logic;

namespace GameServer.script.wrapper
{
    class UserWrapper
    {
        public static User FromMsg(MsgRegistry msg)
        {
            User user = new User();
            user.Password = msg.password;
            user.Username = msg.username;
            return user;
        }
    }
}

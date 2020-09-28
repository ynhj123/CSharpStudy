using ConsoleGame.Component;

namespace ConsoleGame.model
{
    public class User
    {
        private string username;
        private string password;
        private string userid;
        private long score;
        int hp;
        int attachInterval = 3;
        int mp;
        int resistance;

        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string Userid { get => userid; set => userid = value; }
        public long Score { get => score; set => score = value; }
        public int Hp { get => hp; set => hp = value; }
        public int AttachInterval { get => attachInterval; set => attachInterval = value; }
        public int Mp { get => mp; set => mp = value; }
        public int Resistance { get => resistance; set => resistance = value; }
    }
}

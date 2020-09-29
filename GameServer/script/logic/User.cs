namespace GameServer.script.logic
{
    public class User
    {
        private string username;
        private string password;
        private string userid;
        private long score;
        private int roomId;




        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string Userid { get => userid; set => userid = value; }
        public long Score { get => score; set => score = value; }
        public int RoomId { get => roomId; set => roomId = value; }
    }
}

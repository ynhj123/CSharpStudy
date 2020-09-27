namespace ConsoleGame.model
{
    class LoginScence
    {
        static LoginScence scence;
        private LoginScence() { }

        public static LoginScence GetLoinScence()
        {
            if (scence == null)
            {
                scence = new LoginScence();
            }
            return scence;
        }


    }
}

using ConsoleGame.Controller;
using ConsoleGame.utils.Time;
using System;
using System.Threading;

namespace ConsoleGame.model
{
    [GameCommon.Ioc.Annotation.Component]
    class LoginScence : Scence
    {

        private bool isResgistoryCallBack;
        private bool isLoignCallBack;


        public bool IsResgistoryCallBack { get => isResgistoryCallBack; set => isResgistoryCallBack = value; }
        public bool IsLoignCallBack { get => isLoignCallBack; set => isLoignCallBack = value; }

        public void Handle()
        {
            Console.Clear();
            Console.WriteLine(@"请选择
1登录
2注册");
            char keyChar = Console.ReadKey().KeyChar;
            if ('1' == keyChar)
            {
                HandleLogin();
            }
            else if ('2' == keyChar)
            {
                HandleResgistory();
            }

        }

        public string InputPassword()
        {
            string passwordInput = "";

            while (true)
            {
                ConsoleKeyInfo ck = Console.ReadKey(true);
                //判断用户是否按下的Enter键
                if (ck.Key != ConsoleKey.Enter)
                {
                    if (ck.Key != ConsoleKey.Backspace)
                    {
                        //将用户输入的字符存入字符串中
                        passwordInput += ck.KeyChar.ToString();
                        //将用户输入的字符替换为*
                        Console.Write("*");
                    }
                    else
                    {
                        //删除错误的字符
                        Console.Write("\b \b");
                    }
                }
                else
                {
                    break;
                }
            }

            return passwordInput;


        }

        public void HandleLogin()
        {
            Console.Clear();
            bool isCheck = false;
            Console.WriteLine("用户请输入用户名密码登录！");
            while (!isCheck)
            {
                Console.WriteLine("请输入用户名");
                string username = Console.ReadLine();
                Console.WriteLine("请输入密码");
                string password = InputPassword();
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    Console.WriteLine("用户名或密码不能为空");
                }
                else
                {
                    isCheck = true;
                    MsgLogin loginMsg = new MsgLogin();
                    loginMsg.username = username;
                    loginMsg.password = password;
                    NetManagerEvent.Send(loginMsg);
                    isLoignCallBack = false;
                    TimeEvent.Handle(1, 5, ref isLoignCallBack, () =>
                    {
                        NetManagerEvent.Update();
                    }, () =>
                    {
                        Console.WriteLine("登录超时，请重试！");
                    });
                    if (ScenceController.user == null)
                    {
                        Thread.Sleep(2000);
                    }

                }
            }




        }
        public void HandleResgistory()
        {
            Console.Clear();
            bool isCheck = false;
            Console.WriteLine("账户注册");

            while (!isCheck)
            {
                Console.WriteLine("请输入用户名");
                string username = Console.ReadLine();
                Console.WriteLine("请输入密码");
                string password = InputPassword();


                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    Console.WriteLine("用户名或密码不能为空");
                }
                else
                {
                    isCheck = true;
                    MsgRegistry registryMsg = new MsgRegistry();
                    registryMsg.username = username;
                    registryMsg.password = password;
                    NetManagerEvent.Send(registryMsg);
                    isResgistoryCallBack = false;
                    TimeEvent.Handle(1, 5, ref isResgistoryCallBack, () =>
                    {
                        NetManagerEvent.Update();
                    }, () =>
                    {
                        Console.WriteLine("注册超时，请重试！");
                    });
                    Thread.Sleep(2000);
                }
            }

        }


    }
}

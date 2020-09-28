using ConsoleGame.Controller;
using System;

namespace ConsoleGame.model
{
    [GameCommon.Ioc.Annotation.Component]
    class MenuScence : Scence
    {
        public void Handle()
        {
            Console.Clear();

            Console.WriteLine(@"-------菜单界面---------
|     请选择           |
|     1开始游戏        |
|     2个人信息        |
|     3商店            |
|     4排行榜          |
|     5退出            |
------------------------");
            char keyChar = Console.ReadKey().KeyChar;
            HandleKey(keyChar);
        }

        private void HandleKey(char keyChar)
        {
            if ('1' == keyChar)
            {
                ScenceController.curScence = ScenceController.scenceDict["room"];
            }
            else if ('2' == keyChar)
            {

            }
            else if ('3' == keyChar)
            {

            }
            else if ('4' == keyChar)
            {

            }
            else if ('5' == keyChar)
            {
                ScenceController.isLeavel = true;
                NetManagerEvent.Close();

            }
            else
            {
                Handle();
            }
        }
    }
}

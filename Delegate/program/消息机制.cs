using System;
using System.Threading;

namespace 消息机制
{
    class MessageManager
    {
        static MessageManager _instance;
        public static MessageManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MessageManager();
                }
                return _instance;
            }
        }

        public delegate void DelegateOnLevelUp(int level);
        public delegate void DelegateOnMoneyChange(int money);

        public DelegateOnLevelUp onLevelUpEvent;
        public DelegateOnMoneyChange onMoneyChangeEvent;

        public void TestMessage()
        {
            onLevelUpEvent(9);
            onMoneyChangeEvent(100);
        }
    }

    // 顶部UI框
    class UIPanelTop
    {
        public void Init()
        {
            MessageManager.Instance.onLevelUpEvent += OnLevelUp;
        }

        public void OnLevelUp(int level)
        {
            Console.WriteLine("UIPanel Top收到升级消息");
        }
    }

    // 道具UI框
    class UIPanelItem
    {
        public void Init()
        {
            MessageManager.Instance.onLevelUpEvent += OnLevelUp;
            MessageManager.Instance.onMoneyChangeEvent += OnMoneyChange;
        }

        public void OnLevelUp(int level)
        {
            Console.WriteLine("UIPanel Item收到升级消息 " + level);
        }
        public void OnMoneyChange(int money)
        {
            Console.WriteLine("UIPanel Item收到金币变化消息$ " + money);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            UIPanelItem itemPanel = new UIPanelItem();
            itemPanel.Init();

            UIPanelTop topPanel = new UIPanelTop();
            topPanel.Init();

            Thread.Sleep(1000);
            MessageManager.Instance.TestMessage();

            Console.ReadKey();
        }
    }
}

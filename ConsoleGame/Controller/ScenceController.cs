using ConsoleGame.model;
using GameCommon.Builder;
using System;
using System.Collections.Generic;

namespace ConsoleGame.Controller
{
    /**
     * 用于场景的跳转执行
     */
    public class ScenceController
    {
        public static Random random = new Random();
        public static Dictionary<string, Scence> scenceDict = new Dictionary<string, Scence>();
        public static User user;
        public static Scence curScence;
        public static bool isLeavel = false;


        public void InitScence()
        {

            scenceDict.Add("login", ContainerBuilder.Resolve<LoginScence>());
            scenceDict.Add("index", ContainerBuilder.Resolve<MenuScence>());
            scenceDict.Add("battle", ContainerBuilder.Resolve<GameSence>());
            scenceDict.Add("room", ContainerBuilder.Resolve<RoomScence>());
            scenceDict.Add("roomDetail", ContainerBuilder.Resolve<RoomDetailScence>());
            scenceDict.Add("user", ContainerBuilder.Resolve<UserScence>());
        }



        public void Handle()
        {
            curScence = scenceDict["login"];
            while (!isLeavel)
            {
                curScence.Handle();
            }

        }
    }
}

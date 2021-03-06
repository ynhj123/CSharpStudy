﻿using ConsoleGame.model;
using GameCommon.Builder;
using System.Collections.Generic;

namespace ConsoleGame.Controller
{
    class SpriteDestorySystem : IExecuteSystem
    {
        private static SpriteDestorySystem system;

        public Queue<Sprite> sprites = new Queue<Sprite>();

        private SpriteDestorySystem()
        {
        }
        public static SpriteDestorySystem GetSpriteDestorySystem()
        {
            if (system == null)
            {
                system = new SpriteDestorySystem();
            }
            return system;
        }


        public void Execute()
        {
            GameSence gameSence = ContainerBuilder.Resolve<GameSence>();
            while (sprites.Count > 0)
            {
                Sprite sprite = sprites.Dequeue();
                gameSence.RemoveSprite(sprite);
            }
        }
    }
}

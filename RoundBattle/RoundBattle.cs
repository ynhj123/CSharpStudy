using System;
using System.Collections.Generic;

// 回合制对战游戏。
// 用面向对象方式实现，数据与逻辑分离，具有一定的扩展性


// 练习：
// 1、理解战斗的过程。试着改变主角和NPC，改变他们的技能。（只需要修改关卡初始化就可以实现）

// 2、“框架”就是用来解决扩展和规范性问题的，试着添加新的怪物和主角。可以在每关随机出不同的怪物

// （以上2个改动不需要调整底层代码。）

// 3、本程序中，技能可以增加新的类型，比如无敌N回合的技能。试着实现这个技能。

// 4、扩展性是相对的，有时超出最初设计的话就要改动底层了。
//    回血技能没有次数限制，不合理，试着加上次数限制。

// 5、BUG：已经时间为0的状态，不会被删除

// 6、思考一下：持续掉血、持续加血、持续无敌，这类加状态的技能，能否用一种技能类型代替？
// 可能需要给状态类，加一个Copy()方法


namespace RoundBattle
{
    class RoundBattle
    {
        static Random random = new Random();

        static Character player;
        static Character monster;

        // ------关卡设置------
        static void InitStage()
        {

            int randoInt = random.Next(0, 2);
            if (randoInt == 0)
            {
                monster = CreateSkeleton();

            }
            else
            {
                monster = CreateSkeletonKing();
            }
        }

        static Character CreateSimon()
        {
            Character simon = new Character();
            simon.name = "西蒙";
            simon.hp = 100;
            simon.AddSkill(Skill.CreateDamageSkill("出拳", 20));
            simon.AddSkill(Skill.CreateExecuteSkill("处决", 30));
            simon.AddSkill(Skill.CreateDOTSkill("圣水", 8, 4));
            simon.AddSkill(Skill.CreateHealSkill("吃鸡腿", 50));
            simon.AddSkill(Skill.CreateInvincibleSkill("无敌", 3));
            return simon;
        }

        static Character CreateSkeleton()
        {
            Character monster = new Character();
            monster.name = "骷髅兵";
            monster.hp = 50;
            monster.AddSkill(Skill.CreateDamageSkill("扔骨头", 20));
            monster.AddSkill(Skill.CreateHealSkill("死亡之舞", 30));
            return monster;
        }
        static Character CreateSkeletonKing()
        {
            Character monster = new Character();
            monster.name = "骷髅王";
            monster.hp = 100;
            monster.AddSkill(Skill.CreateDamageSkill("扔骨头", 50));
            monster.AddSkill(Skill.CreateHealSkill("死亡之舞", 80));
            return monster;
        }
        // -------------------

        // -------回合内函数-----
        static void ShowSkills(Character cha)
        {
            for (int i = 0; i < cha.skills.Count; ++i)
            {
                Console.WriteLine("{0}. {1}", i + 1, cha.skills[i].name);
            }
        }

        static Skill InputSkill(List<Skill> skills)
        {
            int n = -1;
            // 用户输入的是从1开始
            while (n <= 0 || n > skills.Count)
            {
                ConsoleKeyInfo keyinfo = Console.ReadKey();
                int.TryParse(keyinfo.KeyChar.ToString(), out n);
            }

            return skills[n - 1];
        }

        static void ShowInfo(Character cha)
        {
            Console.WriteLine("{0}的信息：", cha.name);
            Console.Write("    HP:{0}  ", cha.hp);
            for (int i = 0; i < cha.states.Count; ++i)
            {
                Console.Write("状态:{0} ", cha.states[i]);
            }
            Console.WriteLine();
        }

        static bool AllChasAlive(List<Character> l)
        {
            foreach (Character cha in l)
            {
                if (cha.hp <= 0)
                {
                    return false;
                }
            }
            return true;
        }

        static void ChaAct(Character cur_cha, Character other_cha)
        {
            if (cur_cha == player)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            var effect_states = cur_cha.StateTakeEffect();
            for (int i = 0; i < effect_states.Count; ++i)
            {
                Console.WriteLine("{0}的{1}状态生效，当前HP:{2}", cur_cha.name, effect_states[i].name, cur_cha.hp);
            }

            Skill skill = null;
            // 选择技能的方式不同
            if (cur_cha == player)
            {
                while (true)
                {
                    Console.WriteLine("====请选择{0}的技能：", cur_cha.name);
                    ShowSkills(cur_cha);
                    skill = InputSkill(player.skills);
                    if (!cur_cha.UseSkill(skill, other_cha))
                    {
                        Console.WriteLine("技能使用条件不满足, 重新选择");
                        continue;
                    }
                    break;
                }
            }
            else
            {
                skill = monster.RandSkill();        // 没有UseSkill，BUG
                Console.WriteLine("{0}使用了'{1}'技能", monster.name, skill.name);
            }

            // 判断攻击谁，不应该根据技能类型判断，而是用一个字段来判断
            if (skill.type == SkillType.Heal)
            {
                if (cur_cha.BeHit(skill))
                {
                    if (skill.type == SkillType.Heal)
                    {
                        Console.WriteLine("{0}成功受到治疗，当前HP:{1}", cur_cha.name, cur_cha.hp);
                    }
                }
            }
            else if (skill.type == SkillType.Invincible)
            {
                if (cur_cha.BeHit(skill))
                {
                    if (skill.type == SkillType.Invincible)
                    {
                        Console.WriteLine("{0}成功无敌了，当前HP:{1}，无敌回合{2}", cur_cha.name, cur_cha.hp, skill.time);
                    }
                }
            }

            else
            {
                if (other_cha.BeHit(skill))
                {
                    if (skill.type == SkillType.Damage)
                    {
                        Console.WriteLine("{0}受到伤害{1}点，当前HP:{2}", other_cha.name, skill.damage, other_cha.hp);

                    }
                    else if (skill.type == SkillType.DamageOverTime)
                    {
                        Console.WriteLine("{0}中了状态", other_cha.name);
                    }
                    else if (skill.type == SkillType.Execute)
                    {
                        Console.WriteLine("{0}被处决了！", other_cha.name);
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        static Character StageBattle()
        {
            int round = 1;
            Character victory_cha;

            while (player.hp > 0 && monster.hp > 0)
            {
                Console.WriteLine("--------第{0}回合开始--------------", round);

                ChaAct(player, monster);
                if (monster.hp <= 0)
                {
                    break;
                }
                ChaAct(monster, player);

                ShowInfo(player);
                ShowInfo(monster);
                Console.WriteLine();
                round += 1;
            }

            if (player.hp > 0)
            {
                victory_cha = player;
            }
            else
            {
                victory_cha = monster;
            }
            return victory_cha;
        }
        // -------------------

        static void Main(string[] args)
        {
            Character.random = random;
            player = CreateSimon();

            int victory_times = 0;
            while (true)
            {
                InitStage();
                Character victory_cha = StageBattle();
                if (victory_cha == player)
                {
                    Console.WriteLine("{0}战胜了敌人{1}，进入下一关", player.name, monster.name);
                    victory_times += 1;
                    Console.WriteLine();
                }
                else
                {
                    break;
                }
            }

            Console.WriteLine("{0}失败了", player.name);
            Console.WriteLine("这次冒险一共前进了{0}关", victory_times);
            Console.WriteLine("GAME OVER");

            Console.ReadLine();
        }
    }
}

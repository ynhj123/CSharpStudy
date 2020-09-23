using System;
using System.Collections.Generic;

namespace RoundBattle
{
    enum SkillType
    {
        Damage,
        DamageOverTime,
        Heal,
        Execute,
        Invincible,
    }

    enum StateType
    {
        DamageOverTime,
        Invincible,
    }
}

namespace RoundBattle
{
    class Character
    {
        public static Random random = null;

        public string name;
        public int hp;
        public List<Skill> skills = new List<Skill>();

        // 正在生效的状态
        public List<State> states = new List<State>();

        public bool AddState(State state)
        {
            states.Add(state);
            return true;
        }

        public bool HasState(StateType state_type)
        {
            for (int i = 0; i < states.Count; ++i)
            {
                if (states[i].time > 0 && states[i].type == state_type)
                {
                    return true;
                }
            }
            return false;
        }

        public void AddSkill(Skill skill)
        {
            skills.Add(skill);
        }

        public Skill GetSkill(int skill_index)
        {
            Skill skill = skills[skill_index];
            return skill;
        }

        public Skill RandSkill()
        {
            int r = random.Next(skills.Count);
            return GetSkill(r);
        }

        private void CostHp(int n)
        {
            if (IsInvincible())
            {
                return;
            }
            hp -= n;
            if (hp < 0)
            {
                hp = 0;
            }
        }

        public bool BeHit(Skill skill)
        {
            if (hp <= 0)
            {
                return false;
            }
            switch (skill.type)
            {
                case SkillType.Damage:

                    CostHp(skill.damage);
                    break;
                case SkillType.DamageOverTime:
                    State temp = new State(StateType.DamageOverTime, skill.time_DOT, skill.damage_DOT);
                    temp.name = skill.name;
                    AddState(temp);
                    break;
                case SkillType.Heal:
                    hp += skill.heal;
                    break;
                case SkillType.Execute:
                    CostHp(hp);
                    break;
                case SkillType.Invincible:
                    State invincible = new State(StateType.Invincible, skill.time);
                    invincible.name = skill.name;
                    AddState(invincible);
                    break;
            }
            return true;
        }

        public bool IsInvincible()
        {
            foreach (var state in states)
            {
                if ("无敌".Equals(state.name) && state.time != 0)
                {

                    return true;
                }
            }
            return false;

        }

        public List<State> StateTakeEffect()
        {
            List<State> effect_states = new List<State>();
            foreach (State state in states)
            {
                effect_states.Add(state);
                switch (state.type)
                {
                    case StateType.DamageOverTime:
                        CostHp(state.damage);
                        state.time -= 1;
                        break;
                    case StateType.Invincible:
                        state.time -= 1;
                        break;
                }
            }
            for (int i = states.Count - 1; i >= 0; i--)
            {
                if (states[i].time == 0)
                {
                    states.RemoveAt(i);
                }
            }
            return effect_states;
        }

        public bool UseSkill(Skill skill, Character target_cha)
        {
            switch (skill.type)
            {
                case SkillType.Execute:
                    if (target_cha.hp >= skill.max_hp)
                    {
                        return false;
                    }
                    return true;
                default:
                    return true;
            }
        }
    }

    class Skill
    {
        public string name;
        public SkillType type;

        // 攻击类型
        public int damage;

        // 治疗类型
        public int heal;

        // DOT类型
        public int time_DOT;
        public int damage_DOT;

        // 处决类型
        public int max_hp;

        // 无敌类型
        public int time;

        public static Skill CreateDamageSkill(string name, int damage)
        {
            Skill skill = new Skill();
            skill.name = name;
            skill.type = SkillType.Damage;
            skill.damage = damage;

            return skill;
        }

        public static Skill CreateDOTSkill(string name, int time, int damage)
        {
            Skill skill = new Skill();
            skill.name = name;
            skill.type = SkillType.DamageOverTime;
            skill.time_DOT = time;
            skill.damage_DOT = damage;
            return skill;
        }

        public static Skill CreateHealSkill(string name, int heal)
        {
            Skill skill = new Skill();
            skill.name = name;
            skill.type = SkillType.Heal;
            skill.heal = heal;
            return skill;
        }

        public static Skill CreateExecuteSkill(string name, int hp)
        {
            Skill skill = new Skill();
            skill.name = name;
            skill.type = SkillType.Execute;
            skill.max_hp = hp;
            return skill;
        }
        public static Skill CreateInvincibleSkill(string name, int time)
        {
            Skill skill = new Skill();
            skill.name = name;
            skill.type = SkillType.Invincible;
            skill.time = time;
            return skill;
        }
    }

    class State
    {
        public StateType type;
        public string name;
        public int damage;
        public int time;

        public State()
        {
        }

        public State(StateType t, int d1, int d2)
        {
            type = t;
            switch (t)
            {
                case StateType.DamageOverTime:
                    time = d1;
                    damage = d2;
                    break;


            }
        }
        public State(StateType t, int d1)
        {
            type = t;
            switch (t)
            {
                case StateType.Invincible:
                    time = d1;
                    break;
            }
        }

        public override string ToString()
        {
            return name + "(" + time + ")";
        }

        public State Copy()
        {
            State s = new State();
            s.type = type;
            s.name = name;
            s.damage = damage;
            s.time = time;
            return s;
        }
    }

}

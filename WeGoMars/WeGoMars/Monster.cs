
namespace WeGoMars
{
    public class Monster : Character
    {
        public Monster(string name, string job, int level, int atk, int def, int maxHp, int maxMp, int hp, int mp, int gold, int exp, List<Skill> skillList, List<Item> inventory)
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            MaxHp = maxHp;
            MaxMp = maxMp;
            Hp = hp;
            Mp = mp;
            Gold = gold;
            Exp = exp;
            SkillList = skillList;
            Inventory = inventory;
        }

        public override void Attack(int damage)
        {

        }

        public override void TakeDamage(int damage)
        {

        }
    }
}
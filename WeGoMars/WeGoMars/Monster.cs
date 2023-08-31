
namespace WeGoMars
{
    public class Monster : Character
    {
        public Monster(string name, string job, int level, float atk, int def, int maxHp, int maxMp, int hp, int mp, int gold, int exp, List<Skill> skillList, List<Item> inventory)
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

        public override float Attack()
        {

            Random random = new Random();
            float per = random.Next(90, 111) / 100; //10%
            float damage = (Atk * per);

            return damage;
        }

        public override void TakeDamage(float damage)
        {
            int d = Convert.ToInt32(Math.Round(damage));
            if (damage > 0)
            {
                Hp -= d;
                if (Hp < 0)
                {
                    Hp = 0;
                }
            }
        }
    }
}
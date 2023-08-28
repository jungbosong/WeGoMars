
namespace WeGoMars{

	public struct Skill
	{
		public string Name { get; set; }
		public float AttackBonus {get; set; }
		public int TargetCount { get; set; }
		public int MpCost { get; set; }
	}

	public abstract class Character
	{
		public string Name { get; set; }
		public string Job { get; set; }
		public int Level { get; set; }
		public int Atk { get; set; }
		public int Def { get; set; }
		public int Hp { get; set; }
		public int Mp { get; set; }
		public int Gold { get; set; }
		public int Exp { get; set; }
		public List<Skill> SkillList { get; set; }
		public List<Item> Inventory { get; set; }


		public Character(string name, string job, int level, int atk, int def, int hp, int mp, int gold, int exp, List<Skill> skillList, List<Item> inventory)
		{
			Name = name;
			Job = job;
			Level = level;
			Atk = atk;
			Def = def;
			Hp = hp;
			Mp = mp;
			Gold = gold;
			Exp = exp;
			SkillList = skillList;
			Inventory = inventory;
		}
		public abstract void Attack(int damage);

		public abstract void TakeDamage(int damage);

		public Item DropItem()
		{
			int randomNum = new Random().Next(0, Inventory.Count);
			Item dropItem = Inventory[randomNum];
			Inventory.Remove(dropItem);
			return dropItem;

		}

		public bool IsDead()
		{
			if (this.Hp < 0)
				return true;
			else
				return false;
		}
	}
}
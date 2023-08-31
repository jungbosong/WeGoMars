
namespace WeGoMars{

	public struct Skill
	{
		public string Name { get; set; }
		public float AttackBonus {get; set; }
		public int TargetCount { get; set; }
		public int MpCost { get; set; }

		public Skill(string name, float attackBonus, int targetCount, int mpCost)
		{
			Name = name;
			AttackBonus = attackBonus;
			TargetCount = targetCount;
			MpCost = mpCost;
		}
	}

	public abstract class Character
	{
		public string Name { get; set; }
		public string Job { get; set; }
		public int Level { get; set; }
		public float Atk { get; set; }
		public int Def { get; set; }
		public int MaxHp { get; set; }
		public int MaxMp { get; set; }
		public int Hp { get; set; }
		public int Mp { get; set; }
		public int Gold { get; set; }
		public int Exp { get; set; }
		public List<Skill> SkillList { get; set; }
		public List<Item> Inventory { get; set; }


		public abstract float Attack();

		public abstract void TakeDamage(float damage);

		public Item DropItem()
		{
			int randomNum = new Random().Next(0, Inventory.Count);
			Item dropItem = Inventory[randomNum];
			Inventory.Remove(dropItem);
			return dropItem;
		}

		public bool IsDead()
		{
			if (this.Hp <= 0)
				return true;
			else
				return false;
		}
	}
}
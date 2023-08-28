
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
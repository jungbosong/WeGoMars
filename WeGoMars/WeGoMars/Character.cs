
namespace WeGoMars{

	public struct Skill
	{
		public string Name { get; set; }
		public float AttackBonus {get; set; }
		public int TargetCount { get; set; }
		public int MpCost { get; set; }
	}

	abstract class Character
	{
		public string Name { get; }
		public string Job { get; }
		public int Level { get; }
		public int Atk { get; }
		public int Def { get; }
		public int Hp { get; }
		public int Mp { get; }
		public int Gold { get; }
		public int Exp { get; }
		public List<Skill> SkillList;
		public List<Item> Inventory;

		public void Attack(int damage);

		public void TakeDamage(int damage);

		public void DropItem();

		public bool IsDead();
	}
}
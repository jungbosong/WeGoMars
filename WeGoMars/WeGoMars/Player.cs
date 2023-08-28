
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace WeGoMars
{

    public class Player : Character
    {

        public List<Item> EquippedItems { get; }
        public int HealthPotionCnt { get; set; }
        public int ManaPotionCnt { get; set; }

        public Player(string name, string job, int level, int atk, int def, int hp, int mp, int gold, int exp,
                      List<Skill> skillList, List<Item> inventory, List<Item> equippedItems, int healthPotionCnt, int manaPotionCnt)
                       : base(name, job, level, atk, def, hp, mp, gold, exp, skillList, inventory)
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
            EquippedItems = equippedItems;
            HealthPotionCnt = healthPotionCnt;
            ManaPotionCnt = manaPotionCnt;
        }

        public override void Attack(int damage)
        {

        }

        public override void TakeDamage(int damage)
        {

        }

        public void GetItem(Item item)
        {
            Inventory.Add(item);
        }

        public void EquipItem(Item item)
        {
            if (Inventory.Contains(item))
            {
                if (EquippedItems.Contains(item))
                    UnEquipItem(item);
                else
                {
                    foreach (Item equippeditem in EquippedItems)
                    {
                        if (equippeditem.ItemType == item.ItemType)
                        {
                            UnEquipItem(equippeditem);
                        }
                    }
                    EquippedItems.Add(item);
                }
            }
            else
            {
                Inventory.Add(item);
                EquippedItems.Add(item);
            }
        }

        public void UnEquipItem(Item item)
        {
            if (EquippedItems.Contains(item))
            {
                EquippedItems.Remove(item);
            }
        }
        public void BuyItem(Item item)
        {
            if (!Inventory.Contains(item))
            {
                Inventory.Add(item);
                this.Gold -= item.Price;
            }
        }

        public void SellItem(Item item, int gold)
        {
            if (Inventory.Contains(item))
            {
                if (EquippedItems.Contains(item))
                {
                    EquippedItems.Remove(item);
                }
                Inventory.Remove(item);
                Gold += gold;
            }
        }
    }
}
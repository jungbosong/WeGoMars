
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace WeGoMars
{

    public class Player : Character
    {

        public List<Item> EquippedItems { get; }
        public int HealthPotionCnt { get; set; }
        public int ManaPotionCnt { get; set; }

        public Player(string name, string job, int level, float atk, int def, int maxHp, int maxMp, int hp, int mp, int gold, int exp,
                      List<Skill> skillList, List<Item> inventory, List<Item> equippedItems, int healthPotionCnt, int manaPotionCnt)
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

        public void ObtainItem(Item item)
        {
            Inventory.Add(item);
        }

        public void GainExp(int exp)
        {
            int levelExp = 0;
            Exp += exp;
            for (int i = 1; i <= Level; i++)
            {
                levelExp += LevelUpExp(i);
                if (Exp >= levelExp)
                    LevelUp();
            }
        }

        public int LevelUpExp(int level)
        {
            int reqExp = 10;
            if (level > 1)
                reqExp = level * 5 + 15;
            return reqExp;
        }

        void LevelUp()
        {
            Level++;
            Atk += 0.5f;
            Def++;
        }

        public float GetTotalAtk()
        {
            float totalAtk = Atk;
            foreach (Item item in EquippedItems)
            {
                totalAtk += item.Atk;
            }
            return totalAtk;
        }

        public int GetTotalDef()
        {
            int totalDef = Def;
            foreach (Item item in EquippedItems)
            {
                totalDef += item.Def;
            }
            return totalDef;
        }

        public void EquipItem(int idx)
        {
            /*if (EquippedItems.Contains(item))
                UnEquipItem(item);
            else
            {
                foreach (Item equippeditem in EquippedItems)
                {
                    if (equippeditem.Type == item.Type)
                    {
                        UnEquipItem(equippeditem);
                    }
                }
                EquippedItems.Add(item);
                item.Equipped = true;
                MaxHp += item.Hp;
                MaxMp += item.Mp;
            }*/
            foreach (Item equippeditem in EquippedItems)
            {
                if (equippeditem.Type == Inventory[idx].Type && equippeditem.Name == Inventory[idx].Name)
                {
                    UnEquipItem(equippeditem);
                    Inventory[idx].Equipped = false;
                    return;
                }
            }
            EquippedItems.Add(Inventory[idx]);
            Inventory[idx].Equipped = true;
            MaxHp += Inventory[idx].Hp;
            MaxMp += Inventory[idx].Mp;
        }

        public void UnEquipItem(Item item)
        {
            if (EquippedItems.Contains(item))
            {
                EquippedItems.Remove(item);
                MaxHp -= item.Hp;
                MaxMp -= item.Mp;
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
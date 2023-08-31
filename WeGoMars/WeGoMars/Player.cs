
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

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

        public override float Attack()
        {

            Random random = new Random();
            float per =random.Next(90,111)/100; //10%
            float damage = ( GetTotalAtk() *per);

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

        public void EquipItem(Item item)
        {
            if (Inventory.Contains(item))
            {
                if (EquippedItems.Contains(item))
                {
                    UnEquipItem(item);
                    item.Equipped = false;
                }
                else
                {
                    foreach (Item equippeditem in EquippedItems)
                    {
                        if (equippeditem.Type == item.Type)
                        {
                            UnEquipItem(equippeditem);
                            item.Equipped = false;
                            break;
                        }
                    }
                    EquippedItems.Add(item);
                    item.Equipped = true;
                    MaxHp += item.Hp;
                    MaxMp += item.Mp;
                }
            }
            else
            {
                Inventory.Add(item);
                EquippedItems.Add(item);
                item.Equipped = true;
                MaxHp += item.Hp;
                MaxMp += item.Mp;
            }
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
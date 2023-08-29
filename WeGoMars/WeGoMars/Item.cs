using System;

namespace WeGoMars
{
    public enum ItemType { Weapon, Armor }

    public class Item
    {
        public string Code { get; }
        public string Name { get; }
        public ItemType Type{ get; }
        public string Info { get; }
        public float Atk { get; }
        public int Def { get; }
        public int Hp { get; }
        public int Mp { get; }
        public int Price { get; }
        public bool Equipped { get; set; }

        public Item(string code, string name, ItemType type, string info, float atk, int def, int hp, int mp,int price, bool equipped = false)
        {
            Code = code;
            Name = name;
            Type = type;
            Info = info;
            Atk = atk;
            Def = def;
            Hp = hp;
            Mp = mp;
            Price = price;
            Equipped = equipped;
        }
    }
}


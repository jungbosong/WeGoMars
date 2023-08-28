using System;

namespace WeGoMars
{
    public class Item
    {
        public string Name { get; }
        public string Code { get; }
        public string ItemType { get; }
        public string Info { get; }
        public int Atk { get; }
        public int Def { get; }
        public int Hp { get; }
        public int Price { get; }

        public Item(string name, string code, string itemType, string info, int atk, int def, int hp, int price)
        {
            Name = name;
            Code = code;
            ItemType = itemType;
            Info = info;
            Atk = atk;
            Def = def;
            Hp = hp;
            Price = price;
        }
    }
}


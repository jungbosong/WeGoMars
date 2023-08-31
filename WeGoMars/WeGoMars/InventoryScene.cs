using System;
using System.Numerics;
using static System.Formats.Asn1.AsnWriter;

namespace WeGoMars
{
    internal class InventoryScene : Displayer
    {
        List<string> itemList = new List<string>();
        
        public void DisplayInventory()
        {
            SetTitle($"{MsgDefine.INVENTORY}\n");
            Console.Write(MsgDefine.EXPLAN_INVENTORY);
            Console.WriteLine();

            SetItemList();
            WriteItemList(0, 4, itemList);

            SetAction($"1. {MsgDefine.MANAGE_EQUIP}2. {MsgDefine.SORT_ITEM}0. {MsgDefine.OUT}");
            int input = CheckValidInput(0, 2);
            switch (input)
            {
                case 0:
                    Managers.MainScene.DisplayMain();
                    break;
                case 1:
                    DisplayManageEquipment();
                    break;
                case 2:
                    DisplaySortItem();
                    break;
            }
        }

        public void DisplayManageEquipment()
        {
            SetTitle($"{MsgDefine.INVENTORY}-{MsgDefine.MANAGE_EQUIP}");
            Console.Write(MsgDefine.EXPLAN_EQUIP);

            SetItemList();
            WriteOptionalItemsList(0, 10, itemList, ConsoleColor.Green);

            SetAction($"0. {MsgDefine.OUT}");
            int input = CheckValidInput(0, Managers.Player.Inventory.Count);
            if (input == 0)
            {
                DisplayInventory();
            }
            else
            {
                Managers.Player.EquipItem(Managers.Player.Inventory[--input]);
                DisplayManageEquipment();
            }
        }

        public void DisplaySortItem()
        {
            SetTitle($"{MsgDefine.INVENTORY} - {MsgDefine.SORT_ITEM}");
            Console.Write(MsgDefine.EXPLAN_INVENTORY);
            Console.WriteLine();

            SetItemList();
            WriteItemList(0, 10, itemList);

            SetAction($"1. {MsgDefine.NAME}2. {MsgDefine.EQUIPPED}3. {MsgDefine.OFFENSIVE_POWER}\n4. {MsgDefine.DEFENSIVE_POWER}\n0. {MsgDefine.OUT}");
            int input = CheckValidInput(0, 4);
            switch (input)
            {
                case 0:
                    DisplayInventory();
                    break;
                case 1:
                    SortItemList(MsgDefine.NAME);
                    DisplaySortItem();
                    break;
                case 2:
                    SortItemList(MsgDefine.EQUIPPED);
                    DisplaySortItem();
                    break;
                case 3:
                    SortItemList(MsgDefine.OFFENSIVE_POWER);
                    DisplaySortItem();
                    break;
                case 4:
                    SortItemList(MsgDefine.DEFENSIVE_POWER);
                    DisplaySortItem();
                    break;
            }
        }

        public void SetItemList()
        {
            itemList.Clear();
            itemList.Add($"{MsgDefine.LIST_ITEM}\n");

            foreach (Item item in Managers.Player.Inventory)
            {
                string tmp = "";
                if (item.Equipped)
                {
                    tmp += MsgDefine.EQUIP;
                }
                if (item.Type == ItemType.Armor)
                {
                    tmp += $"{item.Name}|{MsgDefine.DEFENSIVE_POWER} +{item.Def}|{item.Info}\n";
                }
                else
                {
                    tmp += $"{item.Name}|{MsgDefine.OFFENSIVE_POWER} +{item.Atk}|{item.Info}\n";
                }
                itemList.Add(tmp);
            }
        }

        public void SortItemList(string sortBy)
        {
            switch (sortBy)
            {
                case MsgDefine.NAME:
                    Managers.Player.Inventory = Managers.Player.Inventory.OrderByDescending(item => item.Name.Replace(" ", string.Empty).Length).ToList();
                    break;
                case MsgDefine.EQUIPPED:
                    Managers.Player.Inventory = Managers.Player.Inventory.OrderByDescending(item => item.Equipped).ToList();
                    break;
                case MsgDefine.OFFENSIVE_POWER:
                    Managers.Player.Inventory = Managers.Player.Inventory.OrderByDescending(item => item.Atk).ToList();
                    break;
                case MsgDefine.DEFENSIVE_POWER:
                    Managers.Player.Inventory = Managers.Player.Inventory.OrderByDescending(item => item.Def).ToList();
                    break;
            }
        }
    }
}
using System;
using System.Numerics;

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
            WriteItemList(itemList);

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
                    //DisplaySortItem();
                    break;
            }
        }

        public void DisplayManageEquipment()
        {
            SetTitle($"{MsgDefine.INVENTORY}-{MsgDefine.MANAGE_EQUIP}");
            Console.Write(MsgDefine.EXPLAN_EQUIP);

            SetItemList();
            WriteOptionalItemsList(itemList, ConsoleColor.Green);

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
                    tmp += string.Format("{0,-15}|{1,-10} +{2}|{3,-30}\n", item.Name, MsgDefine.DEFENSIVE_POWER, item.Def, item.Info);
                }
                else
                {
                    tmp += string.Format("{0,-15}|{1,-10} +{2}|{3,-30}\n", item.Name, MsgDefine.OFFENSIVE_POWER, item.Atk, item.Info);
                }

                itemList.Add(tmp);
            }
        }
    }
 }
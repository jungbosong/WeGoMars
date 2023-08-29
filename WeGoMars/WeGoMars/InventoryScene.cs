using System;
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
            Console.Write(itemList[0]);
            for (int i = 1; i < itemList.Count; i++)
            {
                Console.Write($"- {itemList[i]}");
            }
            Console.WriteLine();

            SetAction($"1. {MsgDefine.MANAGE_EQUIP}2. {MsgDefine.SORT_ITEM}0. {MsgDefine.OUT}");
            int input = CheckValidInput(0, 2);
            switch (input)
            {
                case 0:
                    Managers.MainScene.DisplayMain();
                    break;
                case 1:
                    //DisplayManageEquipment();
                    break;
                case 2:
                    //DisplaySortItem();
                    break;
            }
        }

        public void SetItemList()
        {
            itemList.Clear();
            itemList.Add($"{MsgDefine.LIST_ITEM}\n");

            foreach (Item item in Managers.GameData.GetPlayer(0).Inventory)
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
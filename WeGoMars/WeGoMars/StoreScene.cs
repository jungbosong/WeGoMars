using static System.Formats.Asn1.AsnWriter;
using System.Numerics;
using System.Linq;
using static WeGoMars.Player;
using System;

namespace WeGoMars
{
    internal class StoreScene: Displayer
    {
        List<string> storeItemList = new List<string>();
        List<string> sellItemList = new List<string>();

        public void DisplayStore()
        {
            SetTitle($"{MsgDefine.STORE}\n");
            Console.Write(MsgDefine.EXPLAN_STORE);
            Console.WriteLine();

            Console.Write(MsgDefine.GOLD_POSSESSION);
            Console.WriteLine($"{Managers.Player.Gold} {MsgDefine.GOLD}\n");

            SetStoreItemList();
            WriteItemList(storeItemList);

            SetAction($"1. {MsgDefine.PURCHASE_ITEM}2. {MsgDefine.SELL_ITEM}0. {MsgDefine.OUT}");
            int input = CheckValidInput(0, 2);
            switch (input)
            {
                case 0:
                    Managers.MainScene.DisplayMain();
                    break;
                case 1:
                    DisplayPurchase();
                    break;
                case 2:
                    DisplaySell();
                    break;
            }
        }

        public void DisplayPurchase()
        {
            SetTitle($"{MsgDefine.STORE} - {MsgDefine.PURCHASE_ITEM}\n");
            Console.Write(MsgDefine.EXPLAN_STORE);
            Console.WriteLine();

            Console.Write(MsgDefine.GOLD_POSSESSION);
            Console.WriteLine($"{Managers.Player.Gold} {MsgDefine.GOLD}\n");

            SetStoreItemList();
            WriteOptionalItemsList(storeItemList, ConsoleColor.Green);

            SetAction($"0. {MsgDefine.OUT}");
            int input = CheckValidInput(0, Managers.GameData.GetItemList().Count);
            if (input == 0)
            {
                DisplayStore();
            }
            else
            {
                DisplayPurchaseResult(Managers.Player.PurchaseItem(Managers.GameData.GetItemList()[--input]));
                Thread.Sleep(1000);
                DisplayPurchase();
            }
        }

        public void DisplayPurchaseResult(PurchaseType result)
        {
            switch (result)
            {
                case PurchaseType.LackGold:
                    Managers.FontColorChanger.Write(ConsoleColor.DarkRed, $"\n{MsgDefine.LACK_GOLD}");
                    break;
                case PurchaseType.Success:
                    Managers.FontColorChanger.Write(ConsoleColor.Blue, $"\n{MsgDefine.SUCCESS}");
                    break;
            }
        }

        public void DisplaySell()
        {
            SetTitle($"{MsgDefine.STORE} - {MsgDefine.SELL_ITEM}\n");
            Console.Write(MsgDefine.EXPLAN_STORE);
            Console.WriteLine();

            Console.Write(MsgDefine.GOLD_POSSESSION);
            Console.WriteLine($"{Managers.Player.Gold} {MsgDefine.GOLD}\n");

            SetSellItemList();
            WriteOptionalItemsList(sellItemList, ConsoleColor.Green);

            SetAction($"0. {MsgDefine.OUT}");
            int input = CheckValidInput(0, Managers.Player.Inventory.Count);
            if (input == 0)
            {
                DisplayStore();
            }
            else
            {
                Managers.Player.SellItem(Managers.Player.Inventory[--input]);
                DisplaySell();
            }
        }

        public void SetStoreItemList()
        {
            storeItemList.Clear();
            storeItemList.Add($"{MsgDefine.LIST_ITEM}\n");

            foreach (Item item in Managers.GameData.GetItemList())
            {
                string tmp = "";
                if (item.Type == ItemType.Armor)
                {
                    tmp += string.Format("{0,-15}|{1,-10} +{2}|{3,-30}|", item.Name, MsgDefine.DEFENSIVE_POWER, item.Def, item.Info);
                }
                else
                {
                    tmp += string.Format("{0,-15}|{1,-10} +{2}|{3,-30}|", item.Name, MsgDefine.OFFENSIVE_POWER, item.Atk, item.Info);
                }
                tmp += string.Format("{0,10} G\n", item.Price);
                storeItemList.Add(tmp);
            }
        }

        public void SetSellItemList()
        {
            sellItemList.Clear();
            sellItemList.Add($"{MsgDefine.LIST_ITEM}\n");

            foreach (Item item in Managers.Player.Inventory)
            {
                string tmp = "";
                if (item.Type == ItemType.Armor)
                {
                    tmp += string.Format("{0,-15}|{1,-10} +{2}|{3,-30}|{4,-10} G\n", 
                        item.Name, MsgDefine.DEFENSIVE_POWER, item.Def, item.Info, (int)Math.Round(item.Price * 0.85f, 1));
                }
                else
                {
                    tmp += string.Format("{0,-15}|{1,-10} +{2}|{3,-30}|{4,-10} G\n", 
                        item.Name, MsgDefine.OFFENSIVE_POWER, item.Atk, item.Info, (int)Math.Round(item.Price * 0.85f, 1));
                }

                sellItemList.Add(tmp);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeGoMars
{
    internal class MainScene: Displayer
    {
        public void DisplayMain()
        {
            SetTitle(MsgDefine.MAIN);
            Console.Write(MsgDefine.OPENING_PHARASE);

            SetAction($"1. {MsgDefine.SHOW_STATE}2. {MsgDefine.START_BATTLE}3. {MsgDefine.INVENTORY}\n4. {MsgDefine.STORE}\n5. {MsgDefine.RECOVERY}\n6. {MsgDefine.CHARACTER_SAVE}");
            int input = CheckValidInput(1, 6);
            switch (input)
            {
                case 1:
                    Managers.StatusScene.DisplayStatus();
                    break;
                case 2:
                    Managers.DungeonScene.DisplayDungeonSelect();
                    break;
                case 3:
                    Managers.InventoryScene.DisplayInventory();
                    break;
                case 4:
                    Managers.StoreScene.DisplayStore();
                    break;
                case 5:
                    Managers.RecoveryScene.DisplayRecovery();
                    break;
                case 6:
                    Managers.SaveCharacterScene.DisplaySaveCharacter();
                    break;
            }
        }
    }
}

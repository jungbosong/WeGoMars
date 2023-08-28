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

            SetAction($"1. {MsgDefine.SHOW_STATE}2. {MsgDefine.INVENTORY}\n3. {MsgDefine.STORE}");
            int input = CheckValidInput(1, 3);
            switch (input)
            {
                case 1:
                    Managers.StatusScene.DisplayStatus();
                    break;
                case 2:
                    //DisplayDunjeon();
                    break;
                case 3:
                    //DisplayInventory();
                    break;
            }
        }
    }
}

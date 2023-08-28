using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace WeGoMars
{
    internal class StatusScene: Displayer
    {
        List<string> myInfo = new List<string>();
        public void DisplayStatus()
        {
            SetTitle(MsgDefine.SHOW_STATE);
            Console.Write(MsgDefine.EXPLAN_STATE);

            SetStatus();
            foreach (string info in myInfo)
            {
                Console.Write(info);
            }

            Console.WriteLine();

            SetAction($"0. {MsgDefine.OUT}");
            int input = CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    Managers.MainScene.DisplayMain();
                    break;
            }
        }

        void SetStatus()
        {
            myInfo.Clear();
            //Managers.Player.UpdateInfo();
            string tmp = "";
            if (Managers.Player.Level < 10)
            {
                tmp = $"{MsgDefine.LEVEL}0{Managers.Player.Level}\n";
            }
            else
            {
                tmp = $"{MsgDefine.LEVEL}{Managers.Player.Level}\n";
            }
            myInfo.Add(tmp);

            tmp = $"{MsgDefine.JOB} ( {Managers.Player.Job} )\n";
            myInfo.Add(tmp);

            /*if (player.increasedAtk == 0)
            {
                tmp = $"{MsgDefine.OFFENSIVE_POWER} : {player.atk}\n";
                myInfo.Add(tmp);
            }
            else
            {
                tmp = $"{MsgDefine.OFFENSIVE_POWER} : {player.atk} (+{player.increasedAtk})\n";
                myInfo.Add(tmp);
            }

            if (player.increasedDef == 0)
            {
                tmp = $"{MsgDefine.DEFENSIVE_POWER} : {player.def}\n";
                myInfo.Add(tmp);
            }
            else
            {
                tmp = $"{MsgDefine.DEFENSIVE_POWER} : {player.def} (+{player.increasedDef})\n";
                myInfo.Add(tmp);
            }*/

            tmp = $"{MsgDefine.HP} : {Managers.Player.Hp}\n";
            myInfo.Add(tmp);

            tmp = $"{MsgDefine.GOLD} : {Managers.Player.Gold} G\n";
            myInfo.Add(tmp);
        }
    }
}

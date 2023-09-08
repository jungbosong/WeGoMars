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
            
            if (Managers.Player.Level < 10)
            {
                myInfo.Add($"{MsgDefine.LEVEL}0{Managers.Player.Level}\n");
            }
            else
            {
                myInfo.Add($"{MsgDefine.LEVEL}{Managers.Player.Level}\n");
            }
            
            myInfo.Add($"{MsgDefine.NICKNAME} {Managers.Player.Name}\n");

            myInfo.Add($"{MsgDefine.JOB} ( {Managers.Player.Job} )\n");

            myInfo.Add($"{MsgDefine.HP} : {Managers.Player.Hp}\n");

            myInfo.Add($"{MsgDefine.MP} : {Managers.Player.Mp}\n");

            myInfo.Add($"{MsgDefine.OFFENSIVE_POWER} : {Managers.Player.GetTotalAtk()}(+{Managers.Player.GetTotalAtk() - Managers.Player.Atk})\n");

            myInfo.Add($"{MsgDefine.DEFENSIVE_POWER} : {Managers.Player.GetTotalDef()}(+{Managers.Player.GetTotalDef() - Managers.Player.Def})\n");

            myInfo.Add($"{MsgDefine.GOLD_IN_HAND} : {Managers.Player.Gold} {MsgDefine.GOLD}\n");
            
            myInfo.Add($"\n{MsgDefine.LIST_SKILL}");
            foreach (Skill skill in Managers.Player.SkillList)
            {
                if (skill.TargetCount > 1)
                {
                    myInfo.Add($"{skill.Name} - MP: {skill.MpCost}\n\t{MsgDefine.OFFENSIVE_POWER} * {skill.AttackBonus} 로 {skill.TargetCount}명의 적을 랜덤으로 1회 공격합니다.\n");
                }
                else
                {
                    myInfo.Add($"{skill.Name} - MP: {skill.MpCost}\n\t{MsgDefine.OFFENSIVE_POWER} * {skill.AttackBonus} 로 {skill.TargetCount}명의 적을 공격합니다.\n");
                }
            }
        }
    }
}

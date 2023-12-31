﻿
namespace WeGoMars
{
    internal class SelectCharacterScene : Displayer
    {
        public void DisplaySelectCharacter()
        {
            SetTitle(MsgDefine.CHARACTER_SELECT);
            Console.WriteLine($"\n{MsgDefine.WELCOME_MSG}\n");
            List<(string, string, int)> playersInfo = Managers.GameData.GetPlayersSimpleInfoList();
            DisplayPlayerListWithButton(playersInfo, "[0] 새로 만들기", "");

            int input = CheckValidInput(0, playersInfo.Count - 1);

            if (input == 0)
            {
                Managers.SelectCharacterScene.DisplaySetupCharacterName();
            }
            else
            {
                Managers.Player = Managers.GameData.GetPlayer(input);
            }
        }

        public void DisplaySetupCharacterName()
        {
            SetTitle(MsgDefine.CHARACTER_SETUP);
            Console.WriteLine($"\n{MsgDefine.WELCOME_MSG}\n");
            Console.Write(MsgDefine.INPUT_CHARACTERNAME);
            bool isNameSet = false;
            while (!isNameSet)
            {
                string name = Console.ReadLine();
                if (name != null && name.Length < 31)
                {
                    isNameSet = true;
                    Console.Write($"\n당신의 이름은 ");
                    Managers.FontColorChanger.Write(ConsoleColor.Cyan, name);
                    Console.WriteLine(" 입니다.\n");
                    Managers.Player.Name = name;
                    DisplaySetupCharacterJob();
                }
                else
                {
                    Managers.FontColorChanger.BackgroundWriteLine(ConsoleColor.Red, "이름의 길이는 최대 30글자 입니다.");
                    Console.Write(">>");
                }
            }
        }

        public void DisplaySetupCharacterJob()
        {
            SetTitle(MsgDefine.CHARACTER_SETUP);
            Console.WriteLine();
            Managers.FontColorChanger.Write(ConsoleColor.Cyan, $"{Managers.Player.Name}");
            Console.WriteLine($"{MsgDefine.WELCOME_NAME}\n");

            int x = 0;
            int y = 4;
            int space = 15;
            DisplayJob(x, y, ConsoleColor.Gray, MsgDefine.JOB_1, 10, 5, 100, 50);
            DisplayJob(x + space, y, ConsoleColor.Magenta, MsgDefine.JOB_2, 8, 7, 80, 70);
            DisplayJob(x + 2 * space, y, ConsoleColor.Green, MsgDefine.JOB_3, 12, 8, 150, 25);
            DisplayJob(x + 3 * space, y, ConsoleColor.Yellow, MsgDefine.JOB_4, 6, 6, 70, 100);
            DisplayJob(x + 4 * space, y, ConsoleColor.Red, MsgDefine.JOB_5, 20, 15, 150, 150);
            Console.WriteLine();

            SetAction($"1. {MsgDefine.JOB_1}\n2. {MsgDefine.JOB_2}\n3. {MsgDefine.JOB_3}\n4. {MsgDefine.JOB_4}\n5. {MsgDefine.JOB_5}\n0. {MsgDefine.OUT}");

            Console.SetCursorPosition(0, y + 14);
            Console.Write(MsgDefine.INPUT_CHARACTERJOB);

            int input = CheckValidInput(0, 5);
            switch (input)
            {
                case 0:
                    Managers.SelectCharacterScene.DisplaySelectCharacter();
                    break;
                case 1:
                    SetPlayerJob(MsgDefine.JOB_1, 10, 5, 100, 50);
                    DIsplaySelect();
                    Managers.Player.AddSkill(Managers.GameData.GetSkill(4));
                    break;
                case 2:
                    SetPlayerJob(MsgDefine.JOB_2, 8, 7, 80, 70);
                    DIsplaySelect();
                    break;
                case 3:
                    SetPlayerJob(MsgDefine.JOB_3, 12, 8, 150, 25);
                    Managers.Player.AddSkill(Managers.GameData.GetSkill(3));
                    DIsplaySelect();
                    break;
                case 4:
                    SetPlayerJob(MsgDefine.JOB_4, 6, 6, 70, 100);
                    DIsplaySelect();
                    Managers.Player.AddSkill(Managers.GameData.GetSkill(5));
                    Managers.Player.AddSkill(Managers.GameData.GetSkill(11));
                    break;
                case 5:
                    SetPlayerJob(MsgDefine.JOB_5, 20, 15, 150, 150);
                    DIsplaySelect();
                    Managers.Player.AddSkill(Managers.GameData.GetSkill(3));
                    Managers.Player.AddSkill(Managers.GameData.GetSkill(6));
                    break;
            }
        }

        public void DisplayJob(int x, int y, ConsoleColor consoleColor, string job, int atk, int def, int hp, int mp)
        {
            Console.SetCursorPosition(x, y);
            Console.Write("|    ");
            Managers.FontColorChanger.WriteLine(consoleColor, job);
            Console.SetCursorPosition(x, y + 1);
            Console.WriteLine($"| {MsgDefine.OFFENSIVE_POWER} : {atk}");
            Console.SetCursorPosition(x, y + 2);
            Console.WriteLine($"| {MsgDefine.DEFENSIVE_POWER} : {def}");
            Console.SetCursorPosition(x, y + 3);
            Console.WriteLine($"| {MsgDefine.HP}   : {hp}");
            Console.SetCursorPosition(x, y + 4);
            Console.WriteLine($"| {MsgDefine.MP}   : {mp}");
        }

        public void DIsplaySelect()
        {
            Console.Write($"\n당신은 이제부터 ");
            Managers.FontColorChanger.Write(ConsoleColor.Green, Managers.Player.Name);
            Console.Write(" ( ");
            Managers.FontColorChanger.Write(ConsoleColor.Cyan, Managers.Player.Job);
            Console.Write(" ) ");
            Console.WriteLine($" 입니다.\n\n{MsgDefine.OFFENSIVE_POWER} : {Managers.Player.Atk}\n{MsgDefine.DEFENSIVE_POWER} : {Managers.Player.Def}\n" +
                                            $"{MsgDefine.HP}   : {Managers.Player.Hp}\n{MsgDefine.MP}   : {Managers.Player.Mp}\n");
            Thread.Sleep(2000);
        }

        public void SetPlayerJob(string job, int atk, int def, int hp, int mp)
        {
            Managers.Player.Job = job;
            Managers.Player.Atk = atk;
            Managers.Player.Def = def;
            Managers.Player.MaxHp = hp;
            Managers.Player.Hp = hp;
            Managers.Player.MaxMp = mp;
            Managers.Player.Mp = mp;
        }
    }
}

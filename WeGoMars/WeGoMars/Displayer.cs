using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeGoMars
{
    internal abstract class Displayer
    {
        public void SetTitle(string title)
        {
            Console.Clear();
            Managers.FontColorChanger.Write(ConsoleColor.Yellow, title);
        }

        public void SetAction(string actions)
        {
            Managers.FontColorChanger.BothgroundWrite(ConsoleColor.Black, ConsoleColor.Cyan, actions);
            Console.WriteLine();
            Console.WriteLine();
            Console.Write(MsgDefine.INPUT_ACTION);
        }

        public void DisplayWrongInput()
        {
            Managers.FontColorChanger.WriteLine(ConsoleColor.DarkRed, MsgDefine.WRONG_INPUT);
            Console.Write(MsgDefine.INPUT_ACTION);
        }

        public int CheckValidInput(int min, int max)
        {
            while (true)
            {
                string input = Console.ReadLine();

                bool parseSuccess = int.TryParse(input, out var ret);
                if (parseSuccess)
                {
                    if (ret >= min && ret <= max)
                        return ret;
                }

                DisplayWrongInput();
            }
        }
    }
}

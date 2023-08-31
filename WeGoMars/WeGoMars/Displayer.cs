using System;
using System.Collections.Generic;
using System.Drawing;
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

        public void WriteItemList(int x, int y, List<string> items)
        {
            Console.SetCursorPosition(x, y++);
            Console.Write(items[0]);
            for (int i = 1; i < items.Count; i++)
            { 
                string[] itemInfo = items[i].Split("|");
                Console.SetCursorPosition(x, y);
                Console.Write($"- {itemInfo[0]}");
                Console.SetCursorPosition(x + 20, y);
                Console.Write($"| {itemInfo[1]}");
                Console.SetCursorPosition(x + 35, y);
                Console.Write($"| {itemInfo[2]}");
                y++;
            }
            Console.WriteLine();
        }

        public void WriteOptionalItemsList(int x, int y, List<string> items, ConsoleColor backgroundColor)
        {
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = ConsoleColor.Black;
            for (int i = 1; i < items.Count; i++)
            {
                string[] itemInfo = items[i].Split("|");
                Console.SetCursorPosition(x, y);
                Console.Write($"{i}. {itemInfo[0]}");
                Console.SetCursorPosition(x + 25, y);
                Console.Write($"| {itemInfo[1]}");
                Console.SetCursorPosition(x + 40, y);
                Console.Write($"| {itemInfo[2]}");
                y++;
            }
            Console.ResetColor();
        }

        public void WriteStoreItemList(int x, int y, List<string> items)
        {
            Console.SetCursorPosition(x, y++);
            Console.Write(items[0]);
            for (int i = 1; i < items.Count; i++)
            {
                string[] itemInfo = items[i].Split("|");
                Console.SetCursorPosition(x, y);
                Console.Write($"- {itemInfo[0]}");
                Console.SetCursorPosition(x + 20, y);
                Console.Write($"| {itemInfo[1]}");
                Console.SetCursorPosition(x + 35, y);
                Console.Write($"| {itemInfo[2]}");
                Console.SetCursorPosition(x + 95, y);
                Console.Write($"| {itemInfo[3]}");
                y++;
            }
            Console.WriteLine();
        }

        public void WriteStoreOptionalItemsList(int x, int y, List<string> items, ConsoleColor backgroundColor)
        {
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = ConsoleColor.Black;
            for (int i = 1; i < items.Count; i++)
            {
                string[] itemInfo = items[i].Split("|");
                Console.SetCursorPosition(x, y);
                Console.Write($"{i}. {itemInfo[0]}");
                Console.SetCursorPosition(x + 25, y);
                Console.Write($"| {itemInfo[1]}");
                Console.SetCursorPosition(x + 40, y);
                Console.Write($"| {itemInfo[2]}");
                Console.SetCursorPosition(x + 105, y);
                Console.Write($"| {itemInfo[3]}");
                y++;
            }
            Console.ResetColor();
        }
    }
}

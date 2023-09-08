using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeGoMars
{
    internal class FontColorChanger
    {
        public void Write(ConsoleColor newForeColor, string text)
        {
            Console.ForegroundColor = newForeColor;
            Console.Write(text);
            Console.ResetColor();
        }

        public void WriteLine(ConsoleColor newForeColor, string text)
        {
            Console.ForegroundColor = newForeColor;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public void BackgroundWrite(ConsoleColor newBackColor, string text)
        {
            Console.BackgroundColor = newBackColor;
            Console.Write(text);
            Console.ResetColor();
        }

        public void BackgroundWriteLine(ConsoleColor newBackColor, string text)
        {
            Console.BackgroundColor = newBackColor;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public void BothgroundWrite(ConsoleColor newForeColor, ConsoleColor newBackColor, string text)
        {
            Console.ForegroundColor = newForeColor;
            Console.BackgroundColor = newBackColor;
            Console.Write(text);
            Console.ResetColor();
        }

        public void BothgroundWriteLine(ConsoleColor newForeColor, ConsoleColor newBackColor, string text)
        {
            Console.ForegroundColor = newForeColor;
            Console.BackgroundColor = newBackColor;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}

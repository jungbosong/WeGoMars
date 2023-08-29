using Newtonsoft.Json;
using System.Net.NetworkInformation;
using System.Numerics;

namespace WeGoMars
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = $"{Directory.GetCurrentDirectory()}\\..\\..\\..\\";
            Managers.MainScene.DisplayMain();

        }
    }
}
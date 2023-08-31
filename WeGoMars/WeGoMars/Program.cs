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
            //Managers.SelectCharacterScene.DisplaySelectCharacter();
            //string PlayerData = JsonConvert.SerializeObject(Managers.Player, Formatting.Indented);
            //Console.WriteLine(PlayerData);
            Managers.MainScene.DisplayMain();
        }
    }
}
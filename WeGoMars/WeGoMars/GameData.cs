
using Newtonsoft.Json;
using System.Numerics;

namespace WeGoMars
{
    public class GameData
    {
        private List<Player> players;
        private List<Monster> monsters;
        private List<Item> itemList;
        private string filePath;


        public GameData(string filePath)
        {
            this.filePath = filePath;
            
            if (File.Exists(filePath + "PlayerData.Json"))
            {
                string readItemString = File.ReadAllText(filePath + "ItemData.Json");
                itemList = JsonConvert.DeserializeObject<List<Item>>(readItemString);
            }
            else
                this.itemList = new List<Item>();

            if (File.Exists(filePath + "PlayerData.Json"))
            {
                string readMonsterString = File.ReadAllText(filePath + "MonsterData.Json");
                monsters = JsonConvert.DeserializeObject<List<Monster>>(readMonsterString);
            }
            else
                this.monsters = new List<Monster>();

            if (File.Exists(filePath + "PlayerData.Json"))
            {
                string readPlayerString = File.ReadAllText(filePath + "PlayerData.Json");
                players = JsonConvert.DeserializeObject<List<Player>>(readPlayerString);
            }
            else
                this.players = new List<Player>();
        }

        public Item GetItemFromCode(string code)
        {
            foreach (Item item in itemList)
            {
                if (item.Code == code)
                    return item;
            }
            return null;
        }

        public Player GetPlayer(string name)
        {
            foreach (Player player in players)
            {
                if (player.Name == name)
                    return player;
            }
            return null;
        }

        public Player GetPlayer(int num)
        {
            if (num >= 0 && num < players.Count)
                return players[num];
            else
                return null;
        }

        public Monster GetMonster(int num)
        {
            if (num >= 0 && num < monsters.Count)
                return monsters[num];
            else
                return null;
        }

    }
}
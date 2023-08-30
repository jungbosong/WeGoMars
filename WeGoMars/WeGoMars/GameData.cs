
using Newtonsoft.Json;
using System.Diagnostics;
using System;
using System.Xml.Linq;

namespace WeGoMars
{
    public class GameData
    {
        private List<Item> itemList;
        private List<Skill> skillList;
        private List<Player> players;
        private List<Monster> monsters;
        //private string filePath = $"{Directory.GetCurrentDirectory()}\\";
        private string filePath = $"{Directory.GetCurrentDirectory()}\\..\\..\\..\\";

        public GameData()
        {
            if (File.Exists(filePath + "ItemData.Json"))
            {
                string readItemString = File.ReadAllText(filePath + "ItemData.Json");
                itemList = JsonConvert.DeserializeObject<List<Item>>(readItemString);
            }
            else
            {
                itemList = new List<Item>() { 
                    new Item("0", "�׽�Ʈ ������", ItemType.Weapon, "�׽�Ʈ�� �� �Դϴ�.", 100f, 100, 100, 100, 100000),
                    new Item("1", "���� ��", ItemType.Weapon, "���� �� �� �ִ� ���� ���Դϴ�.", 2f, 0, 0, 0, 1000),
                    new Item("2", "������ ����", ItemType.Armor, "���ÿ� ������ �ִ� �����Դϴ�.", 0f, 5, 0, 0, 2500),
                    new Item("3", "û�� ����", ItemType.Weapon, "��𼱰� ������� ���� �����Դϴ�.", 5f, 0, 0, 0, 2500),
                    new Item("4", "���谩��", ItemType.Armor, "����� ������� ưư�� �����Դϴ�.", 0f, 9, 0, 0, 4500)
                };
            }

            if (File.Exists(filePath + "SkillData.Json"))
            {
                string readSkillString = File.ReadAllText(filePath + "SkillData.Json");
                skillList = JsonConvert.DeserializeObject<List<Skill>>(readSkillString);
            }
            else
            {
                skillList = new List<Skill>() { 
                    new Skill("���� ��Ʈ����ũ", 2f, 1, 10), 
                    new Skill("���� ��Ʈ����ũ", 1.5f, 2, 15),
                    new Skill("������", 3f, 5, 25),
                    new Skill("�ӽ� ���� ��ų 1", 2f, 1, 5), 
                    new Skill("�ӽ� ���� ��ų 2", 1.5f, 2, 10)
                };
            }

            if (File.Exists(filePath + "PlayerData.Json"))
            {
                string readPlayerString = File.ReadAllText(filePath + "PlayerData.Json");
                players = JsonConvert.DeserializeObject<List<Player>>(readPlayerString);
            }
            else
            {
                players = new List<Player>() {
                    new Player("�÷��̾�", "����", 1, 10, 5, 100, 50, 100, 50, 1500, 0,
                    new List<Skill>(){
                        new Skill("���� ��Ʈ����ũ", 2f, 1, 10),
                        new Skill("���� ��Ʈ����ũ", 1.5f, 2, 15),
                        new Skill("������", 3f, 5, 25)},
                    new List<Item>(){
                        itemList[1], 
                        itemList[2]},
                    3, 3) };
            }

            if (File.Exists(filePath + "MonsterData.Json"))
            {
                string readMonsterString = File.ReadAllText(filePath + "MonsterData.Json");
                monsters = JsonConvert.DeserializeObject<List<Monster>>(readMonsterString);
            }
            else
            {
                monsters = new List<Monster>() { 
                    new Monster(name: "�̴Ͼ�", job: "monster", level: 2, atk: 5, def: 0, maxHp: 15, maxMp: 0, hp: 15, mp: 0, gold: 400, exp: 2,
                        new List<Skill>(){ new Skill("����", 1.5f, 1, 5) },
                        new List<Item>() { this.GetItemFromCode("3") }
                        ),
                    new Monster(name: "������", job: "monster", level: 3, atk: 9, def: 0, maxHp: 10, maxMp: 0, hp: 10, mp: 0, gold: 600, exp: 3,
                        new List<Skill>(){ new Skill("�� ������", 1.2f, 2, 7)},
                        new List<Item>() { this.GetItemFromCode("4") }
                        ),
                    new Monster(name: "�����̴Ͼ�", job: "monster", level: 5, atk: 8, def: 0, maxHp: 25, maxMp: 0, hp: 25, mp: 0, gold: 1000, exp: 5,
                        new List<Skill>(){ new Skill("����", 1.5f, 1, 5), new Skill("�� ������", 1.2f, 2, 7)},
                        new List<Item>() { this.GetItemFromCode("3"), this.GetItemFromCode("4") }
                        ),
                };
            }
        }

        public Item GetItemFromCode(string code)
        {
            foreach (Item item in itemList)
            {
                if (item.Code == code)
                    return DeepClone(item);
            }
            return null;
        }

        public Skill GetSkill(int num)
        {
            if (num >= 0 && num < skillList.Count)
                return skillList[num];
            else
                return skillList[0];
        }
        
        public List<Item> GetItemList()
        {
            return itemList;
        }

        public Player GetPlayer(string name)
        {
            foreach (Player player in players)
            {
                if (player.Name == name)
                    return DeepClone(player);
            }
            return null;
        }

        public Player GetPlayer(int num)
        {
            if (num >= 0 && num < players.Count)
                return DeepClone(players[num]);
            else
                return null;
        }

        public Monster GetMonster(int num)
        {
            if (num >= 0 && num < monsters.Count)
                return DeepClone(monsters[num]);
            else
                return null;
        }

        public int GetPlayerListCount()
        {
            return players.Count;
        }

        public List<string> GetPlayerNameList()
        {
            List<string> strings = new List<string>();
            foreach (Player player in players)
            {
                strings.Add(player.Name);
            }
            return strings;
        }

        public void RemovePlayer(int num)
        {
            if (num >= 0 && players.Count > num)
            {
                players.RemoveAt(num);
            }
        }

        public void InsertPlayer(int num, Player player)
        {
            if (num >= 0 && players.Count > num)
            {
                players.RemoveAt(num);
                players.Insert(num, player);
            }
        }

        public void AddPlayer(Player player)
        {
            players.Add(player);
        }

        public void SaveAllData()
        {
            string ItemData = JsonConvert.SerializeObject(itemList, Formatting.Indented);
            string SKillData = JsonConvert.SerializeObject(skillList, Formatting.Indented);
            string PlayerData = JsonConvert.SerializeObject(players, Formatting.Indented);
            string MonsterData = JsonConvert.SerializeObject(monsters, Formatting.Indented);
            File.WriteAllText(filePath + "ItemData.Json", ItemData);
            File.WriteAllText(filePath + "SkillData.Json", SKillData);
            File.WriteAllText(filePath + "PlayerData.Json", PlayerData);
            File.WriteAllText(filePath + "MonsterData.Json", MonsterData);
        }

        T DeepClone<T>(T obj)
        {
            if (obj != null)
            {
                string serialized = JsonConvert.SerializeObject(obj);
                T deepCopied = JsonConvert.DeserializeObject<T>(serialized);
                return deepCopied;
            }
            else
                return default(T);
        }
    }
}
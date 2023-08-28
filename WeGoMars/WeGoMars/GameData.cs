
using Newtonsoft.Json;
using System.Numerics;

namespace WeGoMars
{
    public class GameData
    {
        private List<Item> itemList;
        private List<Skill> skillList;
        private List<Player> players;
        private List<Monster> monsters;
        private string filePath;


        public GameData(string filePath)
        {
            this.filePath = filePath;

            if (File.Exists(filePath + "ItemData.Json"))
            {
                string readItemString = File.ReadAllText(filePath + "ItemData.Json");
                itemList = JsonConvert.DeserializeObject<List<Item>>(readItemString);
            }
            else
            {
                itemList = new List<Item>() { new Item("0", "�׽�Ʈ ������", ItemType.Weapon, "�׽�Ʈ�� �� �Դϴ�.", 100f, 100, 100, 100, 100000),
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
                skillList = new List<Skill>() { new Skill("���� ��Ʈ����ũ", 2f, 1, 10), new Skill("���� ��Ʈ����ũ", 1.5f, 2, 15),
                                                            new Skill("������", 3f, 5, 25),
                                                            new Skill("�ӽ� ���� ��ų 1", 2f, 1, 5), new Skill("�ӽ� ���� ��ų 2", 1.5f, 2, 10)
                                                            };
            }

            if (File.Exists(filePath + "PlayerData.Json"))
            {
                string readPlayerString = File.ReadAllText(filePath + "PlayerData.Json");
                players = JsonConvert.DeserializeObject<List<Player>>(readPlayerString);
            }
            else
            {
                players = new List<Player>() { new Player("�÷��̾�", "����", 1, 10, 5, 100, 50, 100, 50, 1500, 0,
                      new List<Skill>(){new Skill("���� ��Ʈ����ũ", 2f, 1, 10), new Skill("���� ��Ʈ����ũ", 1.5f, 2, 15),
                                        new Skill("������", 3f, 5, 25)},
                      new List<Item>(){ new Item("1", "���� ��", ItemType.Weapon, "���� �� �� �ִ� ���� ���Դϴ�.", 2f, 0, 0, 0, 1000),
                                        new Item("2", "������ ����", ItemType.Armor, "���ÿ� ������ �ִ� �����Դϴ�.", 0f, 5, 0, 0, 2500) },
                      new List<Item>(){ new Item("1", "���� ��", ItemType.Weapon, "���� �� �� �ִ� ���� ���Դϴ�.", 2f, 0, 0, 0, 1000)}, 3, 3)};
            }

            if (File.Exists(filePath + "MonsterData.Json"))
            {
                string readMonsterString = File.ReadAllText(filePath + "MonsterData.Json");
                monsters = JsonConvert.DeserializeObject<List<Monster>>(readMonsterString);
            }
            else 
            {
                monsters = new List<Monster>() { new Monster(name: "�̴Ͼ�", job: "monster", level: 2, atk: 5, def: 0,
                                                                           maxHp: 15, maxMp: 0, hp: 15, mp: 0, gold: 400, exp: 2,
                        new List<Skill>(){ skillList[3] },
                        new List<Item>() { this.GetItemFromCode("3") }
                        ),

                                        new Monster(name: "������", job: "monster", level: 3, atk: 9, def: 0,
                                                                           maxHp: 10, maxMp: 0, hp: 10, mp: 0, gold: 600, exp: 3,
                        new List<Skill>(){ skillList[4]},
                        new List<Item>() { this.GetItemFromCode("4") }
                        ),

                                        new Monster(name: "�����̴Ͼ�", job: "monster", level: 5, atk: 8, def: 0,
                                                                           maxHp: 25, maxMp: 0, hp: 25, mp: 0, gold: 1000, exp: 5,
                        new List<Skill>(){ skillList[3], skillList[4]},
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
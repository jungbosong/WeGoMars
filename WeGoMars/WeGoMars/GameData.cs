
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
                itemList = new List<Item>() { new Item("0", "테스트 아이템", ItemType.Weapon, "테스트용 검 입니다.", 100f, 100, 100, 100, 100000),
                                                        new Item("1", "낡은 검", ItemType.Weapon, "쉽게 볼 수 있는 낡은 검입니다.", 2f, 0, 0, 0, 1000),
                                                        new Item("2", "수련자 갑옷", ItemType.Armor, "수련에 도움을 주는 갑옷입니다.", 0f, 5, 0, 0, 2500),
                                                        new Item("3", "청동 도끼", ItemType.Weapon, "어디선가 사용됬던거 같은 도끼입니다.", 5f, 0, 0, 0, 2500),
                                                        new Item("4", "무쇠갑옷", ItemType.Armor, "무쇠로 만들어져 튼튼한 갑옷입니다.", 0f, 9, 0, 0, 4500)
                                                        };
            }

            if (File.Exists(filePath + "SkillData.Json"))
            {
                string readSkillString = File.ReadAllText(filePath + "SkillData.Json");
                skillList = JsonConvert.DeserializeObject<List<Skill>>(readSkillString);
            }
            else
            {
                skillList = new List<Skill>() { new Skill("알파 스트라이크", 2f, 1, 10), new Skill("더블 스트라이크", 1.5f, 2, 15),
                                                            new Skill("열파참", 3f, 5, 25),
                                                            new Skill("임시 몬스터 스킬 1", 2f, 1, 5), new Skill("임시 몬스터 스킬 2", 1.5f, 2, 10)
                                                            };
            }

            if (File.Exists(filePath + "PlayerData.Json"))
            {
                string readPlayerString = File.ReadAllText(filePath + "PlayerData.Json");
                players = JsonConvert.DeserializeObject<List<Player>>(readPlayerString);
            }
            else
            {
                players = new List<Player>() { new Player("플레이어", "전사", 1, 10, 5, 100, 50, 100, 50, 1500, 0,
                      new List<Skill>(){new Skill("알파 스트라이크", 2f, 1, 10), new Skill("더블 스트라이크", 1.5f, 2, 15),
                                        new Skill("열파참", 3f, 5, 25)},
                      new List<Item>(){ new Item("1", "낡은 검", ItemType.Weapon, "쉽게 볼 수 있는 낡은 검입니다.", 2f, 0, 0, 0, 1000),
                                        new Item("2", "수련자 갑옷", ItemType.Armor, "수련에 도움을 주는 갑옷입니다.", 0f, 5, 0, 0, 2500) },
                      new List<Item>(){ new Item("1", "낡은 검", ItemType.Weapon, "쉽게 볼 수 있는 낡은 검입니다.", 2f, 0, 0, 0, 1000)}, 3, 3)};
            }

            if (File.Exists(filePath + "MonsterData.Json"))
            {
                string readMonsterString = File.ReadAllText(filePath + "MonsterData.Json");
                monsters = JsonConvert.DeserializeObject<List<Monster>>(readMonsterString);
            }
            else 
            {
                monsters = new List<Monster>() { new Monster(name: "미니언", job: "monster", level: 2, atk: 5, def: 0,
                                                                           maxHp: 15, maxMp: 0, hp: 15, mp: 0, gold: 400, exp: 2,
                        new List<Skill>(){ skillList[3] },
                        new List<Item>() { this.GetItemFromCode("3") }
                        ),

                                        new Monster(name: "공허충", job: "monster", level: 3, atk: 9, def: 0,
                                                                           maxHp: 10, maxMp: 0, hp: 10, mp: 0, gold: 600, exp: 3,
                        new List<Skill>(){ skillList[4]},
                        new List<Item>() { this.GetItemFromCode("4") }
                        ),

                                        new Monster(name: "대포미니언", job: "monster", level: 5, atk: 8, def: 0,
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
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static WeGoMars.Player;

namespace WeGoMars
{
    internal class DungeonScene : Displayer
    {
        private int oldHp;
        private int oldExp;
        private int oldLevel;
        private bool isDone = false;
        private List<Monster> monsters = new List<Monster>();

        Random random = new Random();

        public void DisplayDungeonSelect()
        {
            oldHp = Managers.Player.Hp;
            oldLevel = Managers.Player.Level;
            oldExp = Managers.Player.Exp;
            isDone = false;
            SetTitle("던전 입구");
            Console.WriteLine();
            Console.WriteLine("1. 하급던전");
            Console.WriteLine("2. 중급던전");
            Console.WriteLine("3. 상급던전");
            Console.WriteLine();
            Console.WriteLine(MsgDefine.INPUT_ACTION);
            int input = CheckValidInput(1, 3);

            SetMonster(input);

            DisplayDungeon();
        }

        public void DisplayDungeon()           // 몬스터정보, 내정보 출력, 행동지정 
        {
            DungeonCommonDisplay();
            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 스킬");
            Console.WriteLine("3. 아이템 사용");
            Console.WriteLine();
            Console.WriteLine(MsgDefine.INPUT_ACTION);
            int input = CheckValidInput(1, 3);
            switch (input)
            {
                case 1:
                    DisplayPlayerAttack();
                    break;
                case 2:
                    DisplayPlayerSkill();
                    break;
                case 3:
                    DisplayPlayerItem();
                    break;
            }
        }

        public void DisplayPlayerAttack()
        {
            DungeonAimingDisplay();
            Console.WriteLine("0. 취소");
            Console.WriteLine();
            Console.Write("대상을 선택해주세요. \n>>");
            while (isDone == false)
            {
                int input = CheckValidInput(0, monsters.Count);
                {
                    if (input == 0)
                    {

                        DisplayDungeon();
                    }
                    else if (monsters[input - 1].IsDead() == false)
                    {
                        DisplayPlayerTurn(input - 1);
                    }
                    else
                    {
                        Console.Write("대상으로 지정할 수 없습니다. \n>>");
                    }
                }
            }
        }

        public void DisplayPlayerSkill()
        {
            DungeonCommonDisplay();
            int i = 0;
            foreach (Skill skill in Managers.Player.SkillList)
            {
                Console.WriteLine($"{++i}. {skill.Name} - MP {skill.MpCost}");
                if (skill.TargetCount == 1)
                {
                    Console.WriteLine($"\t공격력 X {skill.AttackBonus}로 하나의 적을 공격합니다.");
                }
                else
                {
                    Console.WriteLine($"\t공격력 X {skill.AttackBonus}로 최대 {skill.TargetCount}명의 적을 1회 랜덤으로 공격합니다.");
                }
            }
            Console.WriteLine();
            Console.WriteLine("0. 취소");
            Console.WriteLine();
            Console.WriteLine("사용할 스킬을 입력하세요\n>>");

            int skillInput = CheckValidInput(0, Managers.Player.SkillList.Count);
            if (skillInput == 0)
            {
                DisplayDungeon();
            }
            else if (Managers.Player.SkillList[skillInput - 1].MpCost > Managers.Player.Mp)
            {
                Console.WriteLine("마나가 부족합니다.");
            }
            else if (Managers.Player.SkillList[skillInput - 1].TargetCount == 1)
            {
                DisplayOneTargetSkill(skillInput);
            }
            else
            {
                DisplayRandomTargetSkill(skillInput);
            }
        }

        public void DisplayOneTargetSkill(int skillInput)
        {
            DungeonAimingDisplay();
            Console.WriteLine("0. 취소");
            Console.WriteLine();
            Console.Write("대상을 선택해주세요. \n>>");

            int targetInput = CheckValidInput(0, monsters.Count);

            if (targetInput == 0)
            {

                DisplayPlayerSkill();
            }
            else
            {
                if (monsters[targetInput - 1].IsDead())
                {
                    Console.Write("대상으로 지정할 수 없습니다. \n>>");
                }
                DisplayPlayerTurn(skillInput - 1, targetInput - 1);
            }
        }

        public void DisplayRandomTargetSkill(int skillInput)
        {
            DungeonCommonDisplay();
            Console.WriteLine("1. 스킬사용");
            Console.WriteLine();
            Console.WriteLine("0. 취소");
            Console.WriteLine();

            int input = CheckValidInput(0, 1);
            switch (input)
            {
                case 0:
                    DisplayPlayerSkill();
                    break;
                case 1:
                    DisplayPlayerTurn(skillInput - 1, -1);
                    break;
            }
        }

        public void DisplayPlayerItem()
        {
            DungeonCommonDisplay();
            Console.WriteLine("[내 아이템]");
            Console.WriteLine($"체력포션 : {Managers.Player.HealthPotionCnt}");
            Console.WriteLine($"마나포션 : {Managers.Player.ManaPotionCnt}");
            Console.WriteLine("1. 체력포션 사용");
            Console.WriteLine("2. 마나포션 사용");
            Console.WriteLine();
            Console.WriteLine("0. 취소");
            int input = CheckValidInput(0, 2);
            switch (input)
            {
                case 0:
                    DisplayDungeon();
                    break;
                case 1:
                    DisplayResultUseItem("체력");
                    break;
                case 2:
                    DisplayResultUseItem("마나");
                    break;
            }
        }

        public void DisplayPlayerTurn(int monsterNumber)      // 일반 공격시
        {
            SetTitle("Battle!!");
            Console.WriteLine();
            Console.WriteLine($"{Managers.Player.Name} 의 공격!");
            int beforeHp = monsters[monsterNumber].Hp;
            int criticalEvasion = random.Next(0, 100);
            float dmg = Managers.Player.Attack();
            string monsterInfo = $"Lv.{monsters[monsterNumber].Level} {monsters[monsterNumber].Name}";
            if (criticalEvasion < 10)
            {
                Console.WriteLine($"{monsterInfo}을(를) 공격했지만 아무일도 일어나지 않았습니다.");
                Console.WriteLine($"{monsterInfo}");
            }
            else if (criticalEvasion < 25)
            {
                int criDmg = (int)Math.Round(1.6 * dmg);
                Console.WriteLine("회심의 일격!!");
                monsters[monsterNumber].TakeDamage(criDmg);
                Console.WriteLine($"{monsterInfo}을(를) 맞췄습니다. [데미지 : {criDmg}]");
                Console.WriteLine();
                Console.WriteLine($"{monsterInfo}");
            }
            else
            {
                dmg = Managers.Player.Attack();
                monsters[monsterNumber].TakeDamage((int)Math.Round(dmg));
                Console.WriteLine($"{monsterInfo}을(를) 맞췄습니다. [데미지 : {(int)Math.Round(dmg)}]");
                Console.WriteLine();
                Console.WriteLine($"{monsterInfo}");
            }

            if (monsters[monsterNumber].IsDead())
            {
                Console.WriteLine($"HP {beforeHp} -> Dead");
            }
            else
            {
                Console.WriteLine($"HP {beforeHp} -> {monsters[monsterNumber].Hp}");
            }
            CheckPlayerWin();
        }

        public void DisplayPlayerTurn(int skillNumber, int monsterNumber)           //스킬 공격시
        {
            SetTitle("Battle!!");
            Console.WriteLine();
            Console.WriteLine($"{Managers.Player.Name} 의 {Managers.Player.SkillList[skillNumber].Name}!");

            List<int> aliveMonsters = new List<int>();
            for (int i = 0; i < monsters.Count; i++)
            {
                if (monsters[i].IsDead() == false)
                {
                    aliveMonsters.Add(i);
                }
            }

            int[] monsterHp = new int[monsters.Count()];
            for (int i = 0; i < monsters.Count(); i++)
            {
                monsterHp[i] = monsters[i].Hp;
            }

            float dmg = Managers.Player.SkillList[skillNumber].AttackBonus * Managers.Player.Attack();
            if (monsterNumber == -1)
            {
                // 전원 공격
                if (aliveMonsters.Count <= Managers.Player.SkillList[skillNumber].TargetCount)        // 다수 공격인데 몬스터 수가 스킬의 타겟카운트랑 같거나 적은 경우
                {
                    for (int i = 0; i < monsters.Count; i++)
                    {
                        if (monsters[i].IsDead() == false)
                        {
                            monsters[i].TakeDamage((int)Math.Round(dmg));
                            Console.WriteLine($"Lv.{monsters[i].Level} {monsters[i].Name} 을(를) 맞췄습니다. [데미지 : {(int)Math.Round(dmg)}]");
                            Console.WriteLine($"Lv.{monsters[i].Level} {monsters[i].Name}");
                            Console.WriteLine($"HP {monsterHp[i]} -> {monsters[i].Hp}");
                        }
                    }
                }
                // 랜덤 다수 공격
                else                                                                    // 다수 공격에서 몬스터 수가 더 많은 경우
                {
                    int min = 0;                                                                //타겟카운트만큼 랜덤하게 몬스터를 고르는 과정
                    int max = aliveMonsters.Count;
                    int[] allTargets = Enumerable.Range(min, max).ToArray();
                    int[] realTargets = new int[Managers.Player.SkillList[skillNumber].TargetCount];
                    for (int i = 0; i < Managers.Player.SkillList[skillNumber].TargetCount; i++)
                    {
                        int randomIndex = random.Next(allTargets.Length);
                        realTargets[i] = allTargets[randomIndex];

                        // 추출된 숫자를 배열에서 제거하여 중복을 방지합니다.
                        allTargets = allTargets.Where((val, idx) => idx != randomIndex).ToArray();
                    }
                    for (int i = 0; i < realTargets.Length; i++)
                    {
                        monsters[aliveMonsters[realTargets[i]]].TakeDamage((int)Math.Round(dmg));
                        Console.WriteLine($"Lv.{monsters[aliveMonsters[realTargets[i]]].Level} {monsters[aliveMonsters[realTargets[i]]].Name} 을(를) 맞췄습니다. [데미지 : {(int)Math.Round(dmg)}]");
                        Console.WriteLine($"Lv.{monsters[aliveMonsters[realTargets[i]]].Level} {monsters[aliveMonsters[realTargets[i]]].Name}");
                        Console.WriteLine($"HP {monsterHp[aliveMonsters[realTargets[i]]]} -> {monsters[aliveMonsters[realTargets[i]]].Hp}");
                    }
                }

            }

            else                        // 단일 공격일 경우
            {
                monsters[monsterNumber].TakeDamage((int)Math.Round(dmg));
                Console.WriteLine($"Lv.{monsters[monsterNumber].Level} {monsters[monsterNumber].Name} 을(를) 맞췄습니다. [데미지 : {(int)Math.Round(dmg)}]");
                Console.WriteLine($"Lv.{monsters[monsterNumber].Level} {monsters[monsterNumber].Name}");

                if (monsters[monsterNumber].IsDead())
                {
                    Console.WriteLine($"HP {monsterHp[monsterNumber]} -> Dead");
                }
                else
                {
                    Console.WriteLine($"HP {monsterHp[monsterNumber]} -> {monsters[monsterNumber].Hp}");
                }
            }
            Managers.Player.Mp -= Managers.Player.SkillList[skillNumber].MpCost;

            CheckPlayerWin();
        }

        public void DisplayMonsterTurn()
        {
            SetTitle("Battle!!");
            Console.WriteLine();
            int beforeHp = Managers.Player.Hp;
            for (int i = 0; i < monsters.Count; i++)
            {
                if (monsters[i].IsDead() == false)
                {
                    float dmg = monsters[i].Attack();
                    Managers.Player.TakeDamage((int)Math.Round(dmg));
                    Console.WriteLine($"Lv.{monsters[i].Level} {monsters[i].Name} 의 공격!");
                    Console.WriteLine($"{Managers.Player.Name} 을(를) 맞췄습니다. [데미지 : {(int)Math.Round(dmg)}]");
                }
            }
            CheckMonsterWin();
        }

        public void DisplayResultWin()
        {
            SetTitle("Battle!! - Result");
            int monsterExp = 0;
            for (int i = 0; i < monsters.Count; i++)
            {
                monsterExp += monsters[i].Level;
            }
            Managers.Player.GainExp(monsterExp);
            Console.WriteLine();
            Console.WriteLine("Victory");
            Console.WriteLine();
            Console.WriteLine($"던전에서 몬스터를 {monsters.Count}마리를 잡았습니다.");
            Console.WriteLine();
            Console.WriteLine("[캐릭터 정보]");
            Console.WriteLine($"{Managers.Player.Name} ({Managers.Player.Job})");
            if (oldLevel != Managers.Player.Level)
            {
                Console.WriteLine($"Lv.{oldLevel} -> Lv.{Managers.Player.Level}");
            }
            else
            {
                Console.WriteLine($"Lv.{Managers.Player.Level}");
            }
            Console.WriteLine($"HP {oldHp} -> {Managers.Player.Hp}");
            Console.WriteLine($"Exp {oldExp} -> {Managers.Player.Exp}");
            Console.WriteLine();
            Console.WriteLine("[획득 아이템]");
            int resultGold = 0;
            for (int i = 0; i < monsters.Count; i++)
            {
                resultGold += monsters[i].Level * 100;
            }
            Console.WriteLine($"{resultGold} Gold");
            for (int i = 0; i < monsters.Count; i++)
            {
                Managers.Player.Inventory.Add(monsters[i].DropItem());
                Console.WriteLine(Managers.Player.Inventory[Managers.Player.Inventory.Count - 1].Name);
            }
            Managers.Player.Gold += resultGold;
            isDone = true;
            ToMainScene();
        }

        public void DisplayResultLose()
        {
            SetTitle("Battle!! - Result");
            Console.WriteLine();
            Console.WriteLine("You Lose");
            Console.WriteLine();
            Console.WriteLine($"Lv.{Managers.Player.Level} {Managers.Player.Name}");
            Console.WriteLine($"HP {oldHp} -> 0");
            Managers.Player.Hp = 1;
            isDone = true;
            ToMainScene();
        }

        public void DungeonCommonDisplay()
        {
            SetTitle("Battle!!");
            Console.WriteLine();
            for (int i = 0; i < monsters.Count; i++)
            {
                if (monsters[i].IsDead())
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine($"{MsgDefine.LEVEL}{monsters[i].Level} {monsters[i].Name}  Dead");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"{MsgDefine.LEVEL}{monsters[i].Level} {monsters[i].Name}  HP {monsters[i].Hp}");
                }
            }
            Console.WriteLine();
            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{Managers.Player.Level} {Managers.Player.Name} ({Managers.Player.Job})");
            Console.WriteLine($"HP {Managers.Player.Hp}/{Managers.Player.MaxHp}");
            Console.WriteLine($"MP {Managers.Player.Mp} / {Managers.Player.MaxMp}");
            Console.WriteLine();
        }

        public void DungeonAimingDisplay()
        {
            SetTitle("Battle!!");
            Console.WriteLine();
            for (int i = 0; i < monsters.Count; i++)
            {
                if (monsters[i].IsDead())
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine($"{i + 1}. Lv.{monsters[i].Level} {monsters[i].Name}  Dead");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"{i + 1}. Lv.{monsters[i].Level} {monsters[i].Name}  HP {monsters[i].Hp}");
                }
            }
            Console.WriteLine();
            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{Managers.Player.Level}   {Managers.Player.Name}");
            Console.WriteLine($"HP {Managers.Player.Hp} / {Managers.Player.MaxHp}");
            Console.WriteLine($"MP {Managers.Player.Mp} / {Managers.Player.MaxMp}");
            Console.WriteLine();
        }

        void DisplayResultUseItem(string itemType)
        {
            UseItemType result;
            int amount;
            if (itemType == "체력")
            {
                result = Managers.Player.UseHealthPotion();
                amount = MsgDefine.HP_POTION_AMOUNT;
            }
            else
            {
                result = Managers.Player.UseManaPotion();
                amount = MsgDefine.MP_POTION_AMOUNT;
            }

            switch (result)
            {
                case UseItemType.FullAlready:
                    Console.WriteLine($"이미 {itemType}이(가) 가득 차있습니다.");
                    Thread.Sleep(1000);
                    DisplayPlayerItem();
                    break;
                case UseItemType.Success:
                    Console.WriteLine($"{itemType}이(가) {amount} 회복되었습니다.");
                    ToMonsterTurn();
                    break;
                case UseItemType.LackItem:
                    Console.WriteLine("포션이 없습니다.");
                    Thread.Sleep(1000);
                    DisplayPlayerItem();
                    break;
            }
        }

        public void ToMonsterTurn()
        {
            Console.WriteLine();
            Console.WriteLine("0. 다음");
            Console.WriteLine();
            Console.Write(">>");
            int input = CheckValidInput(0, 0);
            switch (input)
            {
                case 0: DisplayMonsterTurn(); break;
            }
        }

        public void ToMainScene()
        {
            Console.WriteLine();
            Console.WriteLine("0. 다음");
            Console.WriteLine();
            Console.Write(">>");
            int input = CheckValidInput(0, 0);
            switch (input)
            {
                case 0: Managers.MainScene.DisplayMain(); break;
            }
        }

        public void ReturnToDisplayDungeon()
        {
            Console.WriteLine();
            Console.WriteLine("0. 다음");
            Console.WriteLine();
            Console.Write(">>");
            int input = CheckValidInput(0, 0);
            switch (input)
            {
                case 0: DisplayDungeon(); break;
            }
        }

        public void CheckPlayerWin()
        {
            int count = 0;
            for (int i = 0; i < monsters.Count; i++)
            {
                if (monsters[i].IsDead())
                {
                    count++;
                }
            }
            if (count == monsters.Count)
            {
                Console.WriteLine();
                Console.WriteLine("0. 다음");
                Console.WriteLine();
                Console.Write(">>");
                int input = CheckValidInput(0, 0);
                switch (input)
                {
                    case 0: DisplayResultWin(); break;
                }
            }
            else { ToMonsterTurn(); }
        }

        public void CheckMonsterWin()
        {
            if (Managers.Player.IsDead())
            {
                Console.WriteLine();
                Console.WriteLine("0. 다음");
                Console.WriteLine();
                Console.Write(">>");
                int input = CheckValidInput(0, 0);
                switch (input)
                {
                    case 0: DisplayResultLose(); break;
                }
            }
            else { ReturnToDisplayDungeon(); }
        }
        public void SetMonster(int stage)           // 무작위로 몬스터 생성
        {
            monsters.Clear();
            int monsterCnt = random.Next(3, 5);
            int start = 0;
            int end = 0;
            switch (stage)
            {
                case 1:
                    start = 0;
                    end = 4;
                    break;
                case 2:
                    start = 4;
                    end = 7;
                    break;
                case 3:
                    start = 7;
                    end = 10;
                    break;
            }
            for (int i = 0; i < monsterCnt; i++)
            {
                int monsterIdx = random.Next(start, end);
                monsters.Add(Managers.GameData.GetMonster(monsterIdx));
            }
        }
    }
}
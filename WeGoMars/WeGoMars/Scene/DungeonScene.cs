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

namespace WeGoMars
{
    internal class DungeonScene : Displayer
    {
        private int oldHp;
        private int oldExp;
        private int oldLevel;
        private bool isDone = false;
        private bool isDoneSkill = false;

        Random random = new Random();
        public void DisplayDungeonSelect()
        {
            oldHp = Managers.Player.Hp;
            oldLevel = Managers.Player.Level;
            oldExp = Managers.Player.Exp;
            isDone = false;
            isDoneSkill = false;
            SetTitle("던전 입구");
            Console.WriteLine();
            Console.WriteLine("1. 하급던전");
            Console.WriteLine("2. 중급던전");
            Console.WriteLine("3. 상급던전");
            Console.WriteLine();
            Console.WriteLine(MsgDefine.INPUT_ACTION);
            int input = CheckValidInput(1, 3);

            switch (input)
            {
                case 1:
                    DisplayDungeon(Managers.DungeonScene.SetMonster(input));
                    break;
                case 2:
                    DisplayDungeon(Managers.DungeonScene.SetMonster(input));
                    break;
                case 3:
                    DisplayDungeon(Managers.DungeonScene.SetMonster(input));
                    break;
            }
        }
        public void DisplayDungeon(List<Monster> monster)           // 몬스터정보, 내정보 출력, 행동지정 
        {
            DungeonCommonDisplay(monster);
            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 스킬");
            Console.WriteLine("3. 아이템 사용");
            Console.WriteLine();
            Console.WriteLine(MsgDefine.INPUT_ACTION);
            int input = CheckValidInput(1, 3);
            isDoneSkill = false;
            switch (input)
            {
                case 1:
                    DisplayPlayerAttack(monster);
                    break;
                case 2:
                    DisplayPlayerSkill(monster);
                    break;
                case 3:
                    DisplayPlayerItem(monster);
                    break;
            }
        }
        public List<Monster> SetMonster(int stage)           // 무작위로 몬스터 생성
        {

            List<Monster> monster = new List<Monster>();
            int monsterCnt = random.Next(3, 5);
            if (stage == 1)
            {
                for (int i = 0; i < monsterCnt; i++)
                {
                    int monsterIdx = random.Next(0, 4);
                    monster.Add(Managers.GameData.GetMonster(monsterIdx));
                }
            }
            else if (stage == 2)
            {
                for (int i = 0; i < monsterCnt; i++)
                {
                    int monsterIdx = random.Next(4, 7);
                    monster.Add(Managers.GameData.GetMonster(monsterIdx));
                }
            }
            else
            {
                for (int i = 0; i < monsterCnt; i++)
                {
                    int monsterIdx = random.Next(7, 10);
                    monster.Add(Managers.GameData.GetMonster(monsterIdx));
                }
            }

            return monster;
        }

        public void DisplayPlayerTurn(List<Monster> monster, int monsterNumber)      // 일반 공격시
        {
            SetTitle("Battle!!");
            Console.WriteLine();
            Console.WriteLine($"{Managers.Player.Name} 의 공격!");
            int beforeHp = monster[monsterNumber].Hp;
            int criticalEvasion = random.Next(0, 100);
            float dmg = Managers.Player.Attack();
            if (criticalEvasion < 10)
            {
                Console.WriteLine($"Lv.{monster[monsterNumber].Level} {monster[monsterNumber].Name} 을(를) 공격했지만 아무일도 일어나지 않았습니다.");
                Console.WriteLine($"Lv.{monster[monsterNumber].Level} {monster[monsterNumber].Name}");
            }
            else if (criticalEvasion < 25)
            {
                int criDmg = (int)Math.Round(1.6 * dmg);
                Console.WriteLine("회심의 일격!!");
                monster[monsterNumber].TakeDamage(criDmg);
                Console.WriteLine($"Lv.{monster[monsterNumber].Level} {monster[monsterNumber].Name} 을(를) 맞췄습니다. [데미지 : {criDmg}]");
                Console.WriteLine();
                Console.WriteLine($"Lv.{monster[monsterNumber].Level} {monster[monsterNumber].Name}");
            }
            else
            {
                dmg = Managers.Player.Attack();
                monster[monsterNumber].TakeDamage((int)Math.Round(dmg));
                Console.WriteLine($"Lv.{monster[monsterNumber].Level} {monster[monsterNumber].Name} 을(를) 맞췄습니다. [데미지 : {(int)Math.Round(dmg)}]");
                Console.WriteLine();
                Console.WriteLine($"Lv.{monster[monsterNumber].Level} {monster[monsterNumber].Name}");
            }

            if (monster[monsterNumber].IsDead())
            {
                Console.WriteLine($"HP {beforeHp} -> Dead");
            }
            else
            {
                Console.WriteLine($"HP {beforeHp} -> {monster[monsterNumber].Hp}");
            }
            CheckPlayerWin(monster);
        }


        public void DisplayPlayerTurn(List<Monster> monster, int skillNumber, int monsterNumber)           //스킬 공격시
        {
            SetTitle("Battle!!");
            Console.WriteLine();
            Console.WriteLine($"{Managers.Player.Name} 의 {Managers.Player.SkillList[skillNumber].Name}!");
            List<int> aliveMonsterIdx = new List<int>();
            for (int i = 0; i < monster.Count; i++)
            {
                if (monster[i].IsDead() == false)
                {
                    aliveMonsterIdx.Add(i);
                }
            }
            int[] monsterHp = new int[monster.Count()];
            for (int i = 0; i < monster.Count(); i++)
            {
                monsterHp[i] = monster[i].Hp;
            }
            float dmg = Managers.Player.SkillList[skillNumber].AttackBonus * Managers.Player.Attack();
            if (monsterNumber == -1)
            {
                if (aliveMonsterIdx.Count <= Managers.Player.SkillList[skillNumber].TargetCount)        // 다수 공격인데 몬스터 수가 스킬의 타겟카운트랑 같거나 적은 경우
                {
                    for (int i = 0; i < monster.Count; i++)
                    {
                        if (monster[i].IsDead() == false)
                        {
                            monster[i].TakeDamage((int)Math.Round(dmg));
                            Console.WriteLine($"Lv.{monster[i].Level} {monster[i].Name} 을(를) 맞췄습니다. [데미지 : {(int)Math.Round(dmg)}]");
                            Console.WriteLine($"Lv.{monster[i].Level} {monster[i].Name}");
                            Console.WriteLine($"HP {monsterHp[i]} -> {monster[i].Hp}");
                        }
                    }

                }
                else                                                                    // 다수 공격에서 몬스터 수가 더 많은 경우
                {
                    int min = 0;                                                                //타겟카운트만큼 랜덤하게 몬스터를 고르는 과정
                    int max = aliveMonsterIdx.Count;
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
                        monster[aliveMonsterIdx[realTargets[i]]].TakeDamage((int)Math.Round(dmg));
                        Console.WriteLine($"Lv.{monster[aliveMonsterIdx[realTargets[i]]].Level} {monster[aliveMonsterIdx[realTargets[i]]].Name} 을(를) 맞췄습니다. [데미지 : {(int)Math.Round(dmg)}]");
                        Console.WriteLine($"Lv.{monster[aliveMonsterIdx[realTargets[i]]].Level} {monster[aliveMonsterIdx[realTargets[i]]].Name}");
                        Console.WriteLine($"HP {monsterHp[aliveMonsterIdx[realTargets[i]]]} -> {monster[aliveMonsterIdx[realTargets[i]]].Hp}");
                    }
                }

            }

            else                        // 단일 공격일 경우
            {
                monster[monsterNumber].TakeDamage((int)Math.Round(dmg));
                Console.WriteLine($"Lv.{monster[monsterNumber].Level} {monster[monsterNumber].Name} 을(를) 맞췄습니다. [데미지 : {(int)Math.Round(dmg)}]");
                Console.WriteLine($"Lv.{monster[monsterNumber].Level} {monster[monsterNumber].Name}");

                if (monster[monsterNumber].IsDead())
                {
                    Console.WriteLine($"HP {monsterHp[monsterNumber]} -> Dead");
                }
                else
                {
                    Console.WriteLine($"HP {monsterHp[monsterNumber]} -> {monster[monsterNumber].Hp}");
                }
                isDoneSkill = true;
            }
            Managers.Player.Mp -= Managers.Player.SkillList[skillNumber].MpCost;



            CheckPlayerWin(monster);


        }


        public void DisplayPlayerAttack(List<Monster> monster)
        {
            DungeonAimingDisplay(monster);
            Console.WriteLine("0. 취소");
            Console.WriteLine();
            Console.Write("대상을 선택해주세요. \n>>");
            while (isDone == false)
            {
                int input = CheckValidInput(0, monster.Count);
                {
                    switch (input)
                    {
                        case 0:
                            DisplayDungeon(monster);
                            break;
                        default:
                            if (monster[input - 1].IsDead() == false)
                            {
                                DisplayPlayerTurn(monster, input - 1);
                            }
                            else
                            {
                                Console.Write("대상으로 지정할 수 없습니다. \n>>");
                            }
                            break;
                    }
                }

            }

        }
        public void DisplayPlayerSkill(List<Monster> monsters)        // 스킬을 보여주고 대상을 지정하는 씬
        {
            DungeonCommonDisplay(monsters);
            int i = 0;
            foreach (Skill skill in Managers.Player.SkillList)
            {
                Console.WriteLine($"{i + 1}. {skill.Name} - MP {skill.MpCost}");
                i++;
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
                DisplayDungeon(monsters);
            }
            else if (Managers.Player.SkillList[skillInput - 1].MpCost > Managers.Player.Mp)
            {
                Console.WriteLine("마나가 부족합니다.");
            }
            else if (Managers.Player.SkillList[skillInput - 1].TargetCount == 1)
            {
                DisplayOneTargetSkill(monsters, skillInput);
            }
            else
            {
                DisplayRandomTargetSkill(monsters, skillInput);
            }
        }

        public void DisplayOneTargetSkill(List<Monster> monsters, int skillInput)
        {
            DungeonAimingDisplay(monsters);
            Console.WriteLine("0. 취소");
            Console.WriteLine();
            Console.Write("대상을 선택해주세요. \n>>");
            int targetInput = 0;

            targetInput = CheckValidInput(0, monsters.Count);
            
            if (targetInput == 0)
            {

                DisplayPlayerSkill(monsters);
            }
            else
            {
                if (monsters[targetInput - 1].IsDead())
                {
                    Console.Write("대상으로 지정할 수 없습니다. \n>>");
                }
                DisplayPlayerTurn(monsters, skillInput - 1, targetInput - 1);
            }
        }

        public void DisplayRandomTargetSkill(List<Monster> monsters, int skillInput)
        {
            DungeonCommonDisplay(monsters);
            Console.WriteLine("1. 스킬사용");
            Console.WriteLine();
            Console.WriteLine("0. 취소");
            Console.WriteLine();

            int input = CheckValidInput(0, 1);
            switch (input)
            {
                case 0:
                    DisplayPlayerSkill(monsters);
                    break;
                case 1:
                    DisplayPlayerTurn(monsters, skillInput - 1, -1);
                    break;
            }
        }

        public void DisplayPlayerItem(List<Monster> monster)
        {
            DungeonCommonDisplay(monster);
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
                    DisplayDungeon(monster);
                    break;
                case 1:
                    if (Managers.Player.HealthPotionCnt > 0)
                    {
                        if (Managers.Player.Hp == Managers.Player.MaxHp)
                        {
                            Console.WriteLine("이미 체력이 가득 차있습니다.");
                            Thread.Sleep(1000);
                            DisplayPlayerItem(monster);
                        }
                        else
                        {
                            Managers.Player.UseHealthPotion(MsgDefine.HP_POTION_AMOUNT);
                            Console.WriteLine($"체력이 {MsgDefine.HP_POTION_AMOUNT} 회복되었습니다.");
                            ToMonsterTurn(monster);
                        }
                    }
                    else
                    {
                        Console.WriteLine("포션이 없습니다.");
                        Thread.Sleep(1000);
                        DisplayPlayerItem(monster);
                    }
                    break;
                case 2:
                    if (Managers.Player.ManaPotionCnt > 0)
                    {
                        if (Managers.Player.Mp == Managers.Player.MaxMp)
                        {
                            Console.WriteLine("이미 마나가 가득 차있습니다.");
                            Thread.Sleep(1000);
                            DisplayPlayerItem(monster);
                        }
                        else
                        {
                            Managers.Player.UseManaPotion(MsgDefine.MP_POTION_AMOUNT);
                            Console.WriteLine($"마나가 {MsgDefine.MP_POTION_AMOUNT} 회복되었습니다.");
                            ToMonsterTurn(monster);
                        }

                    }
                    else
                    {
                        Console.WriteLine("포션이 없습니다.");
                        Thread.Sleep(1000);
                        DisplayPlayerItem(monster);
                    }

                    break;
            }
        }
        public void DisplayMonsterTurn(List<Monster> monster)
        {
            SetTitle("Battle!!");
            Console.WriteLine();
            int beforeHp = Managers.Player.Hp;
            for (int i = 0; i < monster.Count; i++)
            {
                if (monster[i].IsDead() == false)
                {
                    float dmg = monster[i].Attack();
                    Managers.Player.TakeDamage((int)Math.Round(dmg));
                    Console.WriteLine($"Lv.{monster[i].Level} {monster[i].Name} 의 공격!");
                    Console.WriteLine($"{Managers.Player.Name} 을(를) 맞췄습니다. [데미지 : {(int)Math.Round(dmg)}]");
                }
            }
            CheckMonsterWin(monster);

        }


        public void DisplayResultWin(List<Monster> monster)
        {
            SetTitle("Battle!! - Result");
            int monsterExp = 0;
            for (int i = 0; i < monster.Count; i++)
            {
                monsterExp += monster[i].Level;
            }
            Managers.Player.GainExp(monsterExp);
            Console.WriteLine();
            Console.WriteLine("Victory");
            Console.WriteLine();
            Console.WriteLine($"던전에서 몬스터를 {monster.Count}마리를 잡았습니다.");
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
            for (int i = 0; i < monster.Count; i++)
            {
                resultGold += monster[i].Level * 100;
            }
            Console.WriteLine($"{resultGold} Gold");
            for (int i = 0; i < monster.Count; i++)
            {
                Managers.Player.Inventory.Add(monster[i].DropItem());
                Console.WriteLine(Managers.Player.Inventory[Managers.Player.Inventory.Count - 1].Name);
            }
            Managers.Player.Gold += resultGold;
            isDone = true;
            ToMainScene();
        }

        public void DisplayResultLose(List<Monster> monster)
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


        public void DungeonCommonDisplay(List<Monster> monster)    // 행동지정
        {
            SetTitle("Battle!!");
            Console.WriteLine();
            for (int i = 0; i < monster.Count; i++)
            {
                if (monster[i].IsDead())
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine($"{MsgDefine.LEVEL}{monster[i].Level} {monster[i].Name}  Dead");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"{MsgDefine.LEVEL}{monster[i].Level} {monster[i].Name}  HP {monster[i].Hp}");
                }
            }
            Console.WriteLine();
            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{Managers.Player.Level} {Managers.Player.Name} ({Managers.Player.Job})");
            Console.WriteLine($"HP {Managers.Player.Hp}/{Managers.Player.MaxHp}");
            Console.WriteLine($"MP {Managers.Player.Mp} / {Managers.Player.MaxMp}");
            Console.WriteLine();
        }


        public void DungeonAimingDisplay(List<Monster> monsters)          // 대상지정
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


        public void ToMonsterTurn(List<Monster> monster)
        {
            Console.WriteLine();
            Console.WriteLine("0. 다음");
            Console.WriteLine();
            Console.Write(">>");
            int input = CheckValidInput(0, 0);
            switch (input)
            {
                case 0: DisplayMonsterTurn(monster); break;
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
        public void ReturnToDisplayDungeon(List<Monster> monster)
        {
            Console.WriteLine();
            Console.WriteLine("0. 다음");
            Console.WriteLine();
            Console.Write(">>");
            int input = CheckValidInput(0, 0);
            switch (input)
            {
                case 0: DisplayDungeon(monster); break;
            }
        }

        public void CheckPlayerWin(List<Monster> monster)
        {
            int count = 0;
            for (int i = 0; i < monster.Count; i++)
            {
                if (monster[i].IsDead())
                {
                    count++;
                }
            }
            if (count == monster.Count)
            {
                Console.WriteLine();
                Console.WriteLine("0. 다음");
                Console.WriteLine();
                Console.Write(">>");
                int input = CheckValidInput(0, 0);
                switch (input)
                {
                    case 0: DisplayResultWin(monster); break;
                }

            }
            else { ToMonsterTurn(monster); }
        }
        public void CheckMonsterWin(List<Monster> monster)
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
                    case 0: DisplayResultLose(monster); break;
                }

            }
            else { ReturnToDisplayDungeon(monster); }
        }

    }
}

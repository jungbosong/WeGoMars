using System;
using System.Collections.Generic;
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
        public void DisplayDungeon(List<Monster> monster)           // 몬스터정보, 내정보 출력, 행동지정 
        {

            DungeonCommonDisplay(monster);
            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 스킬");
            Console.WriteLine("3. 아이템 사용");
            Console.WriteLine();
            Console.WriteLine(MsgDefine.INPUT_ACTION);
            int input = CheckValidInput(1, 3);
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
        public List<Monster> SetMonster()
        {
            Random random = new Random();
            List<Monster> monster = new List<Monster>();
            int monsterCnt = random.Next(3, 5);
            for (int i = 0; i < monsterCnt; i++)
            {
                int monsterIdx = random.Next(0, 3);
                monster.Add(Managers.GameData.GetMonster(monsterIdx));
            }
            return monster;
        }

        public void DisplayPlayerTurn(List<Monster> monster, int monsterNumber)      // 일반 공격시
        {
            SetTitle("Battle!!");
            Console.WriteLine();
            int beforeHp = monster[monsterNumber].Hp;
            // 공격함수
            Console.WriteLine($"{Managers.Player.Name} 의 공격!");
            //Console.WriteLine($"Lv.{monster[monsterNumber].Level} {monster[monsterNumber].Name} 을(를) 맞췄습니다. [데미지 : DMg]");  공격함수에서 받은값 DMG에 넣기
            Console.WriteLine();
            Console.WriteLine($"Lv.{monster[monsterNumber].Level} {monster[monsterNumber].Name}");

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
            // 스킬사용 함수
            // Console.WriteLine($"Lv.{monster[monsterNumber].Level} {monster[monsterNumber].Name} 을(를) 맞췄습니다. [데미지 : DMG]"); 스킬함수에서 받은값 DMG에 넣기
            Console.WriteLine();
            int beforeHp = monster[monsterNumber].Hp;     //스킬 함수의 데미지만큼 더해야함
            Console.WriteLine($"Lv.{monster[monsterNumber].Level} {monster[monsterNumber].Name}");

            if (monster[monsterNumber].IsDead())
            {
                Console.WriteLine($"HP {beforeHp} -> Dead");
            }
            else
            {
                Console.WriteLine($"HP {beforeHp} -> {monster[monsterNumber].Hp}");    //전체나 다수 공격에 맞게 코드 변경 필요
            }
            CheckPlayerWin(monster);
            
        }


        public void DisplayPlayerAttack(List<Monster> monster)
        {
            DungeonAimingDisplay(monster);
            Console.WriteLine("0. 취소");
            Console.WriteLine();
            Console.Write("대상을 선택해주세요. \n>>");
            int input = CheckValidInput(0, monster.Count);
            switch (input)
            {
                case 0:
                    DisplayDungeon(monster);
                    break;
                default:
                    DisplayPlayerTurn(monster, input - 1);
                    break;
            }
        }
        public void DisplayPlayerSkill(List<Monster> monster)        // 스킬을 보여주고 대상을 지정하는 씬
        {
            DungeonCommonDisplay(monster);
            for (int i = 0; i < Managers.Player.SkillList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Managers.Player.SkillList[i].Name} - MP {Managers.Player.SkillList[i].MpCost}");
                // Console.WriteLine($"{player.SkillList[i].TargetCount}"); 스킬 설명이 필요. 아직없음. TargetCount자리에 넣기
            }
            Console.WriteLine();
            Console.WriteLine("0. 취소");
            Console.WriteLine();
            Console.WriteLine("사용할 스킬을 입력하세요\n>>");
            int input = CheckValidInput(0, Managers.Player.SkillList.Count);
            switch (input)
            {
                case 0:
                    DisplayDungeon(monster);
                    break;
                default:
                    DungeonAimingDisplay(monster);
                    Console.WriteLine("0. 취소");
                    Console.WriteLine();
                    Console.Write("대상을 선택해주세요. \n>>");
                    int input1 = CheckValidInput(0, monster.Count);
                    switch (input1)
                    {
                        case 0:
                            DisplayPlayerSkill(monster);
                            break;
                        default:
                            DisplayPlayerTurn(monster, input - 1, input1 - 1);
                            break;
                    }
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
            int input = CheckValidInput(0,2);
            switch (input)
            {
                case 0:
                    DisplayDungeon(monster);
                    break;
                case 1:
                    Managers.Player.UseHealthPotion(50);        // 포션회복량을 일단은 50
                    Console.WriteLine("체력이 50 회복되었습니다.");
                    ToMonsterTurn(monster);
                    break;
                case 2:
                    Managers.Player.UseManaPotion(25);
                    Console.WriteLine("마나가 25 회복되었습니다.");
                    ToMonsterTurn(monster);
                    break;
            }
        }
        public void DisplayMonsterTurn(List<Monster> monster)
        {
            SetTitle("Battle!!");
            Console.WriteLine();
            int beforeHp = Managers.Player.Hp;
            for (int i = 0; i<monster.Count; i++)
            {
                if (monster[i].IsDead() == false)
                {
                    //몬스터 공격함수
                    Console.WriteLine($"Lv.{monster[i].Level} {monster[i].Name} 의 공격!");
                    //Console.WriteLine($"{player.Name} 을(를) 맞췄습니다. [데미지 :DMG]");  공격함수에서 받아올것  
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
            int finalExp = monsterExp + Managers.Player.Exp;
            Console.WriteLine();
            Console.WriteLine("Victory");
            Console.WriteLine();
            Console.WriteLine($"던전에서 몬스터를 {monster.Count}마리를 잡았습니다.");
            Console.WriteLine();
            Console.WriteLine("[캐릭터 정보]");
            Console.WriteLine($"Lv.{Managers.Player.Level} {Managers.Player.Name}");              // 레벨업 구현 어떻게 할지
            Console.WriteLine($"HP->{Managers.Player.Hp}");           // 던전 입장시의 hp를 어떻게 할지 생각
            Console.WriteLine($"{Managers.Player.Exp} -> {finalExp}");
            Managers.Player.Exp = finalExp;
            Console.WriteLine();
            Console.WriteLine("[획득 아이템]");
            Console.WriteLine("500 Gold");
            for (int i = 0; i < monster.Count; i++)
            {
                Managers.Player.Inventory.Add(monster[i].DropItem());
                Console.WriteLine(Managers.Player.Inventory[-1]);
            }
            Managers.Player.Gold += 500;
            ToMainScene();
        }

        public void DisplayResultLose(List<Monster> monster)
        {
            SetTitle("Battle!! - Result");
            Console.WriteLine();
            Console.WriteLine("You Lose");
            Console.WriteLine();
            Console.WriteLine($"Lv.{Managers.Player.Level} {Managers.Player.Name}");
            Console.WriteLine("HP -> 0");// 던전 입장시의 hp를 어떻게 할지 생각
            Managers.Player.Hp = 1;
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
            Console.WriteLine();
        }


        public void DungeonAimingDisplay(List<Monster> monster)          // 대상지정
        {
            SetTitle("Battle!!");
            Console.WriteLine();
            for (int i = 0; i < monster.Count; i++)
            {
                if (monster[i].IsDead())
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine($"{i + 1}. Lv.{monster[i].Level} {monster[i].Name}  Dead");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"{i + 1}. Lv.{monster[i].Level} {monster[i].Name}  HP {monster[i].Hp}");
                }
            }
            Console.WriteLine();
            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{Managers.Player.Level}   {Managers.Player.Name}");
            Console.WriteLine($"HP {Managers.Player.Hp} / {Managers.Player.MaxHp}");
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
                DisplayResultWin(monster);
            }
            else { ToMonsterTurn(monster); }
        }
        public void CheckMonsterWin(List<Monster> monster)
        {
            if (Managers.Player.IsDead()) { DisplayResultLose(monster); }
            else { ReturnToDisplayDungeon(monster); }
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WeGoMars
{
    internal class DungeonScene : Displayer
    {
        public void DisplayDungeon(Player player, Monster[] monster)            
        {
            // 죽음여부 판단 필요
            DungeonCommonDisplay(player, monster);
            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 스킬");
            Console.WriteLine("3. 아이템 사용");
            Console.WriteLine();
            Console.WriteLine(MsgDefine.INPUT_ACTION);
            int input = CheckValidInput(1, 3);
            switch (input)
            {
                case 1:
                    DisplayPlayerAttack(player, monster);
                    break;
                case 2:
                    DisplayPlayerSkill(player, monster);
                    break;
                case 3:
                    DisplayPlayerItem(player, monster);
                    break;
            }
        }


        public void DisplayPlayerTurn(Player player, Monster[] monster, int monsterNumber)      // 일반 공격시
        {
            SetTitle("Battle!!");
            Console.WriteLine();
            int beforeHp = monster[monsterNumber].Hp
            // 공격함수
            Console.WriteLine($"{player.Name} 의 공격!");
            //Console.WriteLine($"Lv.{monster[monsterNumber].Level} {monster[monsterNumber].Name} 을(를) 맞췄습니다. [데미지 : DMg]");  공격함수에서 받은값 DMG에 넣기
            Console.WriteLine();
            Console.WriteLine($"Lv.{monster[monsterNumber].Level} {monster[monsterNumber].Name}");

            if (monster[monsterNumber].IsDead())
            {
                Console.WriteLine($"HP {beforeHp} -> Dead");
            }
            else 
            {
                Console.WriteLine($"HP {beforeHp} -> {monster[monsterNumber].Hp});
            }
            ToMonsterTurn(player, monster);
        }


        public void DisplayPlayerTurn(Player player, Monster[] monster, Skill skill, int monsterNumber)           //스킬 공격시
        {
            SetTitle("Battle!!");
            Console.WriteLine();
            Console.WriteLine($"{player.Name} 의 {skill.Name}!");
            // 스킬사용 함수
            // Console.WriteLine($"Lv.{monster[monsterNumber].Level} {monster[monsterNumber].Name} 을(를) 맞췄습니다. [데미지 : DMG]"); 스킬함수에서 받은값 DMG에 넣기
            Console.WriteLine();
            //  int beforeHp = monster[monsterNumber].Hp     스킬 함수의 데미지만큼 더해야함
            Console.WriteLine($"Lv.{monster[monsterNumber].Level} {monster[monsterNumber].Name}");

            if (monster[monsterNumber].IsDead())
            {
                Console.WriteLine($"HP {beforeHp} -> Dead");
            }
            else
            {
                Console.WriteLine($"HP {beforeHp} -> {monster[monsterNumber].Hp}");    //전체나 다수 공격에 맞게 코드 변경 필요
            }
            ToMonsterTurn(player, monster);
        }


        public void DisplayPlayerAttack(Player player, Monster[] monster)
        {
            DungeonAimingDisplay(player, monster);
            Console.WriteLine("0. 취소");
            Console.WriteLine();
            Console.Write("대상을 선택해주세요. \n>>");
            int input = CheckValidInput(0,monster.Length);
            switch (input)
            {
                case 0:
                    DisplayDungeon(player, monster);
                    break;
                default: 
                    DisplayPlayerTurn(player, monster, input-1);
                    break;
            }
        }
        public void DisplayPlayerSkill(Player player, Monster[] monster)        // 스킬을 보여주고 대상을 지정하는 씬
        {
            DungeonCommonDisplay();
            for (int i = 0; i < player.SkillList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {player.SkillList[i].Name} - MP {player.SkillList[i].MpCost}");
                // Console.WriteLine($"{player.SkillList[i].TargetCount}"); 스킬 설명이 필요. 아직없음. TargetCount자리에 넣기
            }
            Console.WriteLine();
            Console.WriteLine("0. 취소");
            Console.WriteLine();
            Console.WriteLine("사용할 스킬을 입력하세요\n>>");
            int input = CheckValidInput(0, player.SkillList.Count);
            switch (input)
            {
                case 0:
                    DisplayDungeon(player, monster);
                    break;
                default:
                    DungeonAimingDisplay(player, monster);
                    Console.WriteLine("0. 취소");
                    Console.WriteLine();
                    Console.Write("대상을 선택해주세요. \n>>");
                    int input1 = CheckValidInput(0, monster.Length);
                    switch (input1)
                    {
                        case 0:
                            DisplayPlayerSkill(player, monster);
                            break;
                        default:
                            DisplayPlayerTurn(player, monster, player.SkillList[input-1], input1 - 1);
                            break;
                    }

            }

        }
        public void DisplayPlayerItem(Player player, Monster[] monster)
        {
            DungeonCommonDisplay();
            Console.WriteLine("[내 아이템]");
            // Console.WriteLine()            체력포션, 마나표션과 남은 갯수 표시, 사용하는것까지 작성 필요
        }
        public void DisplayMonsterTurn(Player player, Monster[] monster)
        {
            SetTitle("Battle!!");
            Console.WriteLine();
            int beforeHp = player.Hp;
            for(int i=0; 1 < monster.Length; i++)
            {
                if (monster[i].IsDead() == false)
                {
                    //몬스터 공격함수
                    Console.WriteLine($"Lv.{monster[i].Level} {monster[i].Name} 의 공격!");
                    //Console.WriteLine($"{player.Name} 을(를) 맞췄습니다. [데미지 :DMG]");  공격함수에서 받아올것  
                }
            }
        }
        public void DisplayResult(Player player, Monster[] monster)
        {

        }
        public void DungeonCommonDisplay(Player player, Monster[] monster)    // 행동지정
        {
            SetTitle("Battle!!");
            Console.WriteLine();
            for (int i = 0; i < monster.Length; i++)
            {
                if (monster[i].IsDead)
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
            Console.WriteLine($"Lv.{player.Level} {player.Name} ({player.Job})");
            // Console.WriteLine($"HP {player.Hp}/100");                            100을 플레이어 최대체력으로 변경 필요
            Console.WriteLine();
        }


        public void DungeonAimingDisplay(Player player, Monster[] monster)          // 대상지정
        {
            SetTitle("Battle!!");
            Console.WriteLine();
            for (int i = 0; i < monster.Length; i++)
            {
                if (monster[i].IsDead)
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
            Console.WriteLine($"Lv.{player.Level} {player.Name} ({player.Job})");
            // Console.WriteLine($"HP {player.Hp}/100");                            100을 플레이어 최대체력으로 변경 필요
            Console.WriteLine();
        }


        public void ToMonsterTurn(Player player, Monster[] monster)
        {
            Console.WriteLine();
            Console.WriteLine("0. 다음");
            Console.WriteLine();
            Console.Write(">>");
            int input = CheckValidInput(0, 0);
            switch (input)
            {
                case 0: DisplayMonsterTurn(player, monster); break;
            }
        }
    }   
}

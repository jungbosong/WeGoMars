namespace WeGoMars
{
    internal class RecoveryScene : Displayer
    {
		public void DisplayRecovery()
		{
            int recoveryCost = MsgDefine.FULLRECOVERY_COST * Managers.Player.Level;
            int healingHealth = MsgDefine.HP_POTION_AMOUNT;
            int healingMana = MsgDefine.MP_POTION_AMOUNT;
            SetTitle($"{MsgDefine.RECOVERY}\n");
            Console.Write($"{MsgDefine.RECOVERY_HEALTHPOTION} 회복량은 ");
            Managers.FontColorChanger.Write(ConsoleColor.Magenta, $"{healingHealth}");
            Console.Write($" 입니다. (남은 {MsgDefine.HP} 포션: ");
            if (Managers.Player.HealthPotionCnt <= 0)
                Managers.FontColorChanger.Write(ConsoleColor.Red, $"{Managers.Player.HealthPotionCnt}");
            else
                Managers.FontColorChanger.Write(ConsoleColor.Magenta, $"{Managers.Player.HealthPotionCnt}");
            Console.WriteLine(" )");
            Console.Write($"{MsgDefine.RECOVERY_MANAPOTION} 회복량은 ");
            Managers.FontColorChanger.Write(ConsoleColor.Magenta, $"{healingMana}");
            Console.Write($" 입니다. (남은 {MsgDefine.MP} 포션: ");
            if (Managers.Player.ManaPotionCnt <= 0)
                Managers.FontColorChanger.Write(ConsoleColor.Red, $"{Managers.Player.ManaPotionCnt}");
            else
                Managers.FontColorChanger.Write(ConsoleColor.Magenta, $"{Managers.Player.ManaPotionCnt}");
            Console.WriteLine(" )");
            Managers.FontColorChanger.Write(ConsoleColor.Yellow, $"{recoveryCost}");
            Console.Write($" {MsgDefine.GOLD} {MsgDefine.RECOVERY_FULLRECOVERY} (보유 골드 : ");
            if (Managers.Player.Gold < recoveryCost)
                Managers.FontColorChanger.Write(ConsoleColor.Red, $"{Managers.Player.Gold} {MsgDefine.GOLD}");
            else
                Managers.FontColorChanger.Write(ConsoleColor.Green, $"{Managers.Player.Gold} {MsgDefine.GOLD}");
            Console.WriteLine(" )");
            Console.WriteLine();


            SetPlayerHealth();

            SetPlayerMana();
            Console.WriteLine();

            SetAction($"1. {MsgDefine.USE_HEALTHPOTION}\n2. {MsgDefine.USE_MANAPOTION}\n3. {MsgDefine.USE_FULLRECOVERY}\n0. {MsgDefine.OUT}");
            int input = CheckValidInput(0, 3);
            switch (input)
            {
                case 0:
                    Managers.MainScene.DisplayMain();
                    break;
                case 1:
                    Managers.Player.UseHealthPotion();
                    Managers.RecoveryScene.DisplayRecovery();
                    break;
                case 2:
                    Managers.Player.UseManaPotion();
                    Managers.RecoveryScene.DisplayRecovery();
                    break;
                case 3:
                    Managers.Player.UseFullRecovery(recoveryCost);
                    Managers.RecoveryScene.DisplayRecovery();
                    break;
            }
        }

        public void SetPlayerHealth()
        {
            Console.Write(MsgDefine.HP + " : ");
            if (Managers.Player.Hp < Managers.Player.MaxHp)
                Managers.FontColorChanger.Write(ConsoleColor.Red, $"{Managers.Player.Hp}");
            else
                Managers.FontColorChanger.Write(ConsoleColor.Green, $"{Managers.Player.Hp}");
            Console.Write(" / ");
            Managers.FontColorChanger.WriteLine(ConsoleColor.Green, $"{Managers.Player.MaxHp}");
        }

        public void SetPlayerMana()
        {
            Console.Write(MsgDefine.MP + " : ");
            if (Managers.Player.Mp < Managers.Player.MaxMp)
                Managers.FontColorChanger.Write(ConsoleColor.Red, $"{Managers.Player.Mp}");
            else
                Managers.FontColorChanger.Write(ConsoleColor.Green, $"{Managers.Player.Mp}");
            Console.Write(" / ");
            Managers.FontColorChanger.WriteLine(ConsoleColor.Green, $"{Managers.Player.MaxMp}");
        }
    }
}
namespace WeGoMars
{
    internal class SaveCharacterScene : Displayer
    {
        public void DisplaySaveCharacter()
        {
            SetTitle(MsgDefine.CHARACTER_SAVE);
            Console.WriteLine($"\n{MsgDefine.SAVE_SCENE}\n");

            string action = "";
            int playersCount = Managers.GameData.GetPlayerListCount();
            List<string> playernames = Managers.GameData.GetPlayerNameList();
            for (int i = 1; i < playersCount; i++)
            {
                action += $"{i}. 캐릭터 이름 : {playernames[i]}\n";
            }
            action += "0. 새로 만들기";
            SetAction(action);
            int input = CheckValidInput(0, playersCount - 1);

            if (input == 0)
            {
                Managers.SelectCharacterScene.DisplaySetupCharacter();
            }
            else
            {
                Managers.Player = Managers.GameData.GetPlayer(input);
            }
        }
    }
}

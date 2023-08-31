namespace WeGoMars
{
    internal class SaveCharacterScene : Displayer
    {
        public void DisplaySaveCharacter()
        {
            SetTitle(MsgDefine.CHARACTER_SAVE);
            Console.WriteLine($"\n{MsgDefine.SAVE_SCENE}\n");

            SetAction($"1. {MsgDefine.CHARACTER_SAVE}\n2. {MsgDefine.CHARACTER_DELETE}\n0. {MsgDefine.OUT}");
            int input = CheckValidInput(0, 2);
            switch (input)
            {
                case 0:
                    Managers.MainScene.DisplayMain();
                    break;
                case 1:
                    Managers.SaveCharacterScene.DisplaySave();
                    break;
                case 2:
                    Managers.SaveCharacterScene.DisplayDelete();
                    break;
            }
        }

        public void DisplaySave()
        {
            SetTitle(MsgDefine.CHARACTER_SAVE);
            Console.WriteLine($"\n{MsgDefine.SAVE_SCENE}\n");

            List<(string, string, int)> playersInfo = Managers.GameData.GetPlayersSimpleInfoList();
            int playersCount = playersInfo.Count;
            DisplayPlayerListWithButton(playersInfo, $"[{playersCount}] {MsgDefine.SAVE_NEW}", $"0. {MsgDefine.OUT}");

            int input = CheckValidInput(0, playersCount);
            if (input == 0)
            {
                Managers.SaveCharacterScene.DisplaySaveCharacter();
            }
            else if (input == playersCount)
            {
                Managers.GameData.AddPlayer(Managers.Player);
                Managers.GameData.SaveAllData();
                Managers.SaveCharacterScene.DisplaySave();
            }
            else
            {
                Managers.GameData.InsertPlayer(input, Managers.Player);
                Managers.GameData.SaveAllData();
                Managers.SaveCharacterScene.DisplaySave();
            }
        }

        public void DisplayDelete()
        {
            SetTitle(MsgDefine.CHARACTER_DELETE);
            Console.WriteLine($"\n{MsgDefine.DELETE_SCENE}\n");

            List<(string, string, int)> playersInfo = Managers.GameData.GetPlayersSimpleInfoList();
            int playersCount = playersInfo.Count;
            DisplayPlayerListWithButton(playersInfo, "", $"0. {MsgDefine.OUT}");

            int input = CheckValidInput(0, playersCount - 1);
            if (input == 0)
            {
                Managers.SaveCharacterScene.DisplaySaveCharacter();
            }
            else
            {
                Managers.GameData.RemovePlayer(input);
                Managers.GameData.SaveAllData();
                Managers.SaveCharacterScene.DisplayDelete();
            }
        }
    }
}

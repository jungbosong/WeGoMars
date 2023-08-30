using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WeGoMars
{
    internal class Managers
    {
        public static Managers s_instance = null;
        public static Managers Instance { get { return s_instance; } }

        private static MainScene s_mainScene = new MainScene();
        private static StatusScene s_statusScene = new StatusScene();
        private static InventoryScene s_inventoryScene = new InventoryScene();
        private static StoreScene s_storeScene = new StoreScene();
        private static RecoveryScene s_recoveryScene = new RecoveryScene();
        private static FontColorChanger s_fontColorChanger = new FontColorChanger();
        private static GameData s_gameData = new GameData();

        public static MainScene MainScene { get { return s_mainScene; } }
        public static StatusScene StatusScene { get { return s_statusScene; } }
        public static InventoryScene InventoryScene { get { return s_inventoryScene; } }
        public static StoreScene StoreScene { get { return s_storeScene; } }    
        public static RecoveryScene RecoveryScene { get { return s_recoveryScene; } }
        public static FontColorChanger FontColorChanger { get { return s_fontColorChanger; } }
        public static GameData GameData { get { return s_gameData; } }
        public static Player Player = GameData.GetPlayer(0);
    }
}

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
        private static DungeonScene s_dungeonScene = new DungeonScene();
        private static StatusScene s_statusScene = new StatusScene();
        private static Player s_player = new Player("플레이어", "전사", 1, 10, 5, 100, 50, 100, 50, 1500, 0,
                      new List<Skill>(), new List<Item>(), new List<Item>(), 0, 0);

        public static MainScene MainScene { get { return s_mainScene; } }
        public static DungeonScene DungeonScene { get { return s_dungeonScene; } }
        public static StatusScene StatusScene { get { return s_statusScene; } }
        public static Player Player { get { return s_player; } }
    }
}

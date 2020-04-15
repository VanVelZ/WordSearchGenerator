using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZJV.WordSearch.BL
{
    public static class Settings
    {
        public static List<Word> Words { get; set; } = new List<Word>();
        public static bool AllowBackwards { get; set; } = false;
        public static bool AllowDiagonals { get; set; } = false;
        public static bool HideNoise { get; set; } = false;
        public static int BoardX { get; set; }
        public static int BoardY { get; set; }
        public static float MaxWords { get; set; } = BoardX * BoardY / 2;

        public static Random rand { get; set; } = new Random();

        
    }

}


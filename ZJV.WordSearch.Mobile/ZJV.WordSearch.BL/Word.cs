using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZJV.WordSearch.BL
{
    public class Word
    {
        public enum Direction { Horizontal, Vertical, DUp, DDown}

        public string OriginalWord { get; set; }
        public string ManipulatedWord { get; set; }
        public int StartingX { get; set; }
        public int StartingY { get; set; }
        public bool IsBackwards { get; set; } = false;
        public Direction Orientation { get; set; }

        public Word(string word)
        {
            OriginalWord = word;
            ManipulatedWord = OriginalWord;
            SetOrientation();
            SetCoordinates();
        }
        public void ReverseWord()
        {

            if (IsBackwards == true) IsBackwards = false;
            else IsBackwards = true;

            string newWord = string.Empty;
            for(int i = OriginalWord.Length - 1; i >=0; i--)
            {
                newWord += OriginalWord[i];
            }
            ManipulatedWord = newWord;
        }

        public void SetCoordinates()
        {
            switch (Orientation)
            {
                case Word.Direction.Vertical:
                    StartingX = Settings.rand.Next(0, Settings.BoardX - ToString().Length);
                    StartingY = Settings.rand.Next(0, Settings.BoardY);
                    return;
                case Word.Direction.Horizontal:
                    StartingX = Settings.rand.Next(0, Settings.BoardX);
                    StartingY = Settings.rand.Next(0, Settings.BoardY - ToString().Length);
                    return;
                case Word.Direction.DDown:
                    StartingX = Settings.rand.Next(0, Settings.BoardX - ToString().Length);
                    StartingY = Settings.rand.Next(0, Settings.BoardY - ToString().Length);
                    return;
                case Word.Direction.DUp:
                    StartingX = Settings.rand.Next(ToString().Length, Settings.BoardX);
                    StartingY = Settings.rand.Next(0, Settings.BoardY - ToString().Length);
                    return;
            }
        }
        public void ResetWord()
        {
            SetOrientation();
            SetCoordinates();
        }
        public void SetOrientation()
        {
            int num;

            if (Settings.AllowDiagonals) num = Settings.rand.Next(0, 4);
            else num = Settings.rand.Next(0, 2);

            if (num == 0) Orientation = Word.Direction.Horizontal;
            else if (num == 1) Orientation = Word.Direction.Vertical;
            else if (num == 2) Orientation = Word.Direction.DDown;
            else Orientation = Word.Direction.DUp;

            if (Settings.AllowBackwards)
            {
                num = Settings.rand.Next(0, 2);
                if (num == 1) ReverseWord();
            }
        }
        public override string ToString()
        {
            return OriginalWord;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZJV.WordSearch.BL
{
    public class WordGrid
    {
        /*
        todo: allow for manual placement of words through listbox selection
        todo: words shouldn't clump up as much as they tend to
        todo: EXCEPTION HANDLING:
              -Too many words, not enough space


        todo: document.
        todo: stop common curse words from randomly appearing :/ 

    */
        public bool[,] spotsIsTaken { get; set; }
        public char[,] Board { get; set; }
        public WordGrid()
        {
            CreateBoard();
            foreach(Word word in Settings.Words)
            {
                word.ResetWord();
            }
        }
        private void CreateBoard()
        {

            spotsIsTaken = new bool[Settings.BoardX, Settings.BoardY];

            Board = new char[Settings.BoardX, Settings.BoardY];


            for (int i = 0; i < Settings.BoardX; i++)
            {
                for (int j = 0; j < Settings.BoardY; j++)
                {
                    if (Settings.HideNoise == false) Board[i, j] = GetLetter();
                    else Board[i, j] = '+';
                    spotsIsTaken[i, j] = false;
                }
            }
            if(Settings.Words != null) foreach(Word word in Settings.Words) AddWordToBoard(word);
            
        }
        private char GetLetter()
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int num = Settings.rand.Next(0, chars.Length - 1);
            return chars[num];
        }
        public bool IsValidSpot(int x, int y, Word word, Word.Direction direction)
        {
            foreach (char letter in word.ManipulatedWord)
            {
                if (spotsIsTaken[x, y] == true && Board[x,y] != letter) return false;
                else
                {
                    if (direction == Word.Direction.Vertical) x++;
                    else if(direction == Word.Direction.DDown)
                    {
                        x++;
                        y++;
                    }
                    else if (direction == Word.Direction.DUp)
                    {
                        x--;
                        y++;
                    }
                    else y++;
                }
                    
            }
            return true;
        }
        public void FillNoise()
        {
            for (int i = 0; i < Settings.BoardX; i++)
            {
                for (int j = 0; j < Settings.BoardY; j++)
                {
                    if (!spotsIsTaken[i, j])
                    {
                        if (Settings.HideNoise == false) Board[i, j] = GetLetter();
                        else Board[i, j] = '+';
                    }
                }
            }

        }
        public void AddWordToBoard(Word word)
        {

            switch (word.Orientation)
            {
                case Word.Direction.Vertical:
                    word.SetCoordinates();
                    if (IsValidSpot(word.StartingX, word.StartingY, word, word.Orientation))
                    {
                        int x = word.StartingX;
                        int y = word.StartingY;
                        foreach (char letter in word.ManipulatedWord)
                        {
                            Board[x, y] = letter;
                            spotsIsTaken[x, y] = true;
                            x++;
                        }
                    }
                    else
                    {
                        AddWordToBoard(word);
                    }
                    return;
                case Word.Direction.Horizontal:

                    word.SetCoordinates();
                    if (IsValidSpot(word.StartingX, word.StartingY, word, word.Orientation))
                    {
                        int x = word.StartingX;
                        int y = word.StartingY;
                        foreach (char letter in word.ManipulatedWord)
                        {
                            Board[x, y] = letter;
                            spotsIsTaken[x, y] = true;
                            y++;
                        }
                    }
                    else
                    {
                        AddWordToBoard(word);
                    }
                    return;
                case Word.Direction.DDown:
                    word.SetCoordinates();
                    if (IsValidSpot(word.StartingX, word.StartingY, word, word.Orientation))
                    {
                        int x = word.StartingX;
                        int y = word.StartingY;
                        foreach (char letter in word.ManipulatedWord)
                        {
                            Board[x, y] = letter;
                            spotsIsTaken[x, y] = true;
                            y++;
                            x++;
                        }
                    }
                    else
                    {
                        AddWordToBoard(word);
                    }
                    return;
                case Word.Direction.DUp:
                    word.StartingX = Settings.rand.Next(word.ToString().Length, Settings.BoardX);
                    word.StartingY = Settings.rand.Next(0, Settings.BoardY - word.ToString().Length);
                    if (IsValidSpot(word.StartingX, word.StartingY, word, word.Orientation))
                    {
                        int x = word.StartingX;
                        int y = word.StartingY;
                        foreach (char letter in word.ManipulatedWord)
                        {
                            Board[x, y] = letter;
                            spotsIsTaken[x, y] = true;
                            y++;
                            x--;
                        }
                    }
                    else
                    {
                        AddWordToBoard(word);
                    }
                    return;


            }
            //Todo: continue Here

        }
        
    }
}

using System;
using System.Collections.Generic;

namespace ZJV.WordSearch.Mobile.Model
{
    public class SavedWordList
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Words { get; set; }
        public string SampleText
        {
            get
            {
                string sampleText = string.Empty;
                foreach (string word in Words)
                {
                    sampleText += word + ", ";
                }
                return sampleText;
            }
        }
        public SavedWordList(int id, string name, string description, List<string> words)
        {
            ID = id;
            Name = name;
            Description = description;
            Words = words;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}

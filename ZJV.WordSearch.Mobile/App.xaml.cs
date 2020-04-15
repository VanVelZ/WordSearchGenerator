using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZJV.WordSearch.BL;
using ZJV.WordSearch.Mobile.Model;

namespace ZJV.WordSearch.Mobile
{
    public partial class App : Application
    {
        public static WordGrid wordGrid;

        public static List<string> words = new List<string> {"Kitten", "Puppy", "Chick", "Duckling", "Bunny", "Calf" };
        public static List<SavedWordList> DictionaryList = new List<SavedWordList>();

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());

        }
        public static void Save()
        {
            string json = JsonConvert.SerializeObject(App.DictionaryList);
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string filePath = Path.Combine(path, "templates.txt");
            using (var file = File.Open(filePath, FileMode.Create, FileAccess.Write))
            using (var strm = new StreamWriter(file))
            {
                strm.Write(json);
            }
            ResyncData();
        }
        public static void Load()
        {
            string json;
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                string filePath = Path.Combine(path, "templates.txt");
                using (var file = File.Open(filePath, FileMode.Open, FileAccess.Read))
                using (var strm = new StreamReader(file))
                {
                    json = strm.ReadToEnd();
                }
            }
            catch
            {
                return;
            }
            App.DictionaryList = JsonConvert.DeserializeObject<List<SavedWordList>>(json);
            ResyncData();

        }
        public static void ResyncData()
        {
            Settings.Words = new List<Word>();
            foreach (string word in App.words)
            {
                Settings.Words.Add(new Word(word));
            }

        }
        protected override void OnSleep()
        {
            Save();
        }

        protected override void OnResume()
        {
            Load();
        }

        protected override void OnStart()
        {
            Load();
            App.wordGrid = new WordGrid();
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using ZJV.WordSearch.BL;
using ZJV.WordSearch.Mobile.Model;

namespace ZJV.WordSearch.Mobile.Views
{
    public partial class ManageListWords : ContentPage
    {
        string result = string.Empty;
        public ManageListWords()
        {
            InitializeComponent();
            Title = "Manage Words";
            ToolbarItem tbi = new ToolbarItem();

            tbi.Text = "Templates";

            tbi.Clicked += delegate {

                Navigation.PushAsync(new WordList());
            };
            ToolbarItems.Add(tbi);
            Rebind();

        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            if (txtWord.Text != "" && txtWord.Text != null && !txtWord.Text.Contains(" "))
            {
                if (Settings.MaxWords > App.words.Count) App.words.Add(txtWord.Text);
                else throw new Exception("That would be too many words for the grid.");
            }
            txtWord.Text = string.Empty;
            txtWord.Focus();
            App.ResyncData();
            App.wordGrid = new WordGrid();
            Rebind();
        }
        void Rebind()
        {
            ResyncData();
            lstWords.ItemsSource = null;
            lstWords.ItemsSource = App.words;

        }

        public void ResyncData()
        {
            Settings.Words = new List<Word>();
            foreach (string word in App.words)
            {
                Settings.Words.Add(new Word(word));
            }

        }
        void lstWords_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            App.words.RemoveAt(e.SelectedItemIndex);
            Rebind();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Rebind();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Rebind();
        }
        async Task<string> btnSave_Clicked()
        {
            SavedWordList newList = new SavedWordList(1, await DisplayPromptAsync("Save to templates", "What should we call this?"), "", App.words);
            App.DictionaryList.Add(newList);
            newList = null;
            App.Save();
            App.Load();

            return result;

        }

        void btnSave_Clicked(System.Object sender, System.EventArgs e)
        {
            Rebind();
            _ = btnSave_Clicked();
        }
        } } 


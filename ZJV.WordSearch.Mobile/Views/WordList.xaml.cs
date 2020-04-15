using System;
using System.Collections.Generic;

using Xamarin.Forms;
using ZJV.WordSearch.Mobile.Model;

namespace ZJV.WordSearch.Mobile.Views
{
    public partial class WordList : ContentPage
    {

        public WordList()
        {
            InitializeComponent();

            Title = "Templates";
            ToolbarItem tbi = new ToolbarItem();

            tbi.Text = "Clear";

            tbi.Clicked += delegate {

                App.DictionaryList.Clear();
                Navigation.PopAsync();
            };
            ToolbarItems.Add(tbi);

            GenerateList();
            
        }

        void lstTemplates_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            App.words = App.DictionaryList[e.SelectedItemIndex].Words;
            Navigation.PopAsync();
        }

        public void GenerateList()
        {
            var template = new DataTemplate(typeof(TextCell));

            template.SetBinding(TextCell.TextProperty, "Name");
            
            template.SetBinding(TextCell.DetailProperty, "SampleText");
            lstTemplates.ItemTemplate = template;
            lstTemplates.ItemsSource = App.DictionaryList;

        }
    }
}

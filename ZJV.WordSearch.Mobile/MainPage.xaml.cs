using System;
using System.Collections.Generic;
using System.ComponentModel;
using Syncfusion.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Xamarin.Forms;
using ZJV.WordSearch.BL;
using ZJV.WordSearch.Mobile.Views;

namespace ZJV.WordSearch.Mobile
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            Title = "Wordsearch Generator";

            ToolbarItem tbi = new ToolbarItem();
            
            tbi.Text = "Add Words";
            

            tbi.Clicked += delegate {

                Navigation.PushAsync(new ManageListWords());
            };
            ToolbarItems.Add(tbi);
            
            //Default Settings and list of words
            Settings.BoardX = 15;
            Settings.BoardY = 15;
            
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            App.ResyncData();
            UpdateText();
        }
        public void UpdateText()
        {
            lblShowWordsearch.Text = "";
            lblShowHeight.Text = "Height: " + Settings.BoardX;
            lblShowWidth.Text = "Width: " + Settings.BoardY;

            stpHeight.Minimum = 10;
            stpWidth.Minimum = 10;

            if (Settings.BoardY > 15) lblShowWordsearch.FontSize = 10;
            for (int i = 0; i < Settings.BoardX; i++)
            {
                for (int j = 0; j < Settings.BoardY; j++)
                {
                    lblShowWordsearch.Text +=App.wordGrid.Board[i, j].ToString().ToUpper() + " ";
                }
                lblShowWordsearch.Text += Environment.NewLine;
                
            }
        }
        public string PDFDisplayWords()
        {
            string pdfOut = string.Empty;

            int count = 0;
            foreach(string word in App.words)
            {
                pdfOut += word + "\t";
                count++;
                if(count == 3)
                {
                    count = 0;
                    pdfOut += Environment.NewLine + Environment.NewLine;
                }
            }
            return pdfOut;

        }
        public void ToggleNoise(object sender, EventArgs e)
        {
            Settings.HideNoise = swtchHideNoise.IsToggled;
            App.wordGrid.FillNoise();
            UpdateText();
        }
        public void ChangeSize(object sender, EventArgs e)
        {
            Settings.BoardX = Convert.ToInt32(stpHeight.Value);
            Settings.BoardY = Convert.ToInt32(stpWidth.Value);
            App.wordGrid = new WordGrid();
            UpdateText();
        }
        public void ToggleBackwards(object sender, EventArgs e)
        {
            if (swtchAllowBackwards.IsToggled) Settings.AllowBackwards = true;
            else Settings.AllowBackwards = false;

            App.ResyncData();

            App.wordGrid = new WordGrid();
            UpdateText();
        }
        public void ToggleDiagonals(object sender, EventArgs e)
        {
            if (swtchAllowDiagonal.IsToggled) Settings.AllowDiagonals = true;
            else Settings.AllowDiagonals = false;

            App.ResyncData();

            App.wordGrid = new WordGrid();
            UpdateText();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        void btnSave_Clicked(System.Object sender, System.EventArgs e)
        {
            SaveToPDF();
        }
        public void SaveToPDF()
        {
            // Create a new PDF document
            PdfDocument document = new PdfDocument();

            //Add a page to the document
            PdfPage page = document.Pages.Add();

            //Create PDF graphics for the page
            PdfGraphics graphics = page.Graphics;

            //Set the standard font
            int fontSize = 20;
            if (Settings.BoardX > 15 || Settings.BoardY > 15) fontSize = 10;
            PdfFont font = new PdfStandardFont(PdfFontFamily.Courier, fontSize);
            PdfFont font2 = new PdfStandardFont(PdfFontFamily.Courier, 10);

            //Draw the text
            graphics.DrawString(
                lblShowWordsearch.Text,
                font, PdfBrushes.Black, new PointF(80, 150));

            graphics.DrawString(
                PDFDisplayWords(),
                font2, PdfBrushes.Black, new PointF(150, 500));

            //Save the document to the stream
            MemoryStream stream = new MemoryStream();
            document.Save(stream);

            //Close the document
            document.Close(true);

            //Save the stream as a file in the device and invoke it for viewing
            Xamarin.Forms.DependencyService.Get<ISave>().SaveAndView("Output.pdf", "application / pdf", stream);
        }

        void btnRefresh_Clicked(Object sender, EventArgs e)
        {
            App.wordGrid = new WordGrid();
            UpdateText();
        }
    }
}

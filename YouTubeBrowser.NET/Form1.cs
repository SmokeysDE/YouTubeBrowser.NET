using CefSharp;
using CefSharp.WinForms;
using CefSharp.Handler;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace YouTubeBrowser.NET
{
    public partial class YouTube : Form {

        private ChromiumWebBrowser browser;

    
        public YouTube()
        {
            InitializeComponent();

            // Initialisiere CefSharp
            Cef.Initialize(new CefSettings());

            // Erstelle den ChromiumWebBrowser
            browser = new ChromiumWebBrowser("https://www.youtube.com");
            
            // Füge den ChromiumWebBrowser zum Formular hinzu
            Controls.Add(browser);
            browser.Dock = DockStyle.Fill;

            //Text des Fensters
            Text = "YouTube";

            //Icon anzeigen lassen
            ShowIcon = true;

            //eigene Methode um das Favicon von YT abzugreifen siehe unten
            SetFaviconFromUrl("https://www.youtube.com/favicon.ico");

            Size = new System.Drawing.Size(800, 600);
        }

        private void SetFaviconFromUrl(string url)
        {
            try
            {
                //using für den Garbage Collector, da die instanzverworfen werden kann.
                using (WebClient client = new WebClient())
                {
                    byte[] bytes = client.DownloadData(url);
                    using (MemoryStream stream = new MemoryStream(bytes))
                    {
                        // Erstelle ein Icon aus dem heruntergeladenen Favicon
                        Icon favicon = new Icon(stream);

                        // Setze das Favicon als Icon für das Formular
                        Icon = favicon;
                    }
                }
            }
            catch (Exception ex)
            {
                // Behandle etwaige Fehler beim Herunterladen und Setzen des Favicons
                Console.WriteLine("Fehler beim Herunterladen und Setzen des Favicons: " + ex.Message);
            }
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Setze das Formular immer im Vordergrund
            TopMost = true;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Schließe CefSharp ordnungsgemäß, wenn das Formular geschlossen wird
            Cef.Shutdown();
            base.OnFormClosing(e);
        }

        private void YouTube_Load(object sender, EventArgs e)
        {

        }
    }
}

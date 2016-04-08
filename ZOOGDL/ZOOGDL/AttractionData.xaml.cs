
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Newtonsoft.Json;
using System.Windows.Media.Imaging;


namespace ZOOGDL
{
    public partial class AttractionData : PhoneApplicationPage
    {
        General obj = new General();
        List<General> test = new List<General>();
        string[] etiquetas = {"Horario","Ecosistema","Fauna","Costo"};
        public AttractionData()
        {
            InitializeComponent();
            

            //Scroll.Height = stack.Height;

            System.Diagnostics.Debug.WriteLine(Especies._id + " id animal ------");
            string url = "http://www.movilzooguadalajara.com/attraction?id=" + Atracciones._id;        
            System.Diagnostics.Debug.WriteLine(url + " url ------");
            Data(url);
        }

        private void Data(string url)
        {
            WebClient webClient = new WebClient();

            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadStringCompleted);
            new Progress().showProgressIndicator(this, "Cargando contenido...");
            webClient.DownloadStringAsync(new Uri(url));
        }

        private void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)// *********
        {
            var rootObject = JsonConvert.DeserializeObject<RootObject>(e.Result);

            foreach (var book in rootObject.sfx_web_service)
            {
                AttractionImage.Source = new BitmapImage(new Uri(book.image, UriKind.RelativeOrAbsolute));
                DataPivot.Title = book.name;
                Information.Text = book.description;
                //Horary.Text = book.horary;
                try
                {
                    if (book.ecosystem.Length >= 4)
                    {
                        eEcosistema.Text = etiquetas[1].ToString();
                        Ecosystem.Text = book.ecosystem + "\n";
                    }
                    if (book.biodiversity.Length >= 4)
                    {
                        eFauna.Text = etiquetas[2].ToString();
                        Biodiversity.Text = book.biodiversity + "\n";
                    }                    
                }
                catch (Exception ex)
                {

                }

                replace(book.cost, book.horary);                
                obj.biodiversity = book.biodiversity;
                obj.ecosystem = book.ecosystem;
                System.Diagnostics.Debug.WriteLine(obj.horary + " horario ------");
                test.Add(obj);
                new Progress().hideProgressIndicator(this);
            }
        }
        private void replace(string cost, string horary)
        {
            cost = cost.Replace("<p>", "");
            cost = cost.Replace("</p>", "");
            cost = cost.Replace("<strong>", "");
            cost = cost.Replace("</strong>", "");
            cost = cost.Replace("&nbsp;", " ");
            cost = cost.Replace("&ntilde;", "ñ");
            cost = cost.Replace("<span style=\"font-family:sans-serif; font-size:14px\">", "");
            cost = cost.Replace("</span>", "");
            horary = horary.Replace("<br/>", "\n");
            horary = horary.Replace("/", "");
            System.Diagnostics.Debug.WriteLine(cost);
            try
            {
                if (cost.Length >= 4)
                {
                    eCosto.Text = etiquetas[3].ToString();
                    Cost.Text = cost;
                }
                if (horary.Length >= 4)
                {
                    eHorario.Text = etiquetas[0].ToString();
                    Horary.Text = horary + "\n";
                }
            }
            catch (Exception ex)
            {

            }
            //obj.horary = horary;
        }


        public class SfxWebService
        {
            public string id { get; set; }
            public string name { get; set; }
            public string logo { get; set; }
            public string image { get; set; }
            public string description { get; set; }
            public string content { get; set; }
            public string price_adult { get; set; }
            public string price_kid { get; set; }
            public string horary { get; set; }
            public string status { get; set; }
            public string order { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
            public string slug { get; set; }
            public string ecosystem { get; set; }
            public string biodiversity { get; set; }
            public string image_results { get; set; }
            public string cost { get; set; }
        }

        public class RootObject
        {
            public List<SfxWebService> sfx_web_service { get; set; }
        }

        public class General
        {
            public string horary;
            public string ecosystem;
            public string biodiversity;
            public string cost;
        }
    }
}


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
using Microsoft.Phone.Tasks;

namespace ZOOGDL
{
    public partial class AnimalData : PhoneApplicationPage
    {
        List<General> test = new List<General>();
        string url;

        public AnimalData()
        {
            InitializeComponent();            

            System.Diagnostics.Debug.WriteLine(Especies._id + " id animal ------");
            string url = "http://www.movilzooguadalajara.com/animal?id=" + Especies._id;
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
                System.Diagnostics.Debug.WriteLine(book.name + " nombre ------");
                /*foreach ( var lines in book.description)
                {
                    System.Diagnostics.Debug.WriteLine(lines + " descripcion ------");
                }*/

                DataAnimal.Title = book.name;
                System.Diagnostics.Debug.WriteLine(book.data_general + " descripcion ------");
                System.Diagnostics.Debug.WriteLine(book.description.Remove(0,32) + " descripcion ------");
                System.Diagnostics.Debug.WriteLine(book.description.Replace("</p>","\n") + " descripcion ------");
                book.description = book.description.Replace("<p style=\"text-align: justify;\">", "");
                book.description = book.description.Replace("</p>", "");
                Description.Text = book.description;
                System.Diagnostics.Debug.WriteLine(book.description + " descripcion ------");
                url = book.url_info;
                System.Diagnostics.Debug.WriteLine(book.url_info + " url ------");
                replace(book.data_general, book.description);
                //remplazar los acentos y las tildes con los mismos metodos  **********              
                

                //this.Data_General.Height = this.Data_General.Text.Length * 2;
                new Progress().hideProgressIndicator(this);
                AnimalImage.Source = new BitmapImage(new Uri(book.image, UriKind.RelativeOrAbsolute));
            }

             
                

        }

        private void replace(string general, string description)
        {
            general = general.Replace("<p>", "");
            general = general.Replace("<strong>", "");
            general = general.Replace("/p>", "\n");
            general = general.Replace("</strong>", "");
            general = general.Replace("<em>", "");
            general = general.Replace("</em>", "");
            general = general.Replace("<p style=\"text-align:justify\">", "");
            general = general.Replace("&aacute;", "á");
            general = general.Replace("&eacute;", "é");
            general = general.Replace("&iacute;", "í");
            general = general.Replace("&oacute;", "ó");
            general = general.Replace("&uacute;", "ú");
            general = general.Replace("&Aacute;", "Á");
            general = general.Replace("&Eacute;", "É");
            general = general.Replace("&Iacute;", "Í");
            general = general.Replace("&Oacute;", "Ó");
            general = general.Replace("&Uacute;", "Ú");
            general = general.Replace("&ntilde;", "ñ");
            general = general.Replace("&nbsp;", " ");
            general = general.Replace("<br />", "\n");
            //general = general.Replace(" ", "");
            description = description.Replace("<br />", "\n");
            //Data_General.Text = general;
            
            System.Diagnostics.Debug.WriteLine(test.ToString() + " text ------");
            //myList.ItemsSource = test.ToString();
            tokens(general);
            //myData.Text = general;
            Description.Text = description;
            
        }

        private void tokens(string general)
        {
            try
            {
                string s1 = general; //se va a descomponer todo el string para borrar los espacios en blanco
                char[] seps = { ':','<','\n' };
                string[] values = s1.Split(seps);   //es el Json ya dividido
                string[] values2 = new string[20];   // es donde se guardará la inforamción antes de imprimirla en pantalla
                int i = 0;
                foreach (string token in values)
                {
                    if (token.Length >= 4)
                    {
                        //token = token.Replace("\n","");
                        System.Diagnostics.Debug.WriteLine(token + " values ------");
                        values2[i] = token;
                        i++;
                    }

                }


                eNombre.Text = values2[0].ToString();
                rNombre.Text = values2[1].ToString();
                eClase.Text = values2[2].ToString();
                rClase.Text = values2[3].ToString();
                eOrden.Text = values2[4].ToString();
                rOrden.Text = values2[5].ToString();
                eFamilia.Text = values2[6].ToString();
                rFamilia.Text = values2[7].ToString();
                eAlimentacion.Text = values2[8].ToString();
                rAlimentacion.Text = values2[9].ToString();
                eCamada.Text = values2[10].ToString();
                rCamada.Text = values2[11].ToString();
                eGestacion.Text = values2[12].ToString();
                rGestacion.Text = values2[13].ToString();
                eLongevidad.Text = values2[14].ToString();
                rLongevidad.Text = values2[15].ToString();
                eHabitat.Text = values2[16].ToString();
                rHabitat.Text = values2[17].ToString();
                eDistribucion.Text = values2[18].ToString();
                rDistribucion.Text = values2[19].ToString();


                //myData.Text = general;
            }catch(Exception ex)
            {

            }

        }


        public class SfxWebService
        {
            public string id { get; set; }
            public string name { get; set; }
            public string image { get; set; }
            public string url_info { get; set; }
            public string description { get; set; }
            public string data_general { get; set; }
            public bool exceeded_description { get; set; }
        }

        public class RootObject
        {
            public List<SfxWebService> sfx_web_service { get; set; }
        }

        public class General
        {
            public string generalData;
            public string data;
        }

        private void link_Tap(object sender, GestureEventArgs e)
        {
            WebBrowserTask enlace = new WebBrowserTask();
            enlace.Uri = new Uri(url);
            enlace.Show();
        }

        
    }
}

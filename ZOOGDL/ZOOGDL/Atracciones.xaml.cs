
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
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows.Media.Imaging;

namespace ZOOGDL
{
    public partial class Atracciones : PhoneApplicationPage
    {
        List<string> _listaId = new List<string>();
        public Atracciones()
        {
            InitializeComponent();
            atractions();
        }

        private void atractions()
        {
            WebClient webClient = new WebClient();

            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadStringCompleted);
            webClient.DownloadStringAsync(new Uri("http://www.movilzooguadalajara.com/attraction"));
        }

        private void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)// *********
        {
            var rootObject = JsonConvert.DeserializeObject<RootObject>(e.Result);

            foreach (var book in rootObject.sfx_web_service)
            {
                int i = 0;
                foreach (var item in book.attractions)
                {
                    System.Diagnostics.Debug.WriteLine(i + " id ");
                    System.Diagnostics.Debug.WriteLine(item.name + " nombre ");
                    System.Diagnostics.Debug.WriteLine(item.logo + " imagen ");                    
                    _listaId.Add(item.id);
                    //Button_1.Title = item.name;
                    //Button_1.Picture.Source = new BitmapImage(new Uri(item.image));
                    botones(i, item.name, item.logo);
                    i++;
                }
            }
        }

        private void botones(int id, string name, string image)
        {

            if (id == 0)
            {
                //Button_1.Title = name;
                Button_1.Source = new BitmapImage(new Uri("/Images/villaustraliana.png", UriKind.RelativeOrAbsolute));
                //Button_1.Picture.Source = new BitmapImage(new Uri("/Images/trenpanoramico.png", UriKind.RelativeOrAbsolute));
            }
            else if (id == 1)
            {
                //Button_2.Title = name;
                Button_2.Source = new BitmapImage(new Uri("/Images/trenpanoramico.png", UriKind.RelativeOrAbsolute));
            }
            else if (id == 2)
            {
                //Button_3.Title = name;
                Button_3.Source = new BitmapImage(new Uri("/Images/skyzoo.png", UriKind.RelativeOrAbsolute));
            }
            else if (id == 3)
            {
                //Button_4.Title = name;
                Button_4.Source = new BitmapImage(new Uri("/Images/shows.png", UriKind.RelativeOrAbsolute));
            }
            else if (id == 4)
            {
                //Button_5.Title = name;
                Button_5.Source = new BitmapImage(new Uri("/Images/selvatropical.png", UriKind.RelativeOrAbsolute));
            }
            else if (id == 5)
            {
                //Button_6.Title = name;
                Button_6.Source = new BitmapImage(new Uri("/Images/safarimasaimara.png", UriKind.RelativeOrAbsolute));
            }
            else if (id == 6)
            {
                //Button_7.Title = name;
                Button_7.Source = new BitmapImage(new Uri("/Images/ranchoveterinario.png", UriKind.RelativeOrAbsolute));
            }
            else if (id == 7)
            {
                //Button_8.Title = name;
                Button_8.Source = new BitmapImage(new Uri("/Images/monkeyland.png", UriKind.RelativeOrAbsolute));
            }

            else if (id == 8)
            {
                //Button_9.Title = name;
                Button_9.Source = new BitmapImage(new Uri("/Images/herpetarios.png", UriKind.RelativeOrAbsolute));
            }
            else if (id == 9)
            {
                //Button_10.Title = name;
                Button_10.Source = new BitmapImage(new Uri("/Images/CIA.png", UriKind.RelativeOrAbsolute));
            }
            else if (id == 10)
            {
                //Button_11.Title = name;
                Button_11.Source = new BitmapImage(new Uri("/Images/safarimasaimara.png", UriKind.RelativeOrAbsolute));
            }
            else if (id == 11)
            {
                //Button_12.Title = name;
                Button_12.Source = new BitmapImage(new Uri("/Images/amazonia.png", UriKind.RelativeOrAbsolute));
            }
            else if (id == 12)
            {
                //Button_13.Title = name;
                Button_13.Source = new BitmapImage(new Uri("/Images/acuario.png", UriKind.RelativeOrAbsolute));
            }
            else if (id == 13)
            {
                //Button_14.Title = name;
                Button_14.Source = new BitmapImage(new Uri("/Images/acuario.png", UriKind.RelativeOrAbsolute));
            }

        }

        private void Button_1_Tap(object sender, GestureEventArgs e)
        {
            _id = _listaId[0];
            this.NavigationService.Navigate(new Uri("/AttractionData.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Button_2_Tap(object sender, GestureEventArgs e)
        {
            _id = _listaId[1];
            this.NavigationService.Navigate(new Uri("/AttractionData.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Button_3_Tap(object sender, GestureEventArgs e)
        {
            _id = _listaId[2];
            this.NavigationService.Navigate(new Uri("/AttractionData.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Button_4_Tap(object sender, GestureEventArgs e)
        {
            _id = _listaId[3];
            this.NavigationService.Navigate(new Uri("/AttractionData.xaml", UriKind.RelativeOrAbsolute));
        }
        

        private void Button_5_Tap(object sender, GestureEventArgs e)
        {
            _id = _listaId[4];
            this.NavigationService.Navigate(new Uri("/AttractionData.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Button_6_Tap(object sender, GestureEventArgs e)
        {
            _id = _listaId[5];
            this.NavigationService.Navigate(new Uri("/AttractionData.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Button_7_Tap(object sender, GestureEventArgs e)
        {
            _id = _listaId[6];
            this.NavigationService.Navigate(new Uri("/AttractionData.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Button_8_Tap(object sender, GestureEventArgs e)
        {
            _id = _listaId[7];
            this.NavigationService.Navigate(new Uri("/AttractionData.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Button_9_Tap(object sender, GestureEventArgs e)
        {
            _id = _listaId[8];
            this.NavigationService.Navigate(new Uri("/AttractionData.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Button_10_Tap(object sender, GestureEventArgs e)
        {
            _id = _listaId[9];
            this.NavigationService.Navigate(new Uri("/AttractionData.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Button_11_Tap(object sender, GestureEventArgs e)
        {
            _id = _listaId[10];
            this.NavigationService.Navigate(new Uri("/AttractionData.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Button_12_Tap(object sender, GestureEventArgs e)
        {
            _id = _listaId[11];
            this.NavigationService.Navigate(new Uri("/AttractionData.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Button_13_Tap(object sender, GestureEventArgs e)
        {
            _id = _listaId[12];
            this.NavigationService.Navigate(new Uri("/AttractionData.xaml", UriKind.RelativeOrAbsolute));
        }
        private void Button_14_Tap(object sender, GestureEventArgs e)
        {
            _id = _listaId[13];
            this.NavigationService.Navigate(new Uri("/AttractionData.xaml", UriKind.RelativeOrAbsolute));
        }

        public static string _id { get; set; } //lista de id´s

        public class Attraction
        {
            public string id { get; set; }
            public string name { get; set; }
            public string logo { get; set; }
        }

        public class SfxWebService
        {
            public List<Attraction> attractions { get; set; }
        }

        public class RootObject
        {
            public List<SfxWebService> sfx_web_service { get; set; }
        }

    }
}

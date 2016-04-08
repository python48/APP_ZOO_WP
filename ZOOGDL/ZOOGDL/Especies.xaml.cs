
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
    public partial class Especies : PhoneApplicationPage
    {
        List<string> _listaId = new List<string>();
        public Especies()
        {
            InitializeComponent();
            species();
        }

        private void species()
        {
            WebClient webClient = new WebClient();

            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadStringCompleted);
            webClient.DownloadStringAsync(new Uri("http://www.movilzooguadalajara.com/animal"));
        }

        private void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)// *********
        {
            var rootObject = JsonConvert.DeserializeObject<RootObject>(e.Result);

            foreach (var book in rootObject.sfx_web_service)
            {
                int i = 0;
                foreach (var item in book.animals)
                {
                    System.Diagnostics.Debug.WriteLine(item.id + " id ");
                    System.Diagnostics.Debug.WriteLine(item.name + " nombre ");
                    System.Diagnostics.Debug.WriteLine(item.image + " imagen ");
                    _listaId.Add(item.id);
                    //Button_1.Title = item.name;
                    //Button_1.Picture.Source = new BitmapImage(new Uri(item.image));
                    botones(i, item.name, item.image, id_Animal);
                    i++;
                }
            }
        }

        private void botones (int id, string name, string image, int Animal)
        {

            if (id == 0)
            {
                System.Diagnostics.Debug.WriteLine(Animal + " id animal ");
                System.Diagnostics.Debug.WriteLine(_listaId[0] + " id animal ------");
                //Button_1.Title = name;
                //Button_1.Picture.Source = new BitmapImage(new Uri(image, UriKind.RelativeOrAbsolute));
                Button_1.Source = new BitmapImage(new Uri(image, UriKind.RelativeOrAbsolute));
                Text_1.Text = name;
            }
            else if (id == 1)
            {
                System.Diagnostics.Debug.WriteLine(Animal + " id animal");
                //Button_2.Title = name;
                //Button_2.Picture.Source = new BitmapImage(new Uri(image, UriKind.RelativeOrAbsolute));
                Button_2.Source = new BitmapImage(new Uri(image, UriKind.RelativeOrAbsolute));
                Text_2.Text = name;               
            }
            else if (id == 2)
            {
                System.Diagnostics.Debug.WriteLine(Animal + " id animal");
                //Button_3.Title = name;
                //Button_3.Picture.Source = new BitmapImage(new Uri(image, UriKind.RelativeOrAbsolute));
                Button_3.Source = new BitmapImage(new Uri(image, UriKind.RelativeOrAbsolute));
                Text_3.Text = name;
            }
            else if (id == 3)
            {
                System.Diagnostics.Debug.WriteLine(Animal + " id animal");
                //Button_4.Title = name;
                //Button_4.Picture.Source = new BitmapImage(new Uri(image, UriKind.RelativeOrAbsolute));
                Button_4.Source = new BitmapImage(new Uri(image, UriKind.RelativeOrAbsolute));
                Text_4.Text = name;
            }
            else if (id == 4)
            {
                System.Diagnostics.Debug.WriteLine(Animal + " id animal");
                //Button_5.Title = name;
                //Button_5.Picture.Source = new BitmapImage(new Uri(image, UriKind.RelativeOrAbsolute));
                Button_5.Source = new BitmapImage(new Uri(image, UriKind.RelativeOrAbsolute));
                Text_5.Text = name;
            }
            else if (id == 5)
            {
                System.Diagnostics.Debug.WriteLine(Animal + " id animal");
                //Button_6.Title = name;
                //Button_6.Picture.Source = new BitmapImage(new Uri(image, UriKind.RelativeOrAbsolute));
                Button_6.Source = new BitmapImage(new Uri(image, UriKind.RelativeOrAbsolute));
                Text_6.Text = name;
            }

        }        

        private void Button_1_Tap(object sender, GestureEventArgs e)
        {
            _id = _listaId[0];
            this.NavigationService.Navigate(new Uri("/AnimalData.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Button_2_Tap(object sender, GestureEventArgs e)
        {
            _id = _listaId[1];
            this.NavigationService.Navigate(new Uri("/AnimalData.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Button_3_Tap(object sender, GestureEventArgs e)
        {
            _id = _listaId[2];
            this.NavigationService.Navigate(new Uri("/AnimalData.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Button_4_Tap(object sender, GestureEventArgs e)
        {
            _id = _listaId[3];
            this.NavigationService.Navigate(new Uri("/AnimalData.xaml", UriKind.RelativeOrAbsolute));
        }
        

        private void Button_5_Tap(object sender, GestureEventArgs e)
        {
            _id = _listaId[4];
            this.NavigationService.Navigate(new Uri("/AnimalData.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Button_6_Tap(object sender, GestureEventArgs e)
        {
            _id = _listaId[5];
            this.NavigationService.Navigate(new Uri("/AnimalData.xaml", UriKind.RelativeOrAbsolute));
        }

        public static int id_Animal { get; set; }

        public static string _id { get; set; } //lista de id´s

        public class Animal
        {
            public string id { get; set; }
            public string name { get; set; }
            public string image { get; set; }
        }

        public class SfxWebService
        {
            public List<Animal> animals { get; set; }
        }

        public class RootObject
        {
            public List<SfxWebService> sfx_web_service { get; set; }
        }

        
    }    

}

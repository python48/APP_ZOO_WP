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
using System.Threading;
using System.Windows.Threading;
using Microsoft.Phone.Shell;


namespace ZOOGDL
{
    public partial class MainPage : PhoneApplicationPage
    {
        BitmapImage bm = new BitmapImage();
        private List<BitmapImage> _lbm = new List<BitmapImage>();
        List<ViewModel> items = new List<ViewModel>();
        ViewModel model;
        int id_image = 0;
        DispatcherTimer playlistTimer = null;
        
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            
                banners();

                playlistTimer = new DispatcherTimer();

                //playlistTimer.Tick += playlistTimer_Tick;
                playlistTimer.Tick += new EventHandler(playlistTimer_Tick);
                playlistTimer.Interval = new TimeSpan(0, 0, 5);
                playlistTimer.Start();
            
        }

        private void banners()
        {
            //pro.showProgressIndicator(this, "Calculando");
            try
            {
                
                WebClient webClient = new WebClient();
                //pro.showProgressIndicator(this, "Cargando");
                webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadStringCompleted);

                /*progressIndicator = new ProgressIndicator()
                {
                    IsVisible = true,
                    IsIndeterminate = false,
                    Text = "Loading..."
                };
                SystemTray.SetProgressIndicator(this, progressIndicator);*/
                new Progress().showProgressIndicator(this, "Cargando...");
                webClient.DownloadStringAsync(new Uri("http://www.movilzooguadalajara.com/banner?entity=main"));
                //pro.hideProgressIndicator(this);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Para poder visualizar las imagenes, se requiere de conexión a internet.");
            }
            
        }

        private void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)// *********
        {
                            
            try{
               /* Dispatcher.BeginInvoke(() =>
                {
                    progressIndicator.IsVisible = false;*/
                    
            var rootObject = JsonConvert.DeserializeObject<RootObject>(e.Result);
            
            foreach (var book in rootObject.sfx_web_service)
            {

                foreach (var item in book.banners)
                {
                                        
                    System.Diagnostics.Debug.WriteLine(item.image + " ******** ");
                    //BannerImage.Source = new BitmapImage(new Uri(item.image));
                    _lbm.Add(new BitmapImage(new Uri(item.image, UriKind.RelativeOrAbsolute)));
                   
                }
               
                //System.Diagnostics.Debug.WriteLine(prueba + " /////// ");
                //System.Diagnostics.Debug.WriteLine(book.ToString() + " ******** ");

            }

            System.Diagnostics.Debug.WriteLine(_lbm.Count());
            int count = _lbm.Count();
            //for (int i = 0; i < count; i++)
            //{

               //BannerImage.Source = _lbm[0];
                //Thread.Sleep(200);
            //}

            /*foreach(var image in _lbm)
            {
                BannerImage.Source = _lbm.Remove();
            }*/

            ImageCarousel();
            
                //});    
            }
            catch(Exception ex)
            {
                MessageBox.Show("Para poder visualizar las imagenes, se requiere de conexión a internet.");
            }
               
        }

        private void ImageCarousel()
        {
            //List<ViewModel> items = new List<ViewModel>();

            for (int i = 0; i <= 10; i++)
            {
                model = new ViewModel()
                {                    
                    ImagePath = _lbm[i]
                };

                items.Add(model);
            }

            this.DataContext = items;
        }

        int count = 0;
        private void playlistTimer_Tick(object sender, EventArgs e)
        {

            if (items != null)
            {
                if (count == items.Count - 1)
                    count = 0;
                new Progress().hideProgressIndicator(this);
                if (count < items.Count)
                {
                    ImageRotation(count);
                    count++;                    
                }
            }
        }

        private void ImageRotation(int i)
        {
            System.Diagnostics.Debug.WriteLine(" ******** " + i);
             model.ImagePath = _lbm[i];
             banner.Source = _lbm[i];
        }
         

        private void changeImage(int id)
        {
          /*  switch(id)
            {
                case 0:
                    BannerImage.Source = _lbm[0];
                    break;
                case 1:
                    BannerImage.Source = _lbm[1];
                    break;
                case 2:
                    BannerImage.Source = _lbm[2];
                    break;
                case 3:
                    BannerImage.Source = _lbm[3];
                    break;
                case 4:
                    BannerImage.Source = _lbm[4];
                    break;
                case 5:
                    BannerImage.Source = _lbm[5];
                    break;
                case 6:
                    BannerImage.Source = _lbm[6];
                    break;
                case 7:
                    BannerImage.Source = _lbm[7];
                    break;
                case 8:
                    BannerImage.Source = _lbm[8];
                    break;
                case 9:
                    BannerImage.Source = _lbm[9];
                    break;
                case 10:
                    BannerImage.Source = _lbm[10];
                    break;
                case 11:
                    BannerImage.Source = _lbm[11];
                    break;
                default:
                    break;
            }*/
        }

        private void Button_Next(object sender, RoutedEventArgs e)
        {
            id_image++;
            changeImage(id_image);
        }

        private void Button_Previous(object sender, RoutedEventArgs e)
        {
            id_image--;
            changeImage(id_image);
        }  

        private void btn_especie_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Especies.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btn_mapa_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Map.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btn_servicio_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Servicios.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btn_atracion_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Atracciones.xaml", UriKind.RelativeOrAbsolute));
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (NavigationService.BackStack.Count() == 1)
            {
                NavigationService.RemoveBackEntry();
            }

        }


        public class Banner
        {
            public string name { get; set; }
            public string image { get; set; }
        }
        
        public class SfxWebService
        {
            public List<Banner> banners { get; set; }          
        }

        public class RootObject
        {
            public List<SfxWebService> sfx_web_service { get; set; }
        }

        public class ViewModel
        {
            public string Title { get; set; }
            public BitmapImage ImagePath { get; set; }
            public string Description { get; set; }
        }
                
        private void About_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/AboutPage.xaml", UriKind.RelativeOrAbsolute));
        }
        

    }       

}

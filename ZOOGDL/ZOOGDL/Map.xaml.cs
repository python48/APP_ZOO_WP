using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Windows.Devices.Geolocation;
using System.Device.Location;
using System.Windows.Shapes;
using System.Windows.Media;
using Microsoft.Phone.Maps.Controls;
using System.Text;
using System.Xml.Linq;
using System.Xml;
using System.IO;
using Newtonsoft.Json;
using System.Windows.Media.Imaging;
using GART;
using Location = System.Device.Location.GeoCoordinate;
using GART.Data;

namespace ZOOGDL
{
    public partial class Map : PhoneApplicationPage
    {
        Geolocator myGeolocator = new Geolocator();
        Geoposition myGeoPosition = null;
        List<MapIcon> listPushpin = new List<MapIcon>();
        WebClient webClient = new WebClient();
        MapIcon mpi = new MapIcon();
        GeoCoordinate me, dest;
        Progress pro = new Progress();

        double[,] iteso = new double[,] { {20.60631349,-103.41838535}, {20.60668657,-103.41724340}, {20.60665195,-103.41663672}, {20.60664273,-103.41637034}, {20.60605877,-103.41594127}};
        public Map()
        {
            InitializeComponent();
            myLocation();
            //cargarLista();
            

            if(Servicios.isClick)
            {
                service_id();
            }

            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadStringCompleted);
        }

        private async void myLocation()
        {
            myGeolocator.DesiredAccuracyInMeters = 5;
            pro.showProgressIndicator(this, "Espere un momento...");
            try
            {                
                myGeoPosition = await myGeolocator.GetGeopositionAsync(TimeSpan.FromMinutes(1), TimeSpan.FromSeconds(10));
                
            }
            catch
            {
                MessageBox.Show("Ubicación esta desactivada en tu dispositivo");
            }
            pro.hideProgressIndicator(this);
            this.MyMap.Center = new GeoCoordinate(myGeoPosition.Coordinate.Latitude, myGeoPosition.Coordinate.Longitude);
            this.MyMap.ZoomLevel = 18;

            Pushpins();
        }

        private void Pushpins()
        {
            Ellipse eli = new Ellipse();
            eli.Width = 20;
            eli.Height = 20;
            eli.Fill = new SolidColorBrush(Colors.Blue);

            MapOverlay mapOverlay = new MapOverlay();
            mapOverlay.Content = eli;
            mapOverlay.GeoCoordinate = new GeoCoordinate(myGeoPosition.Coordinate.Latitude, myGeoPosition.Coordinate.Longitude);

            MapLayer mapLayer = new MapLayer();

            mapLayer.Add(mapOverlay);

            MyMap.Layers.Add(mapLayer);

            //OtherPushpins();
        }

        public void drawPushpin(List<MapIcon> imgenesp) // Metodo que dibuja una lista de puntos en el mapa
        {
            var capapuntos = new MapLayer();

            foreach (var imgp in imgenesp)
            {
                var imgusuarioenelmapa = new MapOverlay();
                GeoCoordinate ubicacion = new GeoCoordinate(double.Parse(imgp.latitud), double.Parse(imgp.longitud));
                imgusuarioenelmapa.GeoCoordinate = ubicacion;
                imgusuarioenelmapa.Content = imgp; //carga la imagen
                capapuntos.Add(imgusuarioenelmapa);

            }
            MyMap.Layers.Add(capapuntos);
        }

        public void cargarLista()           //lee las coordenadas del archivo xml
        {
            StringBuilder output = new StringBuilder();
            XElement booksFromFile = XElement.Load(@"Resources/coordenadas.xml");
            String xmlString = booksFromFile.ToString();

            using (XmlReader reader = XmlReader.Create(new StringReader(xmlString)))
            {
                int i = 0;
                while (i < 112)
                {
                    MapIcon imp = new MapIcon();
                    reader.ReadToFollowing("id");
                    reader.MoveToFirstAttribute();
                    output.AppendLine(reader.ReadElementContentAsString());
                    imp.id = output.ToString();
                    imp.id = imp.id.Substring(0, imp.id.Length - 2);
                    output.Clear();
                    reader.ReadToFollowing("Latitude");
                    reader.MoveToFirstAttribute();
                    output.AppendLine(reader.ReadElementContentAsString());
                    imp.latitud = "20." + output.ToString();
                    //imp.latitud = imp.latitud.Substring(0, imp.latitud.Length -2);
                    output.Clear();
                    reader.ReadToFollowing("Longitude");
                    reader.MoveToFirstAttribute();
                    output.AppendLine(reader.ReadElementContentAsString());
                    imp.longitud = "-103." + output.ToString();
                    //imp.longitud = imp.latitud.Substring(0, imp.latitud.Length -2);
                    output.Clear();
                    reader.ReadToFollowing("Place");
                    reader.MoveToFirstAttribute();
                    output.AppendLine(reader.ReadElementContentAsString());
                    imp.lugar = output.ToString();
                    //imp.longitud = imp.latitud.Substring(0, imp.latitud.Length -2);
                    output.Clear();

                    listPushpin.Add(imp);
                    i++;

                }
            }
            drawPushpin(listPushpin);

        }



        private void MyMap_CenterChanged(object sender, MapCenterChangedEventArgs e)
        {
            foreach (var obj in listPushpin)
            {
                obj.pushPinHeader.Opacity = 0;
                obj.box.Opacity = 0;
                obj.pushpin.Opacity = 100;
            }
        }


        public static int id_pin;
        ApplicationBarIconButton btn0;
        ApplicationBarIconButton btn1;
        ApplicationBarIconButton btn2;
        private void animales_Click(object sender, EventArgs e)
        {
            btn0 = (ApplicationBarIconButton)ApplicationBar.Buttons[0];
            btn1 = (ApplicationBarIconButton)ApplicationBar.Buttons[1];
            btn2 = (ApplicationBarIconButton)ApplicationBar.Buttons[2];
            id_pin = 0;
            listPushpin.Clear();
            MyMap.Layers.Clear();
            ARDisplay.ARItems.Clear();
            myLocation();            
            webClient.DownloadStringAsync(new Uri("http://www.movilzooguadalajara.com/map?type=main&entity=animals"));
            //btn0.IconUri = new Uri("/Images/animales_select.png", UriKind.RelativeOrAbsolute);     //Fallo**********************+         
            btn0.IsEnabled = false;
            btn1.IsEnabled = true;
            btn2.IsEnabled = true;
        }
                
        private void atracciones_Click(object sender, EventArgs e)
        {
            btn0 = (ApplicationBarIconButton)ApplicationBar.Buttons[0];
            btn1 = (ApplicationBarIconButton)ApplicationBar.Buttons[1];
            btn2 = (ApplicationBarIconButton)ApplicationBar.Buttons[2];
            id_pin = 1;
            listPushpin.Clear();
            MyMap.Layers.Clear();
            ARDisplay.ARItems.Clear();
            myLocation();
            webClient.DownloadStringAsync(new Uri("http://www.movilzooguadalajara.com/map?type=main&entity=attractions"));
            btn0.IsEnabled = true;
            btn1.IsEnabled = false;
            btn2.IsEnabled = true;
        }

        private void servicios_Click(object sender, EventArgs e)
        {
            btn0 = (ApplicationBarIconButton)ApplicationBar.Buttons[0];
            btn1 = (ApplicationBarIconButton)ApplicationBar.Buttons[1];
            btn2 = (ApplicationBarIconButton)ApplicationBar.Buttons[2];
            id_pin = 2;
            listPushpin.Clear();
            MyMap.Layers.Clear();
            ARDisplay.ARItems.Clear();
            myLocation();
            webClient.DownloadStringAsync(new Uri("http://www.movilzooguadalajara.com/map?type=main&entity=services"));
            btn0.IsEnabled = true;
            btn1.IsEnabled = true;
            btn2.IsEnabled = false;
        }

        private void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)// *********
        {
            var rootObject = JsonConvert.DeserializeObject<RootObject>(e.Result);
            int x = 0;

             foreach (var book in rootObject.sfx_web_service)
             {
                  foreach (var item in book.maps)
                  {
                      MapIcon mpi = new MapIcon();                             
                      /*System.Diagnostics.Debug.WriteLine(item.place + " id ");
                      System.Diagnostics.Debug.WriteLine("-103." + item.longitude + " nombre ");
                      System.Diagnostics.Debug.WriteLine("20." + item.latitude + " imagen ");*/
                      if(x<=4)
                      {
                          mpi.longitud = iteso[x, 1].ToString();
                          mpi.latitud = iteso[x, 0].ToString();
                          x++;
                      }else if(x == 5)
                      {
                          break;
                      }
                      else{
                      mpi.longitud = "-103." + item.longitude;
                      mpi.latitud = "20." + item.latitude;
                      }
                      
                      
                      Location offset = new Location()
                      {
                          Latitude = Convert.ToDouble(mpi.latitud),
                          Longitude = Convert.ToDouble(mpi.longitud),
                          //Latitude = 20.609597,
                          //Longitude = -103.412007,
                          //Latitude = 20.725220,//zoo                          
                          //Longitude = -103.307884, //zoo
                          //Latitude = iteso[x,0],
                          //Longitude = iteso[x,1],
                          Altitude = Double.NaN // NaN will keep it on the horizon
                      };

                      System.Diagnostics.Debug.WriteLine(offset.Latitude + " nombre ");
                      System.Diagnostics.Debug.WriteLine(offset.Longitude + " imagen ");
                      dest = new GeoCoordinate(offset.Latitude, offset.Longitude);
                      me = MyMap.Center;
                      Double distance = me.GetDistanceTo(dest);
                      distance = Math.Round(distance, 0);
                      mpi.lugar = item.place + " " + distance.ToString() + " m";
                      System.Diagnostics.Debug.WriteLine(distance + " distancia");
                      AddLabel(offset, item.place + " " + distance.ToString() + " m");
                      listPushpin.Add(mpi);
                                            
                  }
                              
             }

             drawPushpin(listPushpin);
        }

        private void service_id()
        {
            int id_servicios = Servicios.id_servicios;
            if(id_servicios == 0)
            {
                id_pin = 2;
                listPushpin.Clear();
                MyMap.Layers.Clear();
                webClient.DownloadStringAsync(new Uri("http://www.movilzooguadalajara.com/map?type=specific&entity=services&specific=toilets"));
            }else if(id_servicios == 1)
            {
                id_pin = 2;
                listPushpin.Clear();
                MyMap.Layers.Clear();
                webClient.DownloadStringAsync(new Uri("http://www.movilzooguadalajara.com/map?type=specific&entity=services&specific=restaurants"));
            }
            else if (id_servicios == 2)
            {
                id_pin = 2;
                listPushpin.Clear();
                MyMap.Layers.Clear();
                webClient.DownloadStringAsync(new Uri("http://www.movilzooguadalajara.com/map?type=specific&entity=services&specific=atm"));
            }
            else if(id_servicios == 3)
            {
                id_pin = 2;
                listPushpin.Clear();
                MyMap.Layers.Clear();
                webClient.DownloadStringAsync(new Uri("http://www.movilzooguadalajara.com/map?type=specific&entity=services&specific=memories"));
            }
            else if(id_servicios == 4)
            {
                id_pin = 2;
                listPushpin.Clear();
                MyMap.Layers.Clear();
                webClient.DownloadStringAsync(new Uri("http://www.movilzooguadalajara.com/map?type=specific&entity=services&specific=phones"));
            } 
            else if(id_servicios == 5)
            {
                id_pin = 2;
                listPushpin.Clear();
                MyMap.Layers.Clear();
                webClient.DownloadStringAsync(new Uri("http://www.movilzooguadalajara.com/map?type=specific&entity=services&specific=first_aid"));
            }
            else if(id_servicios == 6)
            {
                id_pin = 2;
                listPushpin.Clear();
                MyMap.Layers.Clear();
                webClient.DownloadStringAsync(new Uri("http://www.movilzooguadalajara.com/map?type=specific&entity=services&specific=information"));
            }           
            

        }

               
        public class Maps
        {
            public string latitude { get; set; }
            public string longitude { get; set; }
            public string place { get; set; }
        }

        public class SfxWebService
        {
            public List<Maps> maps { get; set; }
        }

        public class RootObject
        {
            public List<SfxWebService> sfx_web_service { get; set; }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            // Start AR services
            ARDisplay.StartServices();

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            // Stop AR services
            ARDisplay.StopServices();

            base.OnNavigatedFrom(e);
        }

        private void AddLabel(GeoCoordinate location, string label)
        {
            // We'll use the specified text for the content and we'll let 
            // the system automatically project the item into world space
            // for us based on the Geo location.
            ARItem item = new ARItem()
            {
                Content = label,
                GeoLocation = location,
            };

            ARDisplay.ARItems.Add(item);
            //CalculateRoute(location);
        }

        private void video_Click(object sender, RoutedEventArgs e)
        {
            UIHelper.ToggleVisibility(VideoPreview);
            UIHelper.ToggleVisibility(WorldView);
            if(MyMap.Visibility == Visibility.Visible)
            {
                MyMap.Visibility = Visibility.Collapsed;
                //HeadingIndicator.Visibility = Visibility.Visible;
            }
            else
            {
                MyMap.Visibility = Visibility.Visible;
                //HeadingIndicator.Visibility = Visibility.Collapsed;
            }
            
        }

        private void location_Click(object sender, RoutedEventArgs e)
        {
            myLocation();
        }


        
    }
}
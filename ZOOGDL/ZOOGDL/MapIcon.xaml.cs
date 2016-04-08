using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media.Imaging;

namespace ZOOGDL
{
    public partial class MapIcon : UserControl
    {

        public MapIcon()
        {
            InitializeComponent();
            borrar2();
            imageId();
        }

        private void imageId()
        {            
            if (Map.id_pin == 0)
            {
                pushpin.Source = new BitmapImage(new Uri("/Images/animal_pin.png", UriKind.RelativeOrAbsolute));
            }
            else if (Map.id_pin == 1)
            {
                pushpin.Source = new BitmapImage(new Uri("/Images/atraccioness_pin.png", UriKind.RelativeOrAbsolute));
            }
            else if (Map.id_pin == 2)
            {
                pushpin.Source = new BitmapImage(new Uri("/Images/servicios_pin.png", UriKind.RelativeOrAbsolute));
            }
        }


        public void borrar2()
        {
            box.Opacity = 0;
            pushPinHeader.Opacity = 0;
        }

        public string id { get; set; }
        public string latitud { get; set; }
        public string longitud { get; set; }
        public string lugar { get; set; }

        private void pushPinHeader_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            box.Opacity = 100;
            pushPinHeader.Opacity = 100;
            pushPinHeader.Text = lugar;
            pushpin.Opacity = 0;
        }

    }
}

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

namespace ZOOGDL
{
    public partial class Servicios : PhoneApplicationPage
    {
        public static int id_servicios;
        public static bool isClick;
        public Servicios()
        {
            InitializeComponent();
        }

        private void Sanitarios_Click(object sender, RoutedEventArgs e)
        {
            id_servicios = 0;
            isClick = true;
            this.NavigationService.Navigate(new Uri("/Map.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Restaurantes_Click(object sender, RoutedEventArgs e)
        {
            id_servicios = 1;
            isClick = true;
            this.NavigationService.Navigate(new Uri("/Map.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Cajero_Click(object sender, RoutedEventArgs e)
        {
            id_servicios = 2;
            isClick = true;
            this.NavigationService.Navigate(new Uri("/Map.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Recuerdos_Click(object sender, RoutedEventArgs e)
        {
            id_servicios = 3;
            isClick = true;
            this.NavigationService.Navigate(new Uri("/Map.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Telefonos_Click(object sender, RoutedEventArgs e)
        {
            id_servicios = 4;
            isClick = true;
            this.NavigationService.Navigate(new Uri("/Map.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Auxilios_Click(object sender, RoutedEventArgs e)
        {
            id_servicios = 5;
            isClick = true;
            this.NavigationService.Navigate(new Uri("/Map.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Informacion_Click(object sender, RoutedEventArgs e)
        {
            id_servicios = 6;
            isClick = true;
            this.NavigationService.Navigate(new Uri("/Map.xaml", UriKind.RelativeOrAbsolute));
        }

        
    }
}

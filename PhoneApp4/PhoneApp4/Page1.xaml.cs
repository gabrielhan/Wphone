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

namespace PhoneApp4
{
    public partial class Page1 : PhoneApplicationPage
    {


        Polyline jolidessin;



        public Page1()
        {
            InitializeComponent();


        }

        protected override void   OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            jolidessin = new Polyline();
            (App.Current as App).drawlist.Add(jolidessin);
            jolidessin.Stroke = this.Resources["PhoneForegroundBrush"] as Brush;
            jolidessin.StrokeThickness = (double)5;
 	        base.OnNavigatedTo(e);
        }

        protected override void OnManipulationStarted(ManipulationStartedEventArgs e)
        {
            e.ManipulationContainer = canvy;
            base.OnManipulationStarted(e);
            canvy.Children.Add(jolidessin);
        }

        protected override void OnManipulationDelta(ManipulationDeltaEventArgs e)
        {
            UIElement element = e.OriginalSource as UIElement;
            Point translation = e.DeltaManipulation.Translation;
            Canvas.SetLeft(element, Canvas.GetLeft(element) + translation.X);
            Canvas.SetTop(element, Canvas.GetTop(element) + translation.Y);
            jolidessin.Points.Add(new Point(Canvas.GetLeft(element) + translation.X, Canvas.GetTop(element) + translation.Y));
            e.Handled = true;
            base.OnManipulationDelta(e);
        }

        protected override void OnManipulationCompleted(ManipulationCompletedEventArgs e)
        {
            string dest = "/page2.xaml";
            canvy.Children.Remove(jolidessin);
            this.NavigationService.Navigate(new Uri(dest, UriKind.Relative));
            base.OnManipulationCompleted(e);
        }

    }
}
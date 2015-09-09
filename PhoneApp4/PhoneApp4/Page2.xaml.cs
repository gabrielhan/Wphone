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
    public partial class Page2 : PhoneApplicationPage
    {
        public Page2()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            foreach (Polyline drawing in (App.Current as App).drawlist)
            {
                canvy.Children.Add(drawing);
            }
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            canvy.Children.Clear();
            base.OnNavigatedFrom(e);
        }

        protected override void OnManipulationStarted(ManipulationStartedEventArgs e)
        {
            e.ManipulationContainer = canvy;
            base.OnManipulationStarted(e);
        }

        protected override void OnManipulationDelta(ManipulationDeltaEventArgs e)
        {
            UIElement element = e.OriginalSource as UIElement;
            Point translation = e.DeltaManipulation.Translation;
            Canvas.SetLeft(element, Canvas.GetLeft(element) + translation.X);
            Canvas.SetTop(element, Canvas.GetTop(element) + translation.Y);
            e.Handled = true;
            base.OnManipulationDelta(e);
        }
    }
}
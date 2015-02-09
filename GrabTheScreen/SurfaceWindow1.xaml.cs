using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Surface;
using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;
using Microsoft.Surface.Presentation.Input;

namespace GrabTheScreen
{
    /// <summary>
    /// Interaction logic for SurfaceWindow1.xaml
    /// </summary>
    public partial class SurfaceWindow1 : SurfaceWindow
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public SurfaceWindow1()
        {
            InitializeComponent();

            // Add handlers for window availability events
            AddWindowAvailabilityHandlers();
        }

        /// <summary>
        /// Occurs when the window is about to close. 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            // Remove handlers for window availability events
            RemoveWindowAvailabilityHandlers();
        }

        /// <summary>
        /// Adds handlers for window availability events.
        /// </summary>
        private void AddWindowAvailabilityHandlers()
        {
            // Subscribe to surface window availability events
            ApplicationServices.WindowInteractive += OnWindowInteractive;
            ApplicationServices.WindowNoninteractive += OnWindowNoninteractive;
            ApplicationServices.WindowUnavailable += OnWindowUnavailable;
        }

        /// <summary>
        /// Removes handlers for window availability events.
        /// </summary>
        private void RemoveWindowAvailabilityHandlers()
        {
            // Unsubscribe from surface window availability events
            ApplicationServices.WindowInteractive -= OnWindowInteractive;
            ApplicationServices.WindowNoninteractive -= OnWindowNoninteractive;
            ApplicationServices.WindowUnavailable -= OnWindowUnavailable;
        }

        /// <summary>
        /// This is called when the user can interact with the application's window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowInteractive(object sender, EventArgs e)
        {
            //TODO: enable audio, animations here
        }

        /// <summary>
        /// This is called when the user can see but not interact with the application's window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowNoninteractive(object sender, EventArgs e)
        {
            //TODO: Disable audio here if it is enabled

            //TODO: optionally enable animations here
        }

        /// <summary>
        /// This is called when the application's window is not visible or interactive.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowUnavailable(object sender, EventArgs e)
        {
            //TODO: disable audio, animations here
        }

        private void SurfaceWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Auto auto = new Auto();
            auto.setModel("Audi A3 Sportback");
            auto.setModelDescription("Ambition 2.0 TDI Clean Diesel 6-Gang");
            auto.setPrice("30.500,00 EUR");
            auto.setSource("Resources/small_audi.jpg");


            Uri uri = new Uri(auto.getSource(), UriKind.Relative);
            BitmapImage imageBitmap = new BitmapImage(uri);
            Image thumbnail = new Image();
            thumbnail.Source = imageBitmap;
            thumbnail_car.Children.Add(thumbnail);
        }

        private void btn_grabIt_Click(object sender, RoutedEventArgs e)
        {
            placeholder_smartphone.Children.Clear();

            Image newImage = new Image();
            newImage.Source = konfig_auto.Source;

            Thickness margin = newImage.Margin;
            margin.Left = 20;
            margin.Right = 20;
            newImage.Margin = margin;

            placeholder_smartphone.Children.Add(newImage);
        }

        public void GRID_Placeholder(object sender, TouchEventArgs e)
        {
            e.TouchDevice.GetIsTagRecognized();
            //e.TouchDevice.GetTagData().Value;
        }

        private void OnVisualizationAdded(object sender, TagVisualizerEventArgs e)
        {
            CameraVisualization camera = (CameraVisualization)e.TagVisualization;
            switch (camera.VisualizedTag.Value)
            {
                case 1:
                    camera.CameraModel.Content = "ABCDEFGHIJKLM";
                    camera.myEllipse.Fill = SurfaceColors.Accent1Brush;
                    break;
                case 2:
                    camera.CameraModel.Content = "XXXXXXXXXXXXXXXXXXXX";
                    camera.myEllipse.Fill = SurfaceColors.Accent2Brush;
                    break;
                case 3:
                    camera.CameraModel.Content = "AAAAAAAAAAAAAA";
                    camera.myEllipse.Fill = SurfaceColors.Accent3Brush;
                    break;
                case 4:
                    camera.CameraModel.Content = "RIRIRIRIIRIRIRIRIRIRI";
                    camera.myEllipse.Fill = SurfaceColors.Accent4Brush;
                    break;
                default:
                    camera.CameraModel.Content = "UNKNOWN MODEL";
                    camera.myEllipse.Fill = SurfaceColors.ControlAccentBrush;
                    break;
            }
        }
    }
}
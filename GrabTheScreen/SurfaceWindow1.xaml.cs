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

        // Erzeugung der Auto-Informationen und Autobild im rechten Block
        private void SurfaceWindow_Loaded(object sender, RoutedEventArgs e)
        { 
            // Auto Objekt erzeugen
            Auto auto = new Auto();
            auto.setModel("Audi A3 Sportback");
            auto.setModelDescription("Ambition 2.0 TDI Clean Diesel 6-Gang");
            auto.setPrice("30.500,00 EUR");
            auto.setSource("Resources/small_audi.jpg");
            //car_information.

            // Miniaturbild (thumbnail) erzeugen
            Uri uri = new Uri(auto.getSource(), UriKind.Relative);
            BitmapImage imageBitmap = new BitmapImage(uri);
            Image thumbnail = new Image();
            thumbnail.Source = imageBitmap;
            thumbnail_car.Children.Add(thumbnail);         
        }

        // Methode, die aufgerufen wird bei Klick auf "grab it" Button
        private void btn_grabIt_Click(object sender, RoutedEventArgs e)
        {
            // damit Miniatur-Bild erst zur Laufzeit angezeigt wird
            placeholder_smartphone.Children.Clear();

            // Erstellen des Vizualizer's
            TagVisualizer visualizer = new TagVisualizer();
            visualizer.Name = "MyTagVisualizer";

            // Visualization Definitionen
            TagVisualizationDefinition tagDefinition = new TagVisualizationDefinition();

            // Tag Value 0x1 - wichtig für Input Simulator
            tagDefinition.Value = "0x1";
            tagDefinition.Source = new Uri("CameraVisualization.xaml", UriKind.Relative);
            tagDefinition.LostTagTimeout = 2000;
            tagDefinition.MaxCount = 2;
            tagDefinition.OrientationOffsetFromTag = 0;
            tagDefinition.TagRemovedBehavior = TagRemovedBehavior.Fade;
            tagDefinition.UsesTagOrientation = true;

            // Definitionen dem Visualizer hinzufügen
            visualizer.Definitions.Add(tagDefinition);

            visualizer.VisualizationAdded += OnVisualizationAdded;

            // Miniaturbild auf gts-Fläche
            Image newImage = new Image();
            newImage.Source = konfig_auto.Source;

            Thickness margin = newImage.Margin;
            margin.Left = 20;
            margin.Right = 20;
            newImage.Margin = margin;

            // zur Laufzeit Bild und Visualizer erzeugen
            placeholder_smartphone.Children.Add(newImage);
            placeholder_smartphone.Children.Add(visualizer);
        }

        // erzeugt Tag-Bereich
        private void OnVisualizationAdded(object sender, TagVisualizerEventArgs e)
        {
            CameraVisualization camera = (CameraVisualization)e.TagVisualization;
            camera.GRABIT.Content = "Auflagefläche des Smartphones";
            camera.myRectangle.Fill = SurfaceColors.Accent1Brush;

            // HIER Auto Objekt (Miniatur + Infos) bauen und für Versand vorbereiten
            // https://msdn.microsoft.com/de-de/library/system.web.script.serialization.javascriptserializer(v=vs.110).aspx
        }
    }
}
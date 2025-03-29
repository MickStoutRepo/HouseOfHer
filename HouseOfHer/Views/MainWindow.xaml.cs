// Bishmillah //

using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using HouseOfHer.Views;
using Newtonsoft.Json;

namespace HouseOfHer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadAppointments();
            UpdateDateView();
            Console.WriteLine("MainWindow initialized.");
        }
        
        private void LoadAppointments()
        {
            string json = File.ReadAllText("agenda.json");
            Appointments = JsonConvert.DeserializeObject<List<Appointment>>(json);
        }
        
        private void SaveAppointments()
        {
            string json = JsonConvert.SerializeObject(Appointments, Formatting.Indented);
            File.WriteAllText("agenda.json", json);
        }
        
        private void UpdateDateView()
        {
            DateTime selectedDate = CalendarControl.SelectedDate ?? DateTime.Now;
            DateViewTextBlock.Text = selectedDate.ToString("d");

            var appointment = Appointments.FirstOrDefault(a => a.Date.Date == selectedDate.Date);
            if (appointment != null)
            {
                var descriptions = appointment.Description
                    .Where(d => !string.IsNullOrEmpty(d.Value))
                    .Select(d => $"{d.Key}: {d.Value}");

                DateViewContentBlock.Text = string.Join("\n", descriptions);
            }
            else
            {
                DateViewContentBlock.Text = "Geen afspraken voor deze datum.";
            }
        }
        
        public class Appointment
        {
            public DateTime Date { get; set; }
            public Dictionary<string, string> Description { get; set; }
        }

        public List<Appointment> Appointments { get; set; }
        
        private void myCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadAppointments();
            UpdateDateView();
        }
        
        static class Navigator
        {
            private static ContentControl MainContent => ((MainWindow)Application.Current.MainWindow).MainContent;

            public static void Navigate(UIElement content)
            {
                MainContent.Content = content;
            }
        }
        
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        
        private void Instellingen_Click(object sender, RoutedEventArgs e)
        {
            Navigator.Navigate(new Instellingen());
        }

        private void Boodschappen(object sender, RoutedEventArgs e)
        {
            Navigator.Navigate(new Boodschappen());
        }
        
        private void Maaltijden(object sender, RoutedEventArgs e)
        {
            Navigator.Navigate(new Maaltijden());
        }
        
        private void Agenda(object sender, RoutedEventArgs e)
        {
            Navigator.Navigate(new Agenda());
        }
        
        private void Minimaliseren_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).WindowState = WindowState.Minimized;
        }
        
        private void Afsluiten_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Wil je de applicatie afsluiten?", "Bevestiging",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }
        
    }
}
// Elhamdulillah //
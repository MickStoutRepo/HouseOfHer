// Bishmillah //
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace HouseOfHer.Views;

public partial class Instellingen : ContentControl
{
    public Instellingen()
    {
        InitializeComponent();
    }

    private void TextBox_1(object sender, TextChangedEventArgs e)
    {
        if (!TextBox1.Text.StartsWith("https://"))
        {
            TextBox1.Text = "https://";
            TextBox1.CaretIndex = TextBox1.Text.Length;
        }
    }

    private void Submit_Click(object sender, RoutedEventArgs e)
    {
        string baseUrl = TextBox1.Text;
        string username = UsernameTextBox.Text;
        string password = PasswordBox.Password;

        string envFilePath = Path.Combine(Directory.GetCurrentDirectory(), ".env");

        var envVariables = new[]
        {
            $"BASEURL={baseUrl}",
            $"USERNAME={username}",
            $"PASSWORD={password}"
        };

        try
        {
            // Controleer of het bestand bestaat, zo niet, maak het aan
            if (!File.Exists(envFilePath))
            {
                File.Create(envFilePath).Dispose();
            }

            // Schrijf de variabelen naar het .env bestand
            File.WriteAllLines(envFilePath, envVariables);

            // Aangepaste stille MessageBox
            CustomMessageBox.Show("Instellingen opgeslagen!", "Succes");
        }
        catch (Exception ex)
        {
            CustomMessageBox.Show($"Er is een fout opgetreden: {ex.Message}", "Fout");
        }
    }

// Aangepaste MessageBox zonder geluid
    public static class CustomMessageBox
    {
        public static void Show(string message, string title)
        {
            Window messageBox = new Window
            {
                Title = title,
                Content = new TextBlock { Text = message, Margin = new Thickness(20) },
                SizeToContent = SizeToContent.WidthAndHeight,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                WindowStyle = WindowStyle.ToolWindow
            };
            messageBox.ShowDialog();
        }
    }
}

// Elhamdulillah //
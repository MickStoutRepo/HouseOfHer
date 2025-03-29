// Bishmillah //

using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace HouseOfHer.Views
{
    public partial class Maaltijden : ContentControl
    {
        private const string FilePath = "maaltijden.json";
        private Dictionary<string, string> maaltijdenIngrediënten = new Dictionary<string, string>();

        public Maaltijden()
        {
            InitializeComponent();
            LoadMaaltijden();
        }

        private void LoadMaaltijden()
        {
            if (File.Exists(FilePath))
            {
                string json = File.ReadAllText(FilePath);
                maaltijdenIngrediënten = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
                foreach (var maaltijd in maaltijdenIngrediënten.Keys)
                {
                    Maaltijdenlijst.Items.Add(new ListBoxItem { Content = maaltijd });
                }
            }
            else
            {
                // Voeg standaard maaltijden toe als het bestand niet bestaat
                maaltijdenIngrediënten = new Dictionary<string, string>
                {
                    { "Aardappel Tortilla", "Ingrediënten: Aardappelen, Eieren, Ui, Olijfolie, Zout" },
                    { "Surimi en Mihoen", "Ingrediënten: Surimi, Mihoen, Groenten, Sojasaus, Sesamolie" },
                    { "Vis Pasta", "Ingrediënten: Pasta, Vis, Tomatensaus, Knoflook, Olijfolie" }
                };
                SaveMaaltijden();
                LoadMaaltijden();
            }
        }

        private void SaveMaaltijden()
        {
            string json = JsonSerializer.Serialize(maaltijdenIngrediënten);
            File.WriteAllText(FilePath, json);
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Maaltijdenlijst.SelectedItem != null)
            {
                string selectedMaaltijd = ((ListBoxItem)Maaltijdenlijst.SelectedItem).Content.ToString();
                if (maaltijdenIngrediënten.ContainsKey(selectedMaaltijd))
                {
                    MaaltijdenControl.Content = maaltijdenIngrediënten[selectedMaaltijd];
                }
            }
        }

        private void VoegMaaltijdToe_Click(object sender, RoutedEventArgs e)
        {
            string nieuweMaaltijdNaam = NieuweMaaltijdNaam.Text;
            string nieuweMaaltijdIngrediënten = NieuweMaaltijdIngrediënten.Text;

            if (!string.IsNullOrWhiteSpace(nieuweMaaltijdNaam) && !string.IsNullOrWhiteSpace(nieuweMaaltijdIngrediënten))
            {
                if (!maaltijdenIngrediënten.ContainsKey(nieuweMaaltijdNaam))
                {
                    maaltijdenIngrediënten.Add(nieuweMaaltijdNaam, nieuweMaaltijdIngrediënten);
                    Maaltijdenlijst.Items.Add(new ListBoxItem { Content = nieuweMaaltijdNaam });
                    SaveMaaltijden();
                    NieuweMaaltijdNaam.Clear();
                    NieuweMaaltijdIngrediënten.Clear();
                }
                else
                {
                    MessageBox.Show("Deze maaltijd bestaat al.");
                }
            }
            else
            {
                MessageBox.Show("Vul zowel de naam als de ingrediënten in.");
            }
        }
    }
}
// Bishmillah //
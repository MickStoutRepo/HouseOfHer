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
        private Dictionary<string, Dictionary<string, string>> maaltijdenIngrediënten = new Dictionary<string, Dictionary<string, string>>();

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
                maaltijdenIngrediënten = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(json);
                foreach (var maaltijd in maaltijdenIngrediënten.Keys)
                {
                    Maaltijdenlijst.Items.Add(new ListBoxItem { Content = maaltijd });
                }
            }
            else
            {
                // Voeg standaard maaltijden toe als het bestand niet bestaat
                maaltijdenIngrediënten = new Dictionary<string, Dictionary<string, string>>
                {
                    { "Aardappel Tortilla", new Dictionary<string, string>
                        {
                            { "1", "Aardappelen" },
                            { "2", "Eieren" },
                            { "3", "Ui" },
                            { "4", "Olijfolie" },
                            { "5", "Zout" }
                        }
                    },
                    { "Surimi en Mihoen", new Dictionary<string, string>
                        {
                            { "1", "Surimi" },
                            { "2", "Mihoen" },
                            { "3", "Groenten" },
                            { "4", "Sojasaus" },
                            { "5", "Sesamolie" }
                        }
                    },
                    { "Vis Pasta", new Dictionary<string, string>
                        {
                            { "1", "Pasta" },
                            { "2", "Vis" },
                            { "3", "Tomatensaus" },
                            { "4", "Knoflook" },
                            { "5", "Olijfolie" }
                        }
                    }
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
                    var ingrediënten = maaltijdenIngrediënten[selectedMaaltijd];
                    MaaltijdViewContentBlock.Text = string.Join(Environment.NewLine, ingrediënten.Values);
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
                    var ingrediëntenArray = nieuweMaaltijdIngrediënten.Split(',');
                    var ingrediëntenDict = new Dictionary<string, string>();
                    for (int i = 0; i < ingrediëntenArray.Length; i++)
                    {
                        ingrediëntenDict[(i + 1).ToString()] = ingrediëntenArray[i].Trim();
                    }

                    maaltijdenIngrediënten.Add(nieuweMaaltijdNaam, ingrediëntenDict);
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
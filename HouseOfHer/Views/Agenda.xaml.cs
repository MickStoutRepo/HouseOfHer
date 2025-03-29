// Bishmillah //

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using Newtonsoft.Json;
using MessageBox = System.Windows.MessageBox;

namespace HouseOfHer.Views
{
    public partial class Agenda : ContentControl
    {
        public Agenda()
        {
            InitializeComponent();
            FillTimeComboBox();
        }

        private void FillTimeComboBox()
        {
            for (int hour = 0; hour < 24; hour++)
            {
                timeComboBox.Items.Add(hour.ToString("00") + ":00");
            }
        }

        private void AddToJson_Click(object sender, RoutedEventArgs e)
        {
            string date = datePicker.SelectedDate?.ToString("yyyy-MM-dd") ?? string.Empty;
            string time = timeComboBox.SelectedItem?.ToString() ?? string.Empty;
            string description = descriptionTextBox.Text;

            if (!string.IsNullOrEmpty(date) && !string.IsNullOrEmpty(time) && !string.IsNullOrEmpty(description))
            {
                string jsonFilePath = "agenda.json";
                List<AgendaItem> agendaItems = new List<AgendaItem>();

                if (File.Exists(jsonFilePath))
                {
                    string existingJson = File.ReadAllText(jsonFilePath);
                    agendaItems = JsonConvert.DeserializeObject<List<AgendaItem>>(existingJson) ?? new List<AgendaItem>();
                }

                AgendaItem existingItem = agendaItems.Find(item => item.Date == date);
                if (existingItem != null)
                {
                    existingItem.Description[time] = description;
                }
                else
                {
                    AgendaItem newItem = new AgendaItem
                    {
                        Date = date,
                        Description = new Dictionary<string, string>
                        {
                            { time, description }
                        }
                    };
                    agendaItems.Add(newItem);
                }

                string newJson = JsonConvert.SerializeObject(agendaItems, Formatting.Indented);
                File.WriteAllText(jsonFilePath, newJson);
                MessageBox.Show("Item toegevoegd aan agenda.");
            }
            else
            {
                MessageBox.Show("Vul alle velden in.");
            }
        }
    }

    public class AgendaItem
    {
        public string Date { get; set; }
        public Dictionary<string, string> Description { get; set; }
    }
}

// Bishmillah //
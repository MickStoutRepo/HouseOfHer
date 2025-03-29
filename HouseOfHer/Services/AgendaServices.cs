using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using HouseOfHer.Models;
using Newtonsoft.Json.Linq;

namespace HouseOfHer.Services
{

    public class AgendaService
    {
        private string filePath = "agenda.json";
        
        public static List<AgendaItem> LoadAgenda(string filePath)
        {
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<AgendaItem>>(json);
        }
        
        public void UpdateAgenda(string date, string time, string text)
        {
            string json = File.ReadAllText(filePath);
            JArray agenda = JArray.Parse(json);

            bool dateExists = false;
            bool timeExists = false;

            foreach (JObject day in agenda)
            {
                if (day.ContainsKey(date))
                {
                    dateExists = true;
                    JObject daySchedule = (JObject)day[date];

                    if (daySchedule.ContainsKey(time))
                    {
                        timeExists = true;
                        daySchedule[time] = text; // Overschrijf bestaande tijd
                    }
                    else
                    {
                        daySchedule.Add(time, text); // Voeg nieuwe tijd toe
                    }
                }
            }

            if (!dateExists)
            {
                JObject newDay = new JObject();
                newDay.Add(time, text);
                JObject newDate = new JObject();
                newDate.Add(date, newDay);
                agenda.Add(newDate);
            }

            File.WriteAllText(filePath, agenda.ToString());
        }
    }
}
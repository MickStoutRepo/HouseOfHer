using System;
using System.ComponentModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using HouseOfHer.Services;

namespace HouseOfHer.Viewmodels
{


    public class AgendaViewModel : INotifyPropertyChanged
    {
        private readonly AgendaService _agendaService;
        private string _selectedDate;
        private string _selectedTime;
        private string _text;

        public AgendaViewModel()
        {
            _agendaService = new AgendaService();
            SaveCommand = new RelayCommand(Save);
        }

        public string SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }

        public string SelectedTime
        {
            get => _selectedTime;
            set
            {
                _selectedTime = value;
                OnPropertyChanged(nameof(SelectedTime));
            }
        }

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                OnPropertyChanged(nameof(Text));
            }
        }

        public ICommand SaveCommand { get; }

        private void Save()
        {
            _agendaService.UpdateAgenda(SelectedDate, SelectedTime, Text);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public List<string> Dates { get; } = new List<string> { "2025-03-31", "2025-04-01" };
        public List<string> Times { get; } = new List<string> { "08:00", "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00" };
        
    
    }
}
using System.Collections.Generic;
using System.Collections.ObjectModel;
using HouseOfHer.ViewModels;
using HouseOfHer.Models;
using HouseOfHer.Services;
namespace HouseOfHer.ViewModels;

public class MainViewModel : BaseViewModel
{
    private ObservableCollection<AgendaItem> _agendaItems;
    public ObservableCollection<AgendaItem> AgendaItems
    {
        get { return _agendaItems; }
        set
        {
            _agendaItems = value;
            OnPropertyChanged();
        }
    }

    public MainViewModel()
    {
        var agendaItems = AgendaService.LoadAgenda("agenda.json");
        AgendaItems = new ObservableCollection<AgendaItem>(agendaItems);
    }
}
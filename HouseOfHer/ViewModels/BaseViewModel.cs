// Bishmillah //
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HouseOfHer.ViewModels
{
    public class BaseViewModel :
        INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

// Elhamdulillah //
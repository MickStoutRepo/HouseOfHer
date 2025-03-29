// Bishmillah //

using System.Windows;
using System.Windows.Controls;

namespace HouseOfHer.Views;

public partial class Boodschappen : ContentControl
{
    public Boodschappen()
    {
        InitializeComponent();
    }
    
    private void AddToList(object sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(BoodschappenInvoer.Text))
        {
            inputListBox.Items.Add(BoodschappenInvoer.Text);
            BoodschappenInvoer.Clear();
        }
    }
    
    private void RemoveSelected(object sender, RoutedEventArgs e)
    {
        if (inputListBox.SelectedItem != null)
        {
            inputListBox.Items.Remove(inputListBox.SelectedItem);
        }
    }
    
}
// Bishmillah //
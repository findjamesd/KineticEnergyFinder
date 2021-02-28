using System.Windows;
using System.Windows.Controls;

namespace KineticEnergyFinder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    enum UnitsOfMass
    {
        K
    }
    public partial class MainWindow : Window
    {
        KineticEnergyViewModel keViewModel = new KineticEnergyViewModel();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = keViewModel;

        }

        private void CalcKineticEnergy_Click(object sender, RoutedEventArgs e)
        {
            keViewModel.Calc();
        }

        private void MassUnits_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem ComboItem = (ComboBoxItem)MassUnits.SelectedItem;
            string name = ComboItem.Name;
            keViewModel.MassUnitChanged(name);
        }
        private void VelocityUnits_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem ComboItem = (ComboBoxItem)VelocityUnits.SelectedItem;
            string name = ComboItem.Name;
            keViewModel.VelocityUnitChanged(name);
        }

        private void KineticEnergyUnit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem ComboItem = (ComboBoxItem)KineticEnergyUnit.SelectedItem;
            string name = ComboItem.Name;
            keViewModel.KEUnitChanged(name);
        }

        private void EnteredVelocity_LostFocus(object sender, RoutedEventArgs e)
        {

            keViewModel.SavePermanentVariable("velocity", EnteredVelocity.Text);
        }

        private void EnteredMass_LostFocus(object sender, RoutedEventArgs e)
        {
            keViewModel.SavePermanentVariable("mass", EnteredMass.Text);
        }
    }
}

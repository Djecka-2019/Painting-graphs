using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Painting_graphs.Graph;

namespace Painting_graphs;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        MethodOfGraphInput.SelectionChanged += MethodOfGraphInput_SelectionChanged;
        MethodOfColorInput.SelectionChanged += MethodOfGraphPaint_SelectionChanged;
        ColorLimitCheckbox.Checked += ColorLimitCheckbox_Checked;
        ColorLimitCheckbox.Unchecked += ColorLimitCheckbox_Unchecked;
    }
    
    private void MethodOfGraphInput_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var comboBox = sender as ComboBox;
    }
    
    private void MethodOfGraphPaint_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var comboBox = sender as ComboBox;
    }
    
    private void ColorLimitCheckbox_Checked(object sender, RoutedEventArgs e)
    {
        var checkBox = sender as CheckBox;
        ColorInput.IsEnabled = true;
    }
    
    private void ColorLimitCheckbox_Unchecked(object sender, RoutedEventArgs e)
    {
        var checkBox = sender as CheckBox;
        ColorInput.IsEnabled = false;
    }
    
    private void StartButton_Click(object sender, RoutedEventArgs e)
    {
        var methodOfGraphInput = MethodOfGraphInput.SelectedIndex;
        var methodOfColorInput = MethodOfColorInput.SelectedIndex;
        var colorLimit = ColorLimitCheckbox.IsChecked;
        var colorInput = ColorInput.Text;
        if(methodOfGraphInput == 0)
        {
            
        }
    }
}
using System.Windows;
using System.Windows.Controls;

namespace Painting_graphs;
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
    
    private void MethodOfGraphInput_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (MethodOfGraphInput.SelectedIndex == 1)
        {
            EdgeInput.IsEnabled = true;
        }
        else
        {
            EdgeInput.IsEnabled = false;
        }
    }

    private void colorBasedOnSelectedMethod(Graph g)
    {
        if (MethodOfColorInput.SelectedIndex == 0)
        {
            List<int> result = g.GreedyPaint();
            GraphForm form = new GraphForm(g, result);
            form.ShowDialog();
        }
        else
        {
            List<int> result = g.ColorGraphWithMrvAndHeuristic();
            GraphForm form = new GraphForm(g, result);
            form.ShowDialog();
        }
    }
    
    private void colorIfCheckboxChecked(Graph g)
    {
        if (ColorLimitCheckbox.IsChecked == true)
        {
            int colorLimit = int.Parse(ColorInput.Text);
            if (colorLimit < g.FindChromaticNumberOfGraph())
            {
                MessageBox.Show("Введена кількість кольорів менша за хроматичне число графа (", + g.FindChromaticNumberOfGraph() + ")");
                return;
            }
            List<int> result = g.GreedyPaint();
            GraphForm form = new GraphForm(g, result);
            form.ShowDialog();
        }
        else
        {
            colorBasedOnSelectedMethod(g);
        }
    }
    
    private void StartButton_Click(object sender, RoutedEventArgs e)
    {
        int size = int.Parse(VertexInput.Text);
        var graphInput = GraphInput.Text;
        List<List<int>> numbers = graphInput.Split('\n')
            .Select(line => line.Split(' ')
                .Select(int.Parse)
                .ToList())
            .ToList();
        if (MethodOfGraphInput.SelectedIndex == 0)
        {
            Graph g = new Graph(numbers, size);
            colorIfCheckboxChecked(g);
        }
        else
        {
            int amountOfPairs = int.Parse(EdgeInput.Text);
            List<Tuple<int, int> > pairs = new List<Tuple<int, int>>();
            for (int i = 0; i < amountOfPairs; i++)
            {
                pairs.Add(new Tuple<int, int>(numbers[i][0], numbers[i][1]));
            }
            Graph g = new Graph(pairs, size);
            colorIfCheckboxChecked(g);
        }
    }
}
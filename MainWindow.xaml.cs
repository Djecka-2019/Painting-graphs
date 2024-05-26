using System.Windows;
using System.Windows.Controls;

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
        int size = int.Parse(VertexInput.Text);
        var graphInput = GraphInput.Text;
        List<List<int>> numbers = graphInput.Split('\n')
            .Select(line => line.Split(' ')
                .Select(int.Parse)
                .ToList())
            .ToList();
        if (MethodOfGraphInput.SelectedIndex == 0)
        {
            for(int i = 0 ; i < size ; i++)
            {
                for(int j = 0 ; j < size ; j++)
                {
                    Console.Write(numbers[i][j] + " ");
                }
                Console.WriteLine();
            }
            Graph g = new Graph(numbers, size);
            if(ColorLimitCheckbox.IsChecked == true)
            {
                
            }
            else
            {
                if (MethodOfColorInput.SelectedIndex == 0)
                {
                    List<int> result = g.GreedyPaint();
                    for (int i = 0; i < size; i++)
                    {
                        Console.WriteLine(i + " : " + result[i]);
                    }
                }
                else
                {
                    List<int> result = g.ColorGraphWithMrvAndHeuristic();
                    for (int i = 0; i < size; i++)
                    {
                        Console.WriteLine(i + " : " + result[i]);
                    }
                }
            }
        }
        else
        {
            int amountOfPairs = int.Parse(EdgeInput.Text);
            List<Tuple<int, int> > pairs = new List<Tuple<int, int>>();
            for (int i = 0; i < amountOfPairs; i++)
            {
                pairs.Add(new Tuple<int, int>(numbers[i][0], numbers[i][1]));
            }
            Console.WriteLine(size);
            for (int i = 0; i < amountOfPairs; i++)
            {
                Console.WriteLine(pairs[i].Item1 + " " + pairs[i].Item2);
            }
            Graph g = new Graph(pairs, size);
            if (ColorLimitCheckbox.IsChecked == true)
            {
                int colorLimit = int.Parse(ColorInput.Text);
                if (colorLimit < g.FindChromaticNumberOfGraph())
                {
                    MessageBox.Show("Введена кількість кольорів менша за хроматичне число графа (", + g.FindChromaticNumberOfGraph() + ")");
                    return;
                }
                Console.WriteLine(g.GreedyPaint());
            }
            else
            {
                if (MethodOfColorInput.SelectedIndex == 0)
                {
                    List<int> result = g.GreedyPaint();
                    for (int i = 0; i < size; i++)
                    {
                        Console.WriteLine(i + " : " + result[i]);
                    }
                }
                else
                {
                    List<int> result = g.ColorGraphWithMrvAndHeuristic();
                    for (int i = 0; i < size; i++)
                    {
                        Console.WriteLine(i + " : " + result[i]);
                    }
                }
            }
        }
    }
}
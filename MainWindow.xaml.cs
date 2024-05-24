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
        var graphInput = GraphInput.Text;
        List<int> numbers = graphInput.Split(' ').Select(int.Parse).ToList();
        if (MethodOfGraphInput.SelectedIndex == 0)
        {
            int size = numbers[0];
            List<List<int>> graph = new List<List<int>>();
            for (int i = 0; i < size; i++)
            {
                graph.Add(new List<int>());
                for (int j = 0; j < size; j++)
                {
                    graph[i].Add(numbers[i * size + j + 1]);
                }
            }
            for(int i = 0 ; i < size ; i++)
            {
                for(int j = 0 ; j < size ; j++)
                {
                    Console.Write(graph[i][j] + " ");
                }
                Console.WriteLine();
            }
            Graph g = new Graph(graph, size);
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
                List<int> result = g.ColorGraphWithMRVAndHeuristic();
                for (int i = 0; i < size; i++)
                {
                    Console.WriteLine(i + " : " + result[i]);
                }
            }
        }
        else
        {
            int size = numbers[0];
            numbers.RemoveAt(0);
            int amountOfPairs = numbers[0];
            numbers.RemoveAt(0);
            List<Tuple<int, int> > pairs = new List<Tuple<int, int>>();
            for (int i = 0; i < amountOfPairs; i++)
            {
                pairs.Add(new Tuple<int, int>(numbers[i * 2], numbers[i * 2 + 1]));
            }
            Console.WriteLine(size);
            for (int i = 0; i < amountOfPairs; i++)
            {
                Console.WriteLine(pairs[i].Item1 + " " + pairs[i].Item2);
            }
            Graph g = new Graph(pairs, size);
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
                List<int> result = g.ColorGraphWithMRVAndHeuristic();
                for (int i = 0; i < size; i++)
                {
                    Console.WriteLine(i + " : " + result[i]);
                }
            }
        }
    }
}
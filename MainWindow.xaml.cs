using System.Windows;
using System.IO;
using System.Windows.Controls;
using MessageBox = System.Windows.MessageBox;

namespace Painting_graphs;
public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
        MethodOfGraphInput.SelectionChanged += MethodOfGraphInput_SelectionChanged;
        ColorLimitCheckbox.Checked += ColorLimitCheckbox_Checked;
        ColorLimitCheckbox.Unchecked += ColorLimitCheckbox_Unchecked;
    }
    
    private void ColorLimitCheckbox_Checked(object sender, RoutedEventArgs e)
    {
        ColorInput.IsEnabled = true;
    }
    
    private void ColorLimitCheckbox_Unchecked(object sender, RoutedEventArgs e)
    {
        ColorInput.IsEnabled = false;
    }
    
    private void MethodOfGraphInput_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        EdgeInput.IsEnabled = MethodOfGraphInput.SelectedIndex == 1;
    }

    private void colorBasedOnSelectedMethod(Graph g)
    {
        List<int> result = new List<int>();
        if (MethodOfColorInput.SelectedIndex == 0)
        {
            result = g.GreedyPaint();
        }
        else if(MethodOfColorInput.SelectedIndex == 1)
        {
            result = g.ColorGraphWithMrv();
        }
        else if(MethodOfColorInput.SelectedIndex == 2)
        {
            result = g.ColorGraphWithHeuristicDegree();
        }
        if(result.Count == 0)
        {
            MessageBox.Show("Граф неможливо розфарбувати обраним методом");
            return;
        }
        string colorData = string.Join(Environment.NewLine, result);
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string filePath = Path.Combine(desktopPath, "colors.txt");
        File.WriteAllText(filePath, colorData);
        GraphForm form = new GraphForm(g, result);
        form.ShowDialog();
    }
    
    private void ColorIfCheckboxChecked(Graph g)
    {
        if (ColorLimitCheckbox.IsChecked == true)
        {
            if(!int.TryParse(ColorInput.Text, out int colorLimit) || colorLimit <= 0)
            {
                MessageBox.Show("Введіть коректну кількість кольорів");
                return;
            }
            if (colorLimit < g.FindChromaticNumberOfGraph())
            {
                MessageBox.Show("Введена кількість кольорів менша за хроматичне число графа (" + g.FindChromaticNumberOfGraph() + ")");
                return;
            }
            colorBasedOnSelectedMethod(g);
        }
        else
        {
            colorBasedOnSelectedMethod(g);
        }
    }
    
    private void StartButton_Click(object sender, RoutedEventArgs e)
    {
        if(MethodOfGraphInput.SelectedIndex == -1)
        {
            MessageBox.Show("Виберіть метод введення графа");
            return;
        }
        if(MethodOfColorInput.SelectedIndex == -1)
        {
            MessageBox.Show("Виберіть метод розфарбування");
            return;
        }
        if(!int.TryParse(VertexInput.Text, out int size))
        {
            MessageBox.Show("Введіть коректний розмір графа");
            return;
        }
        var graphInput = GraphInput.Text;
        if (string.IsNullOrEmpty(graphInput))
        {
            MessageBox.Show("Введіть граф");
            return;
        }
        List<List<int>> numbers;
        try
        {
                numbers = graphInput.Split('\n')
                .Select(line => line.Split(' ')
                    .Select(int.Parse)
                    .ToList())
                .ToList();
        }
        catch (Exception)
        {
            MessageBox.Show("Введіть коректний граф");
            return;
        }
        if (MethodOfGraphInput.SelectedIndex == 0)
        {
            try
            {
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (!(numbers[i][j] == 0 || numbers[i][j] == 1))
                        {
                            MessageBox.Show("Матриця суміжності має містити лише 0 та 1");
                            return;
                        }
                    }
                }
                
            }
            catch(Exception)
            {
                MessageBox.Show("Введіть коректний граф");
                return;
            }
            try
            {
                Graph g = new Graph(numbers, size);
                ColorIfCheckboxChecked(g);
            }
            catch (Exception)
            {
                MessageBox.Show("Граф неможливо розфарбувати обраним методом");
            }
        }
        else
        {
            if(!int.TryParse(EdgeInput.Text, out int amountOfPairs) || amountOfPairs <= 0)
            {
                MessageBox.Show("Введіть коректну кількість пар вершин");
                return;
            }
            List<Tuple<int, int>> pairs = new List<Tuple<int, int>>();
            try
            {
                for (int i = 0; i < amountOfPairs; i++)
                {
                    if (numbers[i][0] < 0 || numbers[i][0] >= size || numbers[i][1] < 0 || numbers[i][1] >= size)
                    {
                        MessageBox.Show("Вершини повинні бути в межах графа");
                        return;
                    }
                    pairs.Add(new Tuple<int, int>(numbers[i][0], numbers[i][1]));
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Введіть коректний граф");
                return;
            }
            try
            {
                Graph g = new Graph(pairs, size);
                ColorIfCheckboxChecked(g);
            }
            catch (Exception)
            {
                MessageBox.Show("Граф неможливо розфарбувати обраним методом");
            }
        }
    }

    private void GenerateRandomGraph_Click(object sender, RoutedEventArgs e)
    {
        if(MethodOfColorInput.SelectedIndex == -1)
        {
            MessageBox.Show("Виберіть метод розфарбування");
            return;
        }
        if(!int.TryParse(VertexInput.Text, out int size))
        {
            MessageBox.Show("Введіть коректний розмір графа");
            return;
        }

        List<List<int>> numbers = new List<List<int>>();
        for (int i = 0; i < size; i++)
        {
            List<int> row = new List<int>();
            for (int j = 0; j < size; j++)
            {
                
                if (i == j)
                {
                    row.Add(0);
                }
                else
                {
                    Random rand = new Random();
                    int num = rand.Next(0, 2);
                    row.Add(num);
                }
            }
            numbers.Add(row);
            Console.WriteLine();
        }

        try
        {
            string text = "";
            for (int i = 0; i < size; i++)
            {
                string row = "";
                for (int j = 0; j < size; j++)
                {
                    row += numbers[i][j].ToString();
                    if(j != size - 1)
                        row += " ";
                }
                if(i != size - 1)
                    row += "\n";
                text += row;
            }

            GraphInput.Text = text;
            MethodOfGraphInput.SelectedIndex = 0;
            Graph g = new Graph(numbers, size);
            ColorIfCheckboxChecked(g);
        }
        catch (Exception)
        {
            MessageBox.Show("Згенерований граф неможливо розфарбувати обраним методом");
        }
    }
}
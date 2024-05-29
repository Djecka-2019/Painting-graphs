﻿using System.Windows;
using System.Windows.Controls;

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

        List<List<int>> numbers = new List<List<int>>();
        try
        {
                numbers = graphInput.Split('\n')
                .Select(line => line.Split(' ')
                    .Select(int.Parse)
                    .ToList())
                .ToList();
        }
        catch (Exception exception)
        {
            MessageBox.Show("Введіть коректний граф");
            return;
        }
        if (MethodOfGraphInput.SelectedIndex == 0)
        {
            Graph g = new Graph(numbers, size);
            ColorIfCheckboxChecked(g);
        }
        else
        {
            if(!int.TryParse(EdgeInput.Text, out int amountOfPairs))
            {
                MessageBox.Show("Введіть коректну кількість пар вершин");
                return;
            }
            List<Tuple<int, int> > pairs = new List<Tuple<int, int>>();
            for (int i = 0; i < amountOfPairs; i++)
            {
                if(numbers[i][0] < 0 || numbers[i][0] >= size || numbers[i][1] < 0 || numbers[i][1] >= size)
                {
                    MessageBox.Show("Вершини повинні бути в межах графа");
                    return;
                }
                pairs.Add(new Tuple<int, int>(numbers[i][0], numbers[i][1]));
            }
            Graph g = new Graph(pairs, size);
            ColorIfCheckboxChecked(g);
        }
    }
}
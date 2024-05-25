namespace Painting_graphs;

using System;
using System.Collections.Generic;
using System.Linq;

class Graph
{
    private int Size;
    private List<List<int>> Adjacency;

    public Graph(List<List<int>> matrix, int size)
    {
        this.Size = size;
        Adjacency = new List<List<int>>(new List<int>[Size]);
        for (int i = 0; i < Size; i++)
        {
            Adjacency[i] = new List<int>();
            for (int j = 0; j < Size; j++)
            {
                Adjacency[i].Add(matrix[i][j]);
            }
        }
    }

    public Graph(List<Tuple<int, int>> list, int size)
    {
        this.Size = size;
        Adjacency = new List<List<int>>(new List<int>[Size]);
        List<List<int>> matrix = new List<List<int>>(new List<int>[Size]);
        for (int i = 0; i < Size; i++)
        {
            matrix[i] = new List<int>(new int[Size]);
            for (int j = 0; j < Size; j++)
            {
                matrix[i].Add(0);
            }
        }
        foreach (Tuple<int, int> pair in list)
        {
            matrix[pair.Item1][pair.Item2] = 1;
            matrix[pair.Item2][pair.Item1] = 1;
        }
        for (int i = 0; i < Size; i++)
        {
            Adjacency[i] = new List<int>();
            for (int j = 0; j < Size; j++)
            {
                Adjacency[i].Add(matrix[i][j]);
            }
        }
    }

    private bool IsSafe(int v, List<int> color, int c)
    {
        foreach (int i in Adjacency[v])
        {
            if (color[i] == c)
            {
                return false;
            }
        }
        return true;
    }

    private bool GraphColoringUtil(List<int> color, int v)
    {
        if (v == Size)
            return true;

        for (int c = 1; c <= Size; c++)
        {
            if (IsSafe(v, color, c))
            {
                color[v] = c;
                if (GraphColoringUtil(color, v + 1))
                    return true;
                color[v] = 0;
            }
        }

        return false;
    }

    public int FindChromaticNumberOfGraph()
    {
        List<int> color = new List<int>(new int[Size]);
        for(int i = 0 ; i < Size ; i++)
        {
            color[i] = 0;
        }
        if (!GraphColoringUtil(color, 0))
        {
            return 0;
        }
        int maxColor = color.Max();
        return maxColor;
    }

    public void PrintGraph()
    {
        for (int i = 0; i < Size; i++)
        {
            Console.Write(i + " : ");
            foreach (int j in Adjacency[i])
                Console.Write(j + " ");
            Console.WriteLine();
        }
    }

    public List<int> GreedyPaint()
    {
        int[] result = new int[Size];
        for (int i = 0; i < Size; i++)
        {
            result[i] = -1;
        }
        result[0] = 0;

        bool[] available = new bool[Size];
        for (int cr = 0; cr < Size; cr++)
        {
            available[cr] = true;
        }

        for (int u = 1; u < Size; u++)
        {
            foreach (int i in Adjacency[u])
            {
                if (result[i] != -1)
                {
                    available[result[i]] = false;
                }
            }

            int cr;
            for (cr = 0; cr < Size; cr++)
            {
                if (available[cr])
                {
                    break;
                }
            }

            result[u] = cr;

            for (int i = 0 ; i < Size ; i++)
            {
                available[i] = true;
            }
        }

        return result.ToList();
    }

    private int MinRemainingValues(List<int> color)
    {
        int min = int.MaxValue;
        int index = -1;
        for (int i = 0; i < color.Count; i++)
        {
            if (color[i] == 0 && Adjacency[i].Count < min)
            {
                min = Adjacency[i].Count;
                index = i;
            }
        }
        return index;
    }

    public List<int> ColorGraphWithMRVAndHeuristic()
    {
        List<int> color = new List<int>(new int[Size]);

        if (!GraphColoringUtil(color, MinRemainingValues(color)))
        {
            return new List<int>();
        }

        return color;
    }
}
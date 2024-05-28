namespace Painting_graphs;

using System;
using System.Collections.Generic;
using System.Linq;

public class Graph
{
    public int Size { get; set; }
    readonly List<List<int>> _adjacency;
    public List<List<int>> Adjacency => _adjacency;
    public Graph(List<List<int>> matrix, int size)
    {
        this.Size = size;
        _adjacency = new List<List<int>>(new List<int>[Size]);
        for (int i = 0; i < Size; i++)
        {
            _adjacency[i] = new List<int>();
        }

        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                if(matrix[i][j] == 1)
                {
                    _adjacency[i].Add(j);
                    _adjacency[j].Add(i);
                }
            }
        }
    }

    public Graph(List<Tuple<int, int>> list, int size)
    {
        this.Size = size;
        _adjacency = new List<List<int>>(new List<int>[Size]);
        for (int i = 0; i < Size; i++)
        {
            _adjacency[i] = new List<int>();
        }

        foreach (Tuple<int, int> tuple in list)
        {
            _adjacency[tuple.Item1].Add(tuple.Item2);
            _adjacency[tuple.Item2].Add(tuple.Item1);
        }
    }

    private bool IsSafe(int v, List<int> color, int c)
    {
        foreach (int i in _adjacency[v])
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

                int nextNode = MinRemainingValues(color);
                if (nextNode == -1 || GraphColoringUtil(color, nextNode))
                    return true;

                color[v] = 0;
            }
        }

        return false;
    }

    public int FindChromaticNumberOfGraph()
    {
        List<int> color = new List<int>(new int[Size]);
        for (int i = 0; i < Size; i++)
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
            foreach (int i in _adjacency[u])
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

            for (int i = 0; i < Size; i++)
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
            if (color[i] == 0)
            {
                int count = _adjacency[i].Count(j => color[j] == 0);
                if (count < min)
                {
                    min = count;
                    index = i;
                }
            }
        }

        return index;
    }

    public List<int> ColorGraphWithMrvAndHeuristic()
    {
        List<int> color = new List<int>(new int[Size]);

        if (!GraphColoringUtil(color, MinRemainingValues(color)))
        {
            return new List<int>();
        }

        return color;
    }
}

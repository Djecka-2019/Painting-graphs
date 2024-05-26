namespace Painting_graphs;

using System;
using System.Collections.Generic;
using System.Linq;

class Graph
{
    private int _size;
    private List<List<int>> _adjacency;

    public Graph(List<List<int>> matrix, int size)
    {
        this._size = size;
        _adjacency = new List<List<int>>(new List<int>[_size]);
        for (int i = 0; i < _size; i++)
        {
            _adjacency[i] = new List<int>();
        }
        for (int i = 0; i < _size; i++)
        {
            foreach (int j in matrix[i])
            {
                if (matrix[i][j] == 1)
                {
                    _adjacency[i].Add(j);
                }
            }
        }
    }

    public Graph(List<Tuple<int, int>> list, int size)
    {
        this._size = size;
        _adjacency = new List<List<int>>(new List<int>[_size]);
        for (int i = 0; i < _size; i++)
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
        if (v == _size)
            return true;

        for (int c = 1; c <= _size; c++)
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
        List<int> color = new List<int>(new int[_size]);
        for(int i = 0 ; i < _size ; i++)
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
        int[] result = new int[_size];
        for (int i = 0; i < _size; i++)
        {
            result[i] = -1;
        }
        result[0] = 0;

        bool[] available = new bool[_size];
        for (int cr = 0; cr < _size; cr++)
        {
            available[cr] = true;
        }

        for (int u = 1; u < _size; u++)
        {
            foreach (int i in _adjacency[u])
            {
                if (result[i] != -1)
                {
                    available[result[i]] = false;
                }
            }

            int cr;
            for (cr = 0; cr < _size; cr++)
            {
                if (available[cr])
                {
                    break;
                }
            }

            result[u] = cr;

            for (int i = 0 ; i < _size ; i++)
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
            if (color[i] == 0 && _adjacency[i].Count < min)
            {
                min = _adjacency[i].Count;
                index = i;
            }
        }
        return index;
    }

    public List<int> ColorGraphWithMrvAndHeuristic()
    {
        List<int> color = new List<int>(new int[_size]);

        if (!GraphColoringUtil(color, MinRemainingValues(color)))
        {
            return new List<int>();
        }

        return color;
    }
}
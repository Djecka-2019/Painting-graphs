using System.Windows.Forms;
using Microsoft.Msagl.GraphViewerGdi;
using Microsoft.Msagl.Drawing;
using Painting_graphs;
using System.Collections.Generic;

public class GraphForm : Form
{
    private Painting_graphs.Graph _graph;
    private List<int> _colors;
    
    public static List<int> GenerateHslColors(int n)
    {
        List<int> rgbColors = new List<int>();
        for (int i = 0; i < n; i++)
        {
            double hue = (double)i / n;
            int rgb = HslToRgb(hue, 0.5, 0.5);
            rgbColors.Add(rgb);
        }
        return rgbColors;
    }
    
    public static int HslToRgb(double h, double s, double l)
    {
        double r, g, b;

        if (s == 0)
        {
            r = g = b = l;
        }
        else
        {
            Func<double, double, double, double> hue2rgb = (p, q, t) =>
            {
                if (t < 0) t += 1;
                if (t > 1) t -= 1;
                if (t < 1 / 6.0) return p + (q - p) * 6 * t;
                if (t < 1 / 2.0) return q;
                if (t < 2 / 3.0) return p + (q - p) * (2 / 3.0 - t) * 6;
                return p;
            };

            var q = l < 0.5 ? l * (1 + s) : l + s - l * s;
            var p = 2 * l - q;
            r = hue2rgb(p, q, h + 1 / 3.0);
            g = hue2rgb(p, q, h);
            b = hue2rgb(p, q, h - 1 / 3.0);
        }

        return ((int)(r * 255) << 16) | ((int)(g * 255) << 8) | (int)(b * 255);
    }
    
    public GraphForm(Painting_graphs.Graph graph, List<int> colors)
    {
        _graph = graph;
        _colors = colors;

        List<int> Colors = GraphForm.GenerateHslColors(_graph.Size);
        var msaglGraph = new Microsoft.Msagl.Drawing.Graph("graph");

        for (int i = 0; i < _graph.Size; i++)
        {
            var node = new Microsoft.Msagl.Drawing.Node(i.ToString());
            byte r = (byte)((Colors[colors[i]] >> 16) & 0xFF);
            byte g = (byte)((Colors[colors[i]] >> 8) & 0xFF);
            byte b = (byte)(Colors[colors[i]] & 0xFF);
            node.Attr.FillColor = new Microsoft.Msagl.Drawing.Color(r, g, b);
            msaglGraph.AddNode(node);
        }

        var edges = new HashSet<Tuple<int, int>>();

        for (int i = 0; i < _graph.Size; i++)
        {
            foreach (var adjacentNode in _graph.Adjacency[i])
            {
                var edge = new Tuple<int, int>(i, adjacentNode);
                var edge1 = new Tuple<int, int>(adjacentNode, i);

                if (!edges.Contains(edge))
                {
                    msaglGraph.AddEdge(i.ToString(), adjacentNode.ToString());
                    edges.Add(edge);
                }

                if (!edges.Contains(edge1))
                {
                    msaglGraph.AddEdge(adjacentNode.ToString(), i.ToString());
                    edges.Add(edge1);
                }
            }
        }

        var viewer = new GViewer();

        viewer.Graph = msaglGraph;

        this.SuspendLayout();
        viewer.Dock = DockStyle.Fill;
        this.Controls.Add(viewer);
        this.ResumeLayout();
    }
}
using System.Windows.Forms;
using Microsoft.Msagl.GraphViewerGdi;
using Microsoft.Msagl.Drawing;
using Painting_graphs; // Assuming this is the namespace where your Graph class is located
using System.Collections.Generic;

public class GraphForm : Form
{
    private Painting_graphs.Graph _graph;
    private List<int> _colors;

    public GraphForm(Painting_graphs.Graph graph, List<int> colors)
    {
        _graph = graph;
        _colors = colors;

        var msaglGraph = new Microsoft.Msagl.Drawing.Graph("graph");

        // Add nodes to the graph
        for (int i = 0; i < _graph.Size; i++)
        {
            var node = new Microsoft.Msagl.Drawing.Node(i.ToString());
            int color = (_colors[i] + 1) * 128;
            byte r = (byte)((color >> 16) & 0xFF);
            byte g = (byte)((color >> 8) & 0xFF);
            byte b = (byte)((color) & 0xFF);
            node.Attr.FillColor = new Microsoft.Msagl.Drawing.Color(r, g, b);
            msaglGraph.AddNode(node);
        }

        // Create a HashSet to store the edges
        var edges = new HashSet<Tuple<int, int>>();

        // Add edges to the graph
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

        // Create a viewer object
        var viewer = new GViewer();

        // Bind the graph to the viewer
        viewer.Graph = msaglGraph;

        // Associate the viewer with the form
        this.SuspendLayout();
        viewer.Dock = DockStyle.Fill;
        this.Controls.Add(viewer);
        this.ResumeLayout();
    }
}
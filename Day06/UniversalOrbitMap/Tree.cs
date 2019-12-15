using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UniversalOrbitMap
{
  public class Tree
  {
    private List<Node> _nodes;
    private List<Node> _leaves;
    
    public Tree(string path)
    {
      _nodes = new List<Node>();
      _leaves = new List<Node>();
      Initialize(path);
    }

    private void Initialize(string path)
    {
      StreamReader file = new StreamReader(path);
      string[] lines = file.ReadToEnd().Split((char)ConsoleKey.Enter);
      file.Close();

      //extract child info
      for (int i = 0; i < lines.Length; i++)
      {
        var x = lines[i].Split(')');
        x[0] = x[0].Trim((char)'\n');
        var n = _nodes.Find(t => t.Name == x[0]);
        if (n == null)    //new node
        {
          _nodes.Add(new Node(x[0]));
          n = _nodes.Find(t => t.Name == x[0]);
          n.Children.Add(new Node(x[1]));
        }
        else
          n.Children.Add(new Node(x[1]));
      }

      //extract parent info
      for (int i = 0; i < lines.Length; i++)
      {
        var x = lines[i].Split(')');
        x[0] = x[0].Trim((char)'\n');
        var n = _nodes.Find(t => t.Name == x[1]);
        if (n == null)    //leaf
        {
          _nodes.Add(new Node(x[1]));
          n = _nodes.Find(t => t.Name == x[1]);
          n.Parent = _nodes.Find(t => t.Name == x[0]);
          _leaves.Add(n);
        }
        else
          n.Parent = _nodes.Find(t => t.Name == x[0]);
      }
    }

    public int GetTotalOrbits()
    {
      int sum = 0;

      while (_leaves != null)
      {
        sum += CountAncestorsForLeaves();
        ShedLeaves();
      }

      return sum;
    }

    private void ShedLeaves()
    {
      List<Node> newLeaves = new List<Node>();

      foreach (var n in _leaves)
      {
        var p = n.Parent;

        if (p.Parent == null) //root reached
        {
          newLeaves = null;
          break;
        }
        else 
        {
          p.Children.RemoveAll(t => t.Name == n.Name);
          if ((!(newLeaves.Contains(p))) && (p.Children.Count == 0))
            newLeaves.Add(p);
        }
      }

      _leaves.Clear();
      _leaves = newLeaves;
    }

    private int CountAncestorsForLeaves()
    {
      int sum = 0;

      foreach (var n in _leaves)
        sum += CountAncestors(n);

      return sum;
    }

    private int CountAncestors(Node n)
    {
      int count = 0;

      while (n.Parent != null)
      {
        n = n.Parent;
        count++;
      }

      return count;
    }
  }
}
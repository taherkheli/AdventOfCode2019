using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

    public int GetOrbitalTransfersToSanta()
    {
      Node you = _nodes.Find(t => t.Name == "YOU");
      Node santa = _nodes.Find(t => t.Name == "SAN");
      Node commonNode = GetFirstCommonAncestor(you, santa);
      return (GetAncestorsUpto(santa, commonNode).Count + GetAncestorsUpto(you, commonNode).Count);
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
        sum += GetAncestors(n).Count;

      return sum;
    }

    private List<Node> GetAncestors(Node n)
    {
      List<Node> result = new List<Node>();

      while (n.Parent != null)
      {
        result.Add(n.Parent);
        n = n.Parent;
      }

      return result;
    }

    private Node GetFirstCommonAncestor(Node n1, Node n2)
    {
      List<Node> n1_ancestors = GetAncestors(n1);
      List<Node> n2_ancestors = GetAncestors(n2);

      return n1_ancestors.Intersect(n2_ancestors).First<Node>();
    }

    private List<Node> GetAncestorsUpto(Node n, Node target)
    {
      List<Node> result = new List<Node>();
      List<Node> n_ancestors = GetAncestors(n);

      //check if target can be hit
      if (n_ancestors.Contains(target))
      {
        while (n.Parent != target)
        {
          result.Add(n.Parent);
          n = n.Parent;
        }
      }
      else
        result = null;

      return result;
    }    
  }
}
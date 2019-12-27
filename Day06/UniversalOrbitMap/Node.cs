using System.Collections.Generic;

namespace UniversalOrbitMap
{
  public class Node
  {
    private readonly string _name;
    private Node _parent;             //always 0-1
    private List<Node> _children;     //0-n; 0 means leaf
    private List<Node> _ancestors;    // 1 or more

    public Node(string name)
    {
      _name = name;
      _parent = null;
      _children = new List<Node>();
      _ancestors = new List<Node>();
    }

    public Node Parent { get => _parent; set => _parent = value; }
    public List<Node> Children { get => _children; set => _children = value; }
    public List<Node> Ancestors { get => _ancestors; set => _ancestors = value; }

    public string Name => _name;
  }
}

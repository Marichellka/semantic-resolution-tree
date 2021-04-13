using System.Collections.Generic;

namespace Project
{
    public class Tree
    {
        public Tree Parent { get; private set; }
        public Tree Node{get;}
        public object Key { get; set; }
        public List<Tree> Childs { get;}

        public Tree(object value)
        {
            Key = value;
            Childs= new List<Tree>();
            Parent = null;
            Node = this;
        }

        public void Insert(object value)
        {
            Tree newNode = new Tree(value);
            newNode.Parent = Node;
            Childs.Add(newNode);
        }
    }
}
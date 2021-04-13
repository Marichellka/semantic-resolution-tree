using System.Collections.Generic;

namespace Project
{
    public class Tree
    {
        public Tree Parent { get; set; }
        public string Key { get; set; }
        public List<Tree> Childs { get; } 

        public Tree(string value)
        {
            Key = value;
            Childs= new List<Tree>();
            Parent = null;
        }

        public void Insert(Tree newNode)
        {
            newNode.Parent = this;
            Childs.Add(newNode);
        }
    }
}
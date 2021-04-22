using System.Collections.Generic;

namespace Project
{
    public class Tree
    {
        public Tree Parent { get; set; }
        public string Key { get; set; }
        public List<Tree> Childs { get; set; } 

        public Tree(string value)
        {
            Key = value;
            Childs= new List<Tree>();
            Parent = null;
        }
        
        public Tree(string value, Tree leftChild, Tree rightChild)
        {
            Key = value;
            Childs = new List<Tree>();
            Childs.Add(leftChild);
            Childs.Add(rightChild);
        }

        public void Insert(Tree newNode)
        {
            newNode.Parent = this;
            Childs.Add(newNode);
        }
    }
}
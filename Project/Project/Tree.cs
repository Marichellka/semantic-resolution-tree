namespace Project
{
    public class Tree
    {
        public Tree Parent { get; private set; }
        public Tree Node{get; private set;}
        public object Key { get; set; }
        public Tree LeftChild { get; private set; }
        public Tree RightChild { get; private set; }

        public Tree(object value)
        {
            Key = value;
            LeftChild = null;
            RightChild = null;
            Parent = null;
            Node = this;
        }

        public void insertLeft(object value)
        {
            Tree newNode = new Tree(value);
            newNode.Parent = Node;
            if(LeftChild==null)
                this.LeftChild = newNode;
            else
            {
                newNode.LeftChild = LeftChild;
                LeftChild = newNode;
            }
        }
        
        public void insertRight(object value)
        {
            Tree newNode = new Tree(value);
            newNode.Parent = Node;
            if(RightChild==null)
                this.RightChild = newNode;
            else
            {
                newNode.RightChild = RightChild;
                RightChild = newNode;
            }
        }
    }
}
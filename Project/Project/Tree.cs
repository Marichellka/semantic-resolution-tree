namespace Project
{
    public class Tree
    {
        public object Parent { get; private set; }
        public object Key { get; set; }
        public object LeftChild { get; private set; }
        public object RightChild { get; private set; }
        
        public Tree(object value)
        {
            Key = value;
            LeftChild = null;
            RightChild = null;
            Parent = null;
        }

        public void insertLeft(object value)
        {
            Tree newNode = new Tree(value);
            newNode.Parent = this.Key;
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
            newNode.Parent = this.Key;
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
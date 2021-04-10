namespace Project
{
    public class Tree
    {
        public object Key { get; }
        public object LeftChild { get; private set; }
        public object RightChild { get; private set; }

        public Tree(object value)
        {
            Key = value;
            LeftChild = null;
            RightChild = null;
        }
        
        public void insertLeft(object value)
        {
            if(LeftChild==null)
                this.LeftChild = new Tree(value);
            else
            {
                Tree newNode = new Tree(value);
                newNode.LeftChild = LeftChild;
                LeftChild = newNode;
            }
        }
        
        public void insertRight(object value)
        {
            if(RightChild==null)
                this.RightChild = new Tree(value);
            else
            {
                Tree newNode = new Tree(value);
                newNode.RightChild = RightChild;
                RightChild = newNode;
            }
        }
    }
}
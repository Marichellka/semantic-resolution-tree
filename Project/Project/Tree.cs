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
    }
}
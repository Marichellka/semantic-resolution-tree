using System;
using System.Collections.Generic;
using System.Collections;

namespace Project
{
    public class Processing
    {
        public Tree Head { get; }
        public Hashtable ht { get;}
        public Processing(Tree item)
        {
            Head = item;
            ht = new Hashtable();
        }

        public void ProcessingTree(Tree item)
        {
            foreach (var subtree in item.Childs)
            {
                if (subtree.Key.Equals("if"))
                {
                    if (Condition(subtree))
                    {
                        ProcessingTree(subtree.Childs[1]);
                    }
                    else if (subtree.Childs.Count > 2)
                    {
                        ProcessingTree(subtree.Childs[2]);
                    }
                }
                else if (subtree.Key.Equals("while"))
                { 
                    while (Condition(subtree))
                    {
                        ProcessingTree(subtree.Childs[1]);
                    }
                }
                else
                {
                    SymmetricalTraversal(subtree);
                }
            }
        }

        public void Optimisation()
        {
            int n = 0;
            bool[] used = new bool[100];
            used[n] = true;
            Queue<Tree> queue = new Queue<Tree>();
            queue.Enqueue(Head);
            while (queue.Count != 0)
            {
                Tree item = queue.Dequeue();
                if (item.Key.Equals("*") && (item.Childs[0].Key.Equals("0") || item.Childs[1].Key.Equals("0")))
                {
                    item.Key = "0";
                    item.Childs = null;
                }
                else if (item.Key.Equals("/") && item.Childs[0].Key.Equals("0"))
                {
                    item.Key = "0";
                    item.Childs = null;
                }

                if (item.Childs != null)
                {
                    for (int i = 0; i < item.Childs.Count; i++)
                    {
                        if (item.Childs[i] != null && !used[n + i + 1])
                        {
                            used[n + i + 1] = true;
                            queue.Enqueue(item.Childs[i]);
                            n += i + 1;
                        }
                    }
                }
            }
        }
        
        private double SymmetricalTraversal(Tree tree)
        {
            ICollection keys = ht.Keys;
            if (tree.Childs != null && tree.Childs.Count != 0)
            {
                double result = 0;
                switch (tree.Key)
                {
                    case "+":
                        result = SymmetricalTraversal(tree.Childs[0]) + SymmetricalTraversal(tree.Childs[1]);
                        break;
                    case "-":
                        result =SymmetricalTraversal(tree.Childs[0]) - SymmetricalTraversal(tree.Childs[1]);
                        break;
                    case "*":
                        result = SymmetricalTraversal(tree.Childs[0]) * SymmetricalTraversal(tree.Childs[1]);
                        break;
                    case "/":
                        result =SymmetricalTraversal(tree.Childs[0]) / SymmetricalTraversal(tree.Childs[1]);
                        break;
                    case "=":
                        if (ht.ContainsKey(tree.Childs[0].Key))
                        {
                            ht[tree.Childs[0].Key] = SymmetricalTraversal(tree.Childs[1]);
                        }
                        else
                        {
                            ht.Add(tree.Childs[0].Key, SymmetricalTraversal(tree.Childs[1]));
                        }
                        return 0;
                }
                tree.Key = result.ToString();
                tree.Childs = new List<Tree>();
            }
            foreach (string key in keys)
            {
                if (tree.Key.Equals(key))
                {
                    return double.Parse(tree.Key = ht[key].ToString());
                }
            }
            return double.Parse(tree.Key);
        }

        private bool Condition(Tree item)
        {
            double a = SymmetricalTraversal(item.Childs[0].Childs[0]);
            double b = SymmetricalTraversal((item.Childs[0].Childs[1]));
            switch (item.Childs[0].Key)
            {
                case "<":
                    if (a < b) return true;
                    return false;
                case "<=":
                    if (a <= b) return true;
                    return false;
                case ">":
                    if (a > b) return true;
                    return false;
                case ">=":
                    if (a >= b) return true;
                    return false;
                case "==":
                    if (a == b) return true;
                    return false;
                case "!=":
                    if (a != b) return true;
                    return false;
            }
            return false;
        }
    }
}

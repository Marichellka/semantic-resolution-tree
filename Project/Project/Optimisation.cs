using System;
using System.Collections.Generic;
using System.Collections;

namespace Project
{
    public class Optimisation
    {
        private Tree Head;
        public Hashtable ht { get; private set; }
        public Optimisation(Tree item)
        {
            Head = item;
            ht = new Hashtable();
            SymmetricalTraversal(Head);
            Print(Head);
        }
        public string SymmetricalTraversal(Tree tree)
        {
            ICollection keys = ht.Keys;
            if (tree.Childs[0] != null && tree.Childs.Count != 0)
            {
                double result = 0;
                switch (tree.Key)
                {
                    case "+":
                        result = (double.Parse(SymmetricalTraversal(tree.Childs[0])) + double.Parse(SymmetricalTraversal(tree.Childs[1])));
                        break;
                    case "-":
                        result = (double.Parse(SymmetricalTraversal(tree.Childs[1])) - double.Parse(SymmetricalTraversal(tree.Childs[0])));
                        break;
                    case "*":
                        result = (double.Parse(SymmetricalTraversal(tree.Childs[0])) * double.Parse(SymmetricalTraversal(tree.Childs[1])));
                        break;
                    case "/":
                        result = (double.Parse(SymmetricalTraversal(tree.Childs[1])) / double.Parse(SymmetricalTraversal(tree.Childs[0])));
                        break;
                }
                tree.Key = result.ToString();
                tree.Childs = new List<Tree>();
            }
            if (tree.Key.Equals("="))
            {
                ht.Add(tree.Childs[1].Key, SymmetricalTraversal(tree.Childs[0]));
            }
            foreach (string key in keys)
            {
                if (tree.Key.Equals(key))
                {
                    return tree.Key = ht[key].ToString();
                }
            }
            return tree.Key;
        }
        public void Print(Tree tree)
        {
            if (tree == null) return;
            for (int i = 0; i < tree.Childs.Count; i++)
            {
                if (i != tree.Childs.Count / 2)
                {
                    Console.Write("(");
                    Print(tree.Childs[i]);
                    Console.Write(")");
                }
                else
                {
                    Console.Write("(");
                    Print(tree.Childs[i]);
                    Console.Write(")");
                    if (tree.Key != null)
                        Console.Write(tree.Key);
                    else
                    {
                        Console.Write("_");
                    }
                }
            }

            if (tree.Childs.Count == 0)
            {
                if (tree.Key != null)
                    Console.Write(tree.Key);
                else
                {
                    Console.Write("_");
                }
            }
        }
    }
}

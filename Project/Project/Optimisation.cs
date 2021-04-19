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
            foreach (var code in Head.Childs)
            {
                Print(Head);
                SymmetricalTraversal(code);
                Console.WriteLine();
            }
        }
        public double SymmetricalTraversal(Tree tree)
        {
            ICollection keys = ht.Keys;
            if (tree.Childs != null && tree.Childs.Count != 0)
            {
                Print(Head);
                Console.WriteLine();
                double result = 0;
                switch (tree.Key)
                {
                    case "+":
                        result = SymmetricalTraversal(tree.Childs[0]) + SymmetricalTraversal(tree.Childs[1]);
                        break;
                    case "-":
                        result = SymmetricalTraversal(tree.Childs[1]) - SymmetricalTraversal(tree.Childs[0]);
                        break;
                    case "*":
                        result = SymmetricalTraversal(tree.Childs[0]) * SymmetricalTraversal(tree.Childs[1]);
                        break;
                    case "/":
                        result = SymmetricalTraversal(tree.Childs[1]) / SymmetricalTraversal(tree.Childs[0]);
                        break;
                    case "=":
                        if (ht.ContainsKey(tree.Childs[1].Key))
                        {
                            ht[tree.Childs[1].Key] = SymmetricalTraversal(tree.Childs[0]);
                        }
                        else
                        {
                            ht.Add(tree.Childs[1].Key, SymmetricalTraversal(tree.Childs[0]));
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

﻿using System;
using System.Collections.Generic;
using System.Collections;

namespace Project
{
    public class Processing
    {
        public Tree Head { get; }
        public Hashtable ht { get; private set; }
        public Processing(Tree item)
        {
            Head = item;
            ht = new Hashtable();
            Print(item);
            Console.WriteLine();
        }

        public void ProcessingTree(Tree item)
        {
            Print(item);
            Console.WriteLine();
            foreach (var subtree in item.Childs)
            {
                if (subtree.Key.Equals("if"))
                {
                    if (IfCondition(subtree))
                    {
                        ProcessingTree(subtree.Childs[1]);
                    }
                    else if (subtree.Childs.Count > 2)
                    {
                        ProcessingTree(subtree.Childs[2]);
                    }
                }
                else
                {
                    SymmetricalTraversal(subtree);
                }
                Print(item);
                Console.WriteLine();
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
                else if (item.Key.Equals("/") && item.Childs[1].Key.Equals("0"))
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

        private bool IfCondition(Tree item)
        {
            double a = SymmetricalTraversal(item.Childs[0].Childs[1]);
            double b = SymmetricalTraversal((item.Childs[0].Childs[0]));
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
        public void Print(Tree tree)
        {
            if (tree == null) return;
            if (tree.Childs == null || tree.Childs.Count == 0)
            {
                if (tree.Key != null)
                    Console.Write(tree.Key);
                else
                {
                    Console.Write("_");
                }
                return;
            }
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
        }
    }
}

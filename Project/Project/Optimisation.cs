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
        }
        public void BFS(int n = 0)
        {
            bool[] used = new bool[20];
            used[n] = true;
            Queue<Tree> queue = new Queue<Tree>();
            queue.Enqueue(Head);
            while (queue.Count != 0)
            {
                Tree item = queue.Dequeue();
                if (item.Key.Contains("="))
                {
                    if (!item.Childs[1].Key.Equals("+") && !item.Childs[1].Key.Equals("-") && !item.Childs[1].Key.Equals("*") && !item.Childs[1].Key.Equals("/"))
                    {
                        ht.Add(item.Childs[0].Key, item.Childs[1].Key);
                    }
                }
                for (int i = 0; i < item.Childs.Count; i++)
                {
                    if (item.Childs[i] != null && !used[n + i + 1])
                    {
                        used[n + i + 1] = true;
                        queue.Enqueue(item.Childs[i]);
                        n += i + 1;
                    }
                }
                Action(item);
            }
        }
        private void Action(Tree item)
        {
            ICollection keys = ht.Keys;
            if (item.Key.Equals("+") || item.Key.Equals("-") || item.Key.Equals("*") || item.Key.Equals("/"))
            {
                foreach (string key in keys)
                {
                    if (item.Childs[0].Key.Contains(key))
                    {
                        /*item.Childs[0].Key.Replace(Convert.ToChar(key), Convert.ToChar(ht[key]));*/
                        item.Childs[0].Key = ht[key].ToString();
                    }
                    if (item.Childs[1].Key.Contains(key))
                    {
                        /*item.Childs[1].Key.Replace(Convert.ToChar(key), Convert.ToChar(ht[key]));*/
                        item.Childs[1].Key = ht[key].ToString();
                    }
                }
                double a = 0;
                double b = 0;
                if (Double.TryParse(item.Childs[0].Key, out a) && Double.TryParse(item.Childs[1].Key, out b))
                {
                    switch (item.Key)
                    {
                        case "+":
                            item = new Tree((a + b).ToString());
                            break;
                        case "-":
                            item = new Tree((a - b).ToString());
                            break;
                        case "*":
                            item = new Tree((a * b).ToString());
                            break;
                        case "/":
                            item = new Tree((a / b).ToString());
                            break;
                    }
                }
                if (item.Parent.Key.Equals("+") || item.Parent.Key.Equals("-") || item.Parent.Key.Equals("*") || item.Parent.Key.Equals("/"))
                {
                    item = item.Parent;
                    Action(item);
                }
            }
        }
    }
}

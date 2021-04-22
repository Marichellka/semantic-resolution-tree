using System;

namespace Project
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser parsing = new Parser( @"info.txt");
            Processing process = new Processing(parsing.AST);
            process.Optimisation();
            process.ProcessingTree(process.Head);
            Console.WriteLine("result="+process.Head.Childs[process.Head.Childs.Count-1].Key);
        }
    }
}
using System;

namespace Project
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser parsing = new Parser( @"C:\Users\user\Desktop\Навчання\ОП\2 семестр\info.txt");
            Processing process = new Processing(parsing.AST);
            process.Optimisation();
            process.ProcessingTree(process.Head);
            Console.WriteLine("result="+process.Result());
        }
    }
}
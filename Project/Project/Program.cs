using System;
using System.IO;

namespace Project
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser parcer = new Parser(Path.Combine(Environment.CurrentDirectory, @"info.txt"));
            Processing o = new Processing(parcer.AST);
        }
    }
}
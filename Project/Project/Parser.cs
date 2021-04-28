using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Project
{
    public class Parser
    {
        public Tree AST { get; private set; }

        public Parser(string path)
        {
            AST = new Tree(string.Empty);
            using (StreamReader reader = new StreamReader(path, Encoding.Default))
            {
                while (!reader.EndOfStream)
                {
                    ParsingString(reader);
                }
            }
        }
        
         private void ParsingString(StreamReader reader)
        {
            string code = reader.ReadLine().Replace(" ", "");
            code.Replace("	", "");
            if (code.Length > 4 && code.Contains("if("))
            {
                AST.Insert(new Tree("if"));
                AST = AST.Childs[AST.Childs.Count-1];
                AST.Insert(ShuntingYard(code.Substring(code.IndexOf("if")+2)));
                AST.Insert(new Tree(string.Empty));
                AST = AST.Childs[AST.Childs.Count-1];
            }
            else if(code=="else")
            {
                AST = AST.Parent;
                AST.Insert(new Tree("else"));
                AST=AST.Childs[AST.Childs.Count-1];
            }
            else if (code.Contains("while("))
            {
                AST.Insert(new Tree("while"));
                AST = AST.Childs[AST.Childs.Count-1];
                AST.Insert(ShuntingYard(code.Substring(code.IndexOf("while")+5)));
                AST.Insert(new Tree(string.Empty));
                AST = AST.Childs[AST.Childs.Count-1];
            }
            else if (code=="endif")
            {
                AST = AST.Parent.Parent;
            }
            else if (code == "endwhile")
            {
                AST = AST.Parent.Parent;
            }
            else
            {
                AST.Insert(ShuntingYard(code));
            }
        }
         
        private Tree ShuntingYard(string expression)
        {
            Stack<char> operStack = new Stack<char>();
            Stack<Tree> exprStack = new Stack<Tree>();
            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == ')')
                {
                    while (operStack.Peek()!='(')
                    {
                        if ("=><!".Contains(operStack.Peek()) && "=><!".Contains(exprStack.Peek().Key))
                        {
                            exprStack.Peek().Key += operStack.Pop();
                        }
                        else
                        {
                            char operat = operStack.Pop();
                            Tree rightChild = exprStack.Pop();
                            Tree leftChild = exprStack.Pop();
                            exprStack.Push(new Tree(operat.ToString(), leftChild, rightChild));
                        }
                    }
                    operStack.Pop();
                }
                else if (Char.IsLetter(expression[i])||Char.IsDigit(expression[i]) || (expression[i]=='-' && (i==0 || expression[i-1]=='(')))
                {
                    string elem = expression[i].ToString();
                    i++;
                    while (i<expression.Length && (Char.IsLetter(expression[i]) || Char.IsDigit(expression[i]) || expression[i]=='.'))
                    {
                        elem += expression[i];
                        i++;
                    }
                    i--;
                    exprStack.Push(new Tree(elem));
                }
                else if (expression[i] == '(')
                {
                    operStack.Push(expression[i]);
                }
                else if("+-*/!=<>".Contains(expression[i]))
                {
                    while (operStack.Count != 0 && GetPriority(operStack.Peek()) >= GetPriority(expression[i]))
                    {
                        if (operStack.Peek() == '=' && expression[i] == '=')
                        {
                            break;
                        }
                        char operat = operStack.Pop();
                        Tree rightChild = exprStack.Pop();
                        Tree leftChild = exprStack.Pop();
                        exprStack.Push(new Tree(operat.ToString(), leftChild, rightChild));
                    }
                    operStack.Push(expression[i]);
                }
            }

            while (operStack.Count != 0)
            {
                char operat = operStack.Pop();
                Tree rightChild = exprStack.Pop();
                Tree leftChild = exprStack.Pop();
                exprStack.Push(new Tree(operat.ToString(), leftChild, rightChild));
            }
            return exprStack.Pop();
        }

        private int GetPriority(char oper)
        {
            switch (oper)
            {
                case '(':
                    return -2;
                case ')':
                    return 1;
                case char when "+-".Contains(oper):
                    return 2;
                case char when "*/".Contains(oper):
                    return 3;
                case char when "><!".Contains(oper):
                    return -1;
                case '=':
                    return 0;
            }

            return 0;
        }
    }
}
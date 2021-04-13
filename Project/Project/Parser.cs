using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Project
{
    public class Parser
    {
        private Tree AST;
        private List<string> tokens;

        public Parser(string path)
        {
            AST = new Tree(null);
            using (StreamReader reader = new StreamReader(path, Encoding.Default))
            {
                while (!reader.EndOfStream)
                {
                    string code = reader.ReadLine().Replace(" ", "");
                    code.Replace("	", "");
                    PolishNotation(code);
                    AST.Insert(BuildCurrentTree());
                }
            }
        }

        private int GetPriority(char oper)
        {
            switch (oper)
            {
                case '(':
                    return 0;
                case ')':
                    return 1;
                case '+':
                    return 2;
                case '-':
                    return 2;
                case '*':
                    return 3;
                case '/':
                    return 3;
                case '=':
                    return -1;
            }

            return 0;
        }
        
        public void PolishNotation(string code)
        {
            tokens = new List<string>();
            Stack<char> stack = new Stack<char>();
            for (int i = 0; i < code.Length; i++)
            {
                if (code[i] == ')')
                {
                    while (stack.Peek()!='(')
                    {
                        tokens.Add(stack.Pop().ToString());
                    }
                    stack.Pop();
                }
                else if (Char.IsLetter(code[i])||Char.IsDigit(code[i]))
                {
                    string elem = "";
                    while (i<code.Length && (Char.IsLetter(code[i]) || Char.IsDigit(code[i]) || code[i]=='.'))
                    {
                        
                        elem += code[i];
                        i++;
                    }

                    i--;
                    tokens.Add(elem);
                }
                else if (code[i] == '('||code[i]=='=')
                {
                    stack.Push(code[i]);
                }
                else if(code[i]=='+'||code[i]=='-'||code[i]=='/'||code[i]=='*')
                {
                    if(stack.Count==0)
                        stack.Push(code[i]);
                    else if(GetPriority(stack.Peek())<GetPriority(code[i]))      
                        stack.Push(code[i]);
                    else                              
                    {
                        while(stack.Count!=0 && GetPriority(stack.Peek())>=GetPriority(code[i]))
                            tokens.Add(stack.Pop().ToString());
                        
                        stack.Push(code[i]);           
                    } 
                }
            }
            foreach (var elem in stack)
            {
                tokens.Add(elem.ToString());
            }
        }

        public Tree BuildCurrentTree()
        {
            Tree currentTree =new Tree(null);
            Tree currentNode = currentTree;
            for(int i=tokens.Count-1; i>=0; i--)
            {
                if (double.TryParse(tokens[i], out _) || Char.IsLetter(tokens[i][0]))
                {
                    if (currentNode.Childs.Count <  2)
                    {
                        currentNode.Insert(new Tree(tokens[i]));
                    }
                    else
                    {
                        currentNode = currentNode.Parent;
                        while (currentNode.Childs.Count==2)
                        {
                            currentNode = currentNode.Parent;
                        }
                        currentNode.Insert(new Tree(tokens[i]));
                    }
                }
                else
                {
                    if (currentNode.Key!=null)
                    {
                        currentNode.Insert(new Tree(null));
                        currentNode = currentNode.Childs[currentNode.Childs.Count - 1];
                    }
                    currentNode.Key = tokens[i];
                }
            }
            return currentTree;
        }
    }
}
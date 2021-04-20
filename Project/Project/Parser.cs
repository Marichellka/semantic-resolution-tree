using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Project
{
    public class Parser
    {
        public Tree AST { get; private set; }
        private List<string> tokens;

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
            tokens = new List<string>();
            if (code.Length > 4 && code.Contains("if("))
            {
                AST.Insert(new Tree("if"));
                AST = AST.Childs[AST.Childs.Count-1];
                PolishNotation(code.Substring(code.IndexOf("if")+2));
                AST.Insert(BuildCurrentTree());
                AST.Insert(new Tree(string.Empty));
                AST = AST.Childs[AST.Childs.Count-1];
            }
            else if(code=="else")
            {
                AST = AST.Parent;
                AST.Insert(new Tree("else"));
                AST=AST.Childs[AST.Childs.Count-1];
            }
            else if (code=="endif")
            {
                AST = AST.Parent;
                AST = AST.Parent;
            }
            else
            {
                PolishNotation(code);
                AST.Insert(BuildCurrentTree());
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
                case char when "+-".Contains(oper):
                    return 2;
                case char when "*/".Contains(oper):
                    return 3;
                case char when "=><!".Contains(oper):
                    return -1;
            }

            return 0;
        }
        
        private void PolishNotation(string code)
        {
            Stack<char> stack = new Stack<char>();
            for (int i = 0; i < code.Length; i++)
            {
                if (code[i] == ')')
                {
                    while (stack.Peek()!='(')
                    {
                        if ("=><!".Contains(stack.Peek()) && "=><!".Contains(tokens[tokens.Count - 1]))
                        {
                            tokens[tokens.Count - 1] = stack.Pop()+ tokens[tokens.Count - 1] ;
                        }
                        else
                        {
                            tokens.Add(stack.Pop().ToString());
                        }
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
                else if ("(!=<>".Contains(code[i]))
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

        private Tree BuildCurrentTree()
        {
            Tree currentTree =new Tree(string.Empty);
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
                    if (!currentNode.Key.Equals(string.Empty))
                    {
                        currentNode.Insert(new Tree(string.Empty));
                        currentNode = currentNode.Childs[currentNode.Childs.Count - 1];
                    }
                    currentNode.Key = tokens[i];
                }
            }
            return currentTree;
        }
    }
}
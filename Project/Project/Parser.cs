using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Project
{
    public class Parser
    {
        private Tree AST;
        private List<object> tokens;

        public Parser(string path)
        {
            string code = "";
            using (StreamReader reader = new StreamReader(path, Encoding.Default))
            {
                while (!reader.EndOfStream)
                {
                    code += reader.ReadLine();
                }
            }
            ParsingString(code);
        }

        private void ParsingString(string code)
        {
            tokens = new List<object>();
            for (int i = 0; i < code.Length; i++)
            {
                if(code[i]==' ')continue;
                if (Char.IsDigit(code[i]) || code[i]=='.')
                {
                    if (i != 0)
                    {
                        string prevToken = tokens[tokens.Count - 1].ToString();
                        if (double.TryParse(prevToken, out double n))
                        {
                            tokens[tokens.Count - 1] = prevToken + code[i];
                        }
                        else
                        {
                            tokens.Add(code[i]);
                        }
                    }
                    else
                    {
                        tokens.Add(code[i]);
                    }
                }
                else if(Char.IsLetter(code[i]))
                {
                    if (i != 0)
                    {
                        string prevToken = tokens[tokens.Count - 1].ToString();
                        if (Char.IsLetter(prevToken[prevToken.Length - 1]))
                        {
                            tokens[tokens.Count - 1] = prevToken + code[i];
                        }
                        else
                        {
                            tokens.Add(code[i]);
                        }
                    }
                    else
                    {
                        tokens.Add(code[i]);
                    }
                }
                else
                {
                    tokens.Add(code[i]);
                }
            }
        }

        public void BuildTree()
        {
            AST = new Tree(null);
            Tree currentNode = AST;
            currentNode.Insert(null);
            currentNode = currentNode.Childs[0];
            foreach (var token in tokens)
            {
                if (token.Equals('('))
                {
                    continue;
                }
                else if (double.TryParse(token.ToString(), out double n) || Char.IsLetter(token.ToString()[0]))
                {
                    currentNode.Key = token;
                    currentNode = currentNode.Parent;
                }
                else if (token.Equals(')'))
                {
                    currentNode = currentNode.Parent;
                }
                else
                {
                    currentNode.InsertBetween(new Tree(token));
                    currentNode = currentNode.Childs[currentNode.Childs.Count - 1];
                    currentNode.Insert(null);
                    currentNode = currentNode.Childs[currentNode.Childs.Count - 1];
                }
            }
        }
    }
}
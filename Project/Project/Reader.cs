using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Project
{
    public class Reader
    {
        private Tree AST;
        private List<object> tokens;

        public Reader(string path)
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
            foreach (var token in tokens)
            {
                if (token.Equals('('))
                {
                    currentNode.insertLeft(currentNode);
                    currentNode = currentNode.LeftChild;
                }
                else if (token.GetType() == typeof(double))
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
                    currentNode.Key = token;
                    currentNode.insertRight(null);
                    currentNode = currentNode.RightChild;
                }
            }
        }
    }
}
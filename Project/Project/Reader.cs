using System;
using System.Collections.Generic;
using System.IO;

namespace Project
{
    public class Reader
    {
        private Tree AST;

        public Reader(string path)
        {
            string code = "";
            using (StreamReader reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    code += reader.ReadLine();
                }
            }
        }
        
        private List<object> ParsingString(string code)
        {
            List<object> tokens = new List<object>();
            for (int i = 0; i < code.Length; i++)
            {
                if (double.TryParse(code[i].ToString(), out double n))
                {
                    if (tokens[tokens.Count-1].GetType()==typeof(double))
                    {
                        tokens[tokens.Count - 1] = Convert.ToDouble(tokens[tokens.Count - 1].ToString() + code[i]);
                    }
                    else
                    {
                        tokens.Add(n);
                    }
                }
                else if(code[i]=='.')
                {
                    tokens[tokens.Count - 1] = Convert.ToDouble(tokens[tokens.Count - 1].ToString() + code[i]+code[i+1]);
                    i++;
                }
                else
                {
                    tokens.Add(code[i]);
                }
            }
            return tokens;
        }
    }
}
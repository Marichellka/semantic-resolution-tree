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
    }
}
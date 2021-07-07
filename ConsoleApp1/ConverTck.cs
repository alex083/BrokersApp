using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class ConverTck
    {
        public static void RewriteTck(List<string> path)
        {
            List<string> newlines = new List<string>();
            for (int i=0; i < path.Count; i++)
            {
                string[] lines = File.ReadAllLines(path[i]);
                foreach (string line in lines)
                {
                    string temp = line.Replace(';',',');
                    newlines.Add(temp);
                }
                File.WriteAllLines(path[i], newlines.ToArray());
            }
        }
    }
}

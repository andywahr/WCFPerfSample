using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp47
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"DefaultConnectionLimit = {System.Net.ServicePointManager.DefaultConnectionLimit}");
            Console.WriteLine($"Expect100Continue = {System.Net.ServicePointManager.Expect100Continue}");
            Console.WriteLine($"Expect100Continue = {System.Net.ServicePointManager.UseNagleAlgorithm}");
        }
    }
}

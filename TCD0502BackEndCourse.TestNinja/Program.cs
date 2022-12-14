using System;

namespace TCD0502BackEndCourse.TestNinja
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Math math = new Math();
            int a = 10;
            int b = 20;
            int c = math.Sum(a, b);
            Console.WriteLine(c);
        }
    }
}

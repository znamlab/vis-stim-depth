using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpPractice1
{
    static class AStaticClass //We cannot create an instance out of a static class (e.g. Math)
    {
        //Static methods are called by Class.method; other methods are called by Object.method
        public static void AStaticMethod(string name)
        {
            Console.WriteLine("Hello " + name);
        }
    }
}

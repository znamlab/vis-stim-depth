using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpPractice1
{
    class ItalianChef : Chef //Inherits all functions from the chef class
    {
        public void MakePasta()
        {
            Console.WriteLine("The Italian chef made pasta");
        }

        public override void MakeSpecialDish() //Overrise a method from the superclass
        {
            Console.WriteLine("The Italian chef made chicken parm");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpPractice1
{
    class Chef
    {
        public void MakeChicken()
        {
            Console.WriteLine("The chef made chicken");

        }

        public virtual void MakeSpecialDish()  //"virtual":subclasses can overrise this method
        {
            Console.WriteLine("The chef made bbq ribs");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpPractice1
{
    class Program
    {
        static void Main(string[] args) 
        {   
            //Data types
            string string1 = "Machine";
            int int1;
            int1 = 70;

            //Write or Read lines
            Console.WriteLine("Write down a number: ");
            double inputNum = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine(inputNum + 15);

            Console.WriteLine("Hello\nWorld " + string1 + int1);
            Console.WriteLine(string1.ToUpper());
            int1++;
            Console.WriteLine(int1);

            //Arrays
            int[] anarray = { 1, 2, 3, 4, 5 };
            Console.WriteLine(anarray[0]);
            var anotherArray = new int[3] { 10, 20, 30 };
            Console.WriteLine(anotherArray[0]);

            //Call a method
            SayHi(string1, int1);
            Console.WriteLine(cube(int1));
            Console.WriteLine(GetDay(3));
            aWhileLoop();
            aDoWhileLoop();
            aForLoop();
            aMatrix();

            AStaticClass.AStaticMethod("ME"); //Calling a STATIC METHOD

            Chef aChef = new Chef();
            aChef.MakeChicken();
            ItalianChef aItalianChef = new ItalianChef();
            aItalianChef.MakeChicken();
            aItalianChef.MakePasta();
            aItalianChef.MakeSpecialDish();


            //Create an object with a datatype specified by another class (an instance of the specified class)
            //Way1:
            /*Book book1 = new Book("John");
                book1.title = "Harry Porter";
                book1.author = "JK Rowling";
                book1.pages = 400;
                Console.WriteLine(book1.title);
            */
            //Way2:
            Book book1 = new Book("Harry Porter", "JK Rowling", 400, "lala");
            Console.WriteLine(book1.title + " " + book1.author + " " + Convert.ToString(book1.pages) + " " + book1.Type);
            Console.WriteLine(Book.aStaticAttribute);



            Console.ReadLine();  //keep console window up until we input something
        }

        //Create another method: methodType returnType methodName (inputType inputName)
        static void SayHi(string name, int age)
        {
            Console.WriteLine("Hello " +name + " " + age  );
        }


        //Return type
        static int cube(int anum)
        {
            int result = anum * anum * anum;
            return result;
        }

        //If statement:if(){}, else if(){}, else(){}

        //Switch statement
        static string GetDay(int daynum)
        {
            string DayName;
            switch (daynum)
            {
                case 0:
                    DayName = "Sunday";
                    break; //stop checking other cases if this case is true
                case 1:
                    DayName = "Monday";
                    break;
                default:
                    DayName = "Invalid Day Number";
                    break;
            }
            return DayName;
        }

        //While Loop & Do while loop & For loop
        static void aWhileLoop()
        {
            int index = 1;
            while(index<=5)
            {
                Console.WriteLine(index);
                index++;
            }
        }
        static void aDoWhileLoop() //Do before checking While condition
        {
            int index = 6;
            do
            {
                Console.WriteLine(index);
                index++;
            } while (index <= 5);
        }
        static void aForLoop()
        {
            for(int i=1; i<=5; i++)
            {
                Console.WriteLine(i);
            }
        }


        //Matrix or 2d array
        static void aMatrix()
        {
            int[,] myMatrix = new int[2, 2];
            myMatrix[0, 0] = 1;
            myMatrix[0, 1] = 2;
            myMatrix[1, 0] = 3;
            myMatrix[1, 1] = 4;
            Console.WriteLine(myMatrix[1,1]);
        }

        /*Exception handling: 
         * try{} 
         * catch(Exception e)
         *  {Console.WriteLine(e.Message)}
         * catch(aExceptionType e)
         *  {}
         * finally
         *  {always execute this block}
         * 
         */





    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpPractice1
{
    class Book //A class creates a customised datatype 
    {
        //Class attributes: specify what's inside the Book datatype
        public string title;
        public string author;
        public int pages;
        private string type;

        //Static class attributes: attributes shared by all objects in this class
        public static string aStaticAttribute = "\nThis is a static attribute";

        //Constructor: a method that will be called whenever you create an object of this class;
        //we can have more tha one constructor, so that we have alternative ways of creating an object 
        public Book(string aTitle, string aAuthor, int aPages, string aType)
        {
            title = aTitle;
            author = aAuthor;
            pages = aPages;
            Type = aType;
        }

        //Getters & Setters: Prevent an unintendable argument when creating an object
        public string Type
        {
            get { return type; }
            set
            {
                if (value == "TypeA" || value == "TypeB")  //value is whatever we passed in
                    type = value;
                else
                {
                    type = "Invalid";
                }

            }
        }
    }
}

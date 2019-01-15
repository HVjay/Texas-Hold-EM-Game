using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(174,43);
       
            //Console.BufferHeight = 40;
            Console.Title = "Texas Hold Em";
            DealingCards dc = new DealingCards();
            bool quit = false;
            while(!quit)
            {
                dc.Deal();

                char selection = ' ';
                while(!selection.Equals('Y') && !selection.Equals('N'))
                {
                    Console.WriteLine("Play Again? Y-N");
                    selection = Convert.ToChar(Console.ReadLine().ToUpper());
                    if(selection.Equals('Y'))
                    {
                        quit = false;
                    }
                    else if(selection.Equals('N'))
                    {
                        quit = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid, Try Again");
                    }
                }
            }
         }
    }
}

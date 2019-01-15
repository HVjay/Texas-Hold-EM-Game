using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp1
{
    class DrawingCards
    {
        public static void CardOutLine( int xmap, int ymap)
        {
            Console.ForegroundColor = ConsoleColor.White;

            int x = xmap * 12;
            int y = ymap;


            Console.SetCursorPosition(x, y);
            Console.Write(" __________\n");

            for (int i=0; i<10; i++)
            {
                Console.SetCursorPosition(x, y + 1 + i);

                if(i!=9) //if not bottom
                {
                    Console.WriteLine("|          |");
                }

                else
                {
                    Console.WriteLine("|__________|");
                }
            }

        }

        public static void DrawingCard(Card card, int xmap, int ymap)
        {
            string cSuit=" ";
            int x = xmap * 12;
            int y = ymap;

            switch(card.MySuit)
            {
                case Card.SUIT.HEARTS:
                    cSuit = "HEARTS";
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case Card.SUIT.DIAMONDS:
                    cSuit = "DIAMONDS";
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case Card.SUIT.CLUBS:
                    cSuit = "CLUBS";
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case Card.SUIT.SPADES:
                    cSuit = "SPADES";
                    Console.ForegroundColor = ConsoleColor.White;
                    break;

            }

            Console.SetCursorPosition(x+3, y+5);
            Console.Write(cSuit);
            Console.SetCursorPosition(x + 4, y + 7);
            Console.Write(card.MyValue);
        }
    }
}

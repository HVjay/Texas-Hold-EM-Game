using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class CardDeck: Card
    {
        const int carNum = 52;
        public Card[] deck; 

        //constructor
        public  CardDeck()
        {
            deck = new Card[52];
        }

        public Card[] getDeck { get{ return deck; } }

        //create deck
        public void createDeck()
        {
            int i = 0;
            foreach(SUIT s in Enum.GetValues(typeof(SUIT)))
            {
                foreach(VALUE v in Enum.GetValues(typeof(VALUE)))
                {
                    deck[i] = new Card { MySuit = s, MyValue = v };
                    i++;
                }
            }
            Shuffle();
        }

        public void Shuffle()
        {
            Random rand = new Random();
            Card temporary;

            for(int k=0; k<1000; k++)
            {
                for (int c=0; c<carNum; c++)
                {
                    int tempIndex = rand.Next(13);
                    temporary = deck[c];
                    deck[c] = deck[tempIndex];
                    deck[tempIndex] = temporary;
                }
            }
        }
    }
}

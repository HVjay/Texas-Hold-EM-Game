using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class DealingCards:CardDeck
    {
        private Card[] player1;
        private Card[] player2;
        private Card[] player1S;
        private Card[] player2S;
        private Card[] table;
        private Card[] player1SS;
        private Card[] player2SS;
        
        public DealingCards()
        {
            player1 = new Card[2];
            player1S = new Card[7];
            player1SS = new Card[7];
            player2 = new Card[2];
            player2S = new Card[7];
            player2SS = new Card[7];
            table = new Card[5];

        }

        public void Deal()
        {
            createDeck();
            SetHand();
            SortCards();
            DisplayCards();
            EvaluateHands();
        }

        public void SetHand()
        {
            for (int j=0; j<2; j++)
                player1[j]=getDeck[j];

            for (int k = 2; k < 4; k++)
            {
                player2[k-2] = getDeck[k];
            }

            for (int i=4; i<9; i++)
            {
                table[i - 4] = getDeck[i];
            }
        }

        public void SortCards()
        {
            for (int i=0; i<2; i++)
            {
                player1S[i] = player1[i];
            }
            for (int i = 2; i < 7; i++)
            {
                player1S[i] = table[i-2];
            }

            for (int i = 0; i < 2; i++)
            {
                player2S[i] = player2[i];
            }
            for (int i = 2; i < 7; i++)
            {
                player2S[i] = table[i - 2];
            }

            var queryPlayer1 = from hand in player1S
                              orderby hand.MyValue
                              select hand;

            var queryPlayer2 = from hand in player2S
                                orderby hand.MyValue
                                select hand;

            int index = 0;
            foreach(var element in queryPlayer1)
            {
                player1SS[index] = element;
                index++;
            }

           index = 0;
            foreach (var element in queryPlayer2)
            {
                player2SS[index] = element;
                index++;
            }
        }

        public void DisplayCards()
        {
            Console.Clear();
            int x = 0;
            int y = 1;

            //p1 hand
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Player 1 Hand");
            for (int i=0; i<2; i++)
            {
                DrawingCards.CardOutLine(x, y);
                DrawingCards.DrawingCard(player1[i], x, y);
                x++;
            }

            y = 15;
            x = 0;
            Console.SetCursorPosition(x, 14);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Player 2 Hand");
            for (int k=2; k<4; k++)
            {
                DrawingCards.CardOutLine(x, y);
                DrawingCards.DrawingCard(player2[k - 2], x, y);
                x++;
            }

            y = 30;
            x = 0;
            Console.SetCursorPosition(x, 29);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Cards on Table");
            for(int h=4; h<9; h++)
            {
                DrawingCards.CardOutLine(x, y);
                DrawingCards.DrawingCard(table[h - 4], x, y);
                x++;
            }

        }

        public void EvaluateHands()
        {
            HandEvaluate player1HandEval = new HandEvaluate(player1SS);
            HandEvaluate player2HandEval = new HandEvaluate(player2SS);

            Hand player1Hand = player1HandEval.EvaluateHand();
            Hand player2Hand = player2HandEval.EvaluateHand();

            Console.WriteLine("\n\n\n\n\nPlayer 1 Hand:" + player1Hand);
            Console.WriteLine("\nPlayer 2 Hand:" + player2Hand+"\n");

            if (player1Hand > player2Hand)
            {
                Console.WriteLine("Player 1 Wins");
            }
            else if(player1Hand<player2Hand)
            {
                Console.WriteLine("Player 2 Wins");
            }
            else
            {
                if (player1HandEval.HandValues.Total > player2HandEval.HandValues.Total)
                    Console.WriteLine("Player 1 Wins");

                else if (player1HandEval.HandValues.Total < player2HandEval.HandValues.Total)
                    Console.WriteLine("Player 2 Wins");

                else if (player1HandEval.HandValues.HighCard > player2HandEval.HandValues.HighCard)
                    Console.WriteLine("Player 1 Wins");

                else if (player1HandEval.HandValues.HighCard < player2HandEval.HandValues.HighCard)
                    Console.WriteLine("Player 2 Wins");
                else
                    Console.WriteLine("Draw");
            }
        }
    }
}

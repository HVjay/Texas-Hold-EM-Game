using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public enum Hand
    {
        Bad,
        OnePair,
        TwoPairs,
        ThreeKind,
        Straight,
        Flush,
        FullHouse,
        FourKind,
        StraightFlush,
        RoyalFlush

    }

    public struct HandValue
    {
        public int Total { get; set; }
        public int HighCard { get; set; }
    }

    class HandEvaluate : Card
    {
        private int heartSum;
        private int diamSum;
        private int clubSum;
        private int spadeSum;
        private Card[] cards;
        private HandValue handValue;

        public HandEvaluate(Card[] Ssorted)
        {
            heartSum = 0;
            diamSum = 0;
            clubSum = 0;
            spadeSum = 0;
            cards = new Card[7];
            Cards = Ssorted; //**
            handValue = new HandValue();

        }

        public HandValue HandValues
        {
            get { return handValue; }
            set { handValue = value; }
        }

        public Card[] Cards
        {
            get { return cards; }
            set
            {
                cards[0] = value[0];
                cards[1] = value[1];
                cards[2] = value[2];
                cards[3] = value[3];
                cards[4] = value[4];
                cards[5] = value[5];
                cards[6] = value[6];
            }
        }

        public Hand EvaluateHand()
        {
            getNumberOfSuit();
            if (RoyalFlush())
                return Hand.RoyalFlush;
            else if (StraightFlush())
                return Hand.StraightFlush;
            else if (FourOfKind())
                return Hand.FourKind;
            else if (FullHouse())
                return Hand.FullHouse;
            else if (Flush())
                return Hand.Flush;
            else if (Straight())
                return Hand.Straight;
            else if (ThreeOfKind())
                return Hand.ThreeKind;
            else if (TwoPairs())
                return Hand.TwoPairs;
            else if (OnePair())
                return Hand.OnePair;

            handValue.HighCard = (int)cards[6].MyValue;
            return Hand.Bad;
        }

        private void getNumberOfSuit()
        {
            foreach(var suits in Cards)
            {
                if (suits.MySuit == Card.SUIT.HEARTS)
                    heartSum++;
                else if (suits.MySuit == Card.SUIT.DIAMONDS)
                    diamSum++;
                else if (suits.MySuit == Card.SUIT.CLUBS)
                    clubSum++;
                else if (suits.MySuit == Card.SUIT.SPADES)
                    spadeSum++;
            }
        }

        private bool RoyalFlush()
        {
            if (heartSum >= 5 || diamSum >= 5 || clubSum > 5 || spadeSum >= 5)
            {
                for (int i = 0; i < 2; i++)
                {
                    if (cards[i].MyValue == Card.VALUE.TEN)
                    {
                        if (cards[i].MyValue + 1 == Card.VALUE.JACK &&
                           cards[i].MyValue + 2 == Card.VALUE.QUEEN &&
                           cards[i].MyValue + 3 == Card.VALUE.KING &&
                           cards[i].MyValue + 4 == Card.VALUE.ACE)
                        {
                            return true;
                        }
                    }
                }
            }
            return false; 
        }
        private bool StraightFlush()
        {
            if (cards[0].MyValue + 1 == cards[1].MyValue &&
                cards[1].MyValue + 1 == cards[2].MyValue &&
                cards[2].MyValue + 1 == cards[3].MyValue &&
                cards[3].MyValue + 1 == cards[4].MyValue)
            {
                if (heartSum >= 5 || diamSum >= 5 || clubSum > 5 || spadeSum >= 5)
                {
                    handValue.Total = (int)cards[4].MyValue;
                    return true;
                }
            }

            else if (cards[1].MyValue + 1 == cards[2].MyValue &&
                cards[2].MyValue + 1 == cards[3].MyValue &&
                cards[3].MyValue + 1 == cards[4].MyValue &&
                cards[4].MyValue + 1 == cards[5].MyValue)
            {
                if (heartSum >= 5 || diamSum >= 5 || clubSum > 5 || spadeSum >= 5)
                {
                    handValue.Total = (int)cards[4].MyValue;
                    return true;
                }
            }

            else if (cards[2].MyValue + 1 == cards[3].MyValue &&
                     cards[3].MyValue + 1 == cards[4].MyValue &&
                     cards[4].MyValue + 1 == cards[5].MyValue &&
                     cards[5].MyValue + 1 == cards[6].MyValue)
            {
                if (heartSum >= 5 || diamSum >= 5 || clubSum > 5 || spadeSum >= 5)
                {
                    handValue.Total = (int)cards[4].MyValue;
                    return true;
                }
            }
            return false;
        }
        private bool FourOfKind()
        {
            if(cards[0].MyValue==cards[1].MyValue && cards[0].MyValue==cards[2].MyValue && cards[0].MyValue==cards[3].MyValue)
            {
                handValue.Total = (int)cards[0].MyValue * 4;
                handValue.HighCard = (int)cards[4].MyValue;
                return true;
            }
            else if (cards[1].MyValue == cards[2].MyValue && cards[1].MyValue == cards[3].MyValue && cards[1].MyValue == cards[4].MyValue)
            {
                handValue.Total = (int)cards[1].MyValue * 4;
                handValue.HighCard = (int)cards[7].MyValue;
                return true;
            }
            else if (cards[2].MyValue == cards[3].MyValue && cards[2].MyValue == cards[4].MyValue && cards[2].MyValue == cards[5].MyValue)
            {
                handValue.Total = (int)cards[2].MyValue * 4;
                handValue.HighCard = (int)cards[7].MyValue;
                return true;
            }
            else if (cards[3].MyValue == cards[4].MyValue && cards[3].MyValue == cards[5].MyValue && cards[3].MyValue == cards[6].MyValue)
            {
                handValue.Total = (int)cards[3].MyValue * 4;
                handValue.HighCard = (int)cards[7].MyValue;
                return true;
            }
            else if (cards[4].MyValue == cards[5].MyValue && cards[4].MyValue == cards[6].MyValue && cards[4].MyValue == cards[7].MyValue)
            {
                handValue.Total = (int)cards[4].MyValue * 4;
                handValue.HighCard = (int)cards[7].MyValue;
                return true;
            }

            return false;
        }

        private bool FullHouse() //call three of a kind and two of a kind
        {
            if( ThreeOfKind() && FourOfKind())
            {
                return true;
            }
            return false;
        }

        private bool Flush()
        {
            if(heartSum>=5 || diamSum>=5 || clubSum>5|| spadeSum>=5)
            {
                handValue.Total = (int)cards[4].MyValue;
                return true;
            }

            return false;
        }

        private bool Straight()
        {
            if(cards[0].MyValue+1==cards[1].MyValue &&
                cards[1].MyValue + 1 == cards[2].MyValue &&
                cards[2].MyValue + 1 == cards[3].MyValue &&
                cards[3].MyValue + 1 == cards[4].MyValue )
            {
                handValue.Total = (int)cards[4].MyValue;
                return true;
            }
            else if (cards[1].MyValue + 1 == cards[2].MyValue &&
                cards[2].MyValue + 1 == cards[3].MyValue &&
                cards[3].MyValue + 1 == cards[4].MyValue &&
                cards[4].MyValue + 1 == cards[5].MyValue)
            {
                handValue.Total = (int)cards[5].MyValue;
                return true;
            }
            else if (cards[2].MyValue + 1 == cards[3].MyValue &&
                     cards[3].MyValue + 1 == cards[4].MyValue &&
                     cards[4].MyValue + 1 == cards[5].MyValue &&
                     cards[5].MyValue + 1 == cards[6].MyValue)
            {
                handValue.Total = (int)cards[6].MyValue;
                return true;
            }
            return false;
        }

        private bool ThreeOfKind()
        {
            if((cards[0].MyValue==cards[1].MyValue && cards[0].MyValue==cards[2].MyValue) ||
                    (cards[1].MyValue==cards[2].MyValue && cards[1].MyValue==cards[3].MyValue))
            {
                handValue.Total = (int)cards[2].MyValue * 3;
                handValue.HighCard = (int)cards[4].MyValue;
                return true;
            }
            else if ((cards[2].MyValue == cards[3].MyValue && cards[2].MyValue == cards[4].MyValue) ||
                         (cards[3].MyValue == cards[4].MyValue && cards[3].MyValue == cards[5].MyValue))
            {
                handValue.Total = (int)cards[4].MyValue * 3;
                handValue.HighCard = (int)cards[6].MyValue;
                return true;
            }
            else if ((cards[4].MyValue == cards[5].MyValue && cards[4].MyValue == cards[6].MyValue))
            {
                handValue.Total = (int)cards[6].MyValue * 3;
                handValue.HighCard = (int)cards[3].MyValue;
                return true;
            }
            return false;
        }

        private bool TwoPairs()
        {
            //case 1: 1,2
            if(cards[0].MyValue==cards[1].MyValue && cards[2].MyValue==cards[3].MyValue)
            {
                handValue.Total = ((int)cards[1].MyValue * 2) + ((int)cards[3].MyValue * 2);
                handValue.HighCard = (int)cards[4].MyValue;
                return true;
            }

            else if (cards[0].MyValue == cards[1].MyValue && cards[3].MyValue == cards[4].MyValue)
            {
                handValue.Total = ((int)cards[1].MyValue * 2) + ((int)cards[4].MyValue * 2);
                handValue.HighCard = (int)cards[5].MyValue;
                return true;
            }

            else if (cards[0].MyValue == cards[1].MyValue && cards[4].MyValue == cards[5].MyValue)
            {
                handValue.Total = ((int)cards[1].MyValue * 2) + ((int)cards[5].MyValue * 2);
                handValue.HighCard = (int)cards[6].MyValue;
                return true;
            }
            else if (cards[0].MyValue == cards[1].MyValue && cards[5].MyValue == cards[6].MyValue)
            {
                handValue.Total = ((int)cards[1].MyValue * 2) + ((int)cards[6].MyValue * 2);
                handValue.HighCard = (int)cards[3].MyValue;
                return true;
            }

            //case 2 2,3
            else if (cards[1].MyValue == cards[2].MyValue && cards[3].MyValue == cards[4].MyValue)
            {
                handValue.Total = ((int)cards[2].MyValue * 2) + ((int)cards[4].MyValue * 2);
                handValue.HighCard = (int)cards[5].MyValue;
                return true;
            }
            else if (cards[1].MyValue == cards[2].MyValue && cards[4].MyValue == cards[5].MyValue)
            {
                handValue.Total = ((int)cards[2].MyValue * 2) + ((int)cards[5].MyValue * 2);
                handValue.HighCard = (int)cards[6].MyValue;
                return true;
            }
            else if (cards[1].MyValue == cards[2].MyValue && cards[5].MyValue == cards[6].MyValue)
            {
                handValue.Total = ((int)cards[2].MyValue * 2) + ((int)cards[6].MyValue * 2);
                handValue.HighCard = (int)cards[4].MyValue;
                return true;
            }

            //case 3 3,4
            else if (cards[2].MyValue == cards[3].MyValue && cards[4].MyValue == cards[5].MyValue)
            {
                handValue.Total = ((int)cards[3].MyValue * 2) + ((int)cards[5].MyValue * 2);
                handValue.HighCard = (int)cards[6].MyValue;
                return true;
            }
            else if (cards[2].MyValue == cards[3].MyValue && cards[5].MyValue == cards[6].MyValue)
            {
                handValue.Total = ((int)cards[3].MyValue * 2) + ((int)cards[6].MyValue * 2);
                handValue.HighCard = (int)cards[6].MyValue;
                return true;
            }

            //case 4  4,5
            else if (cards[3].MyValue == cards[4].MyValue && cards[5].MyValue == cards[6].MyValue)
            {
                handValue.Total = ((int)cards[4].MyValue * 2) + ((int)cards[6].MyValue * 2);
                handValue.HighCard = (int)cards[2].MyValue;
                return true;
            }
            return false;

        }

        private bool OnePair()
        {
            if(cards[0].MyValue==cards[1].MyValue)
            {
                handValue.Total = (int)cards[0].MyValue*2;
                handValue.HighCard = (int)cards[6].MyValue;
                return true;
            }

            else if (cards[1].MyValue == cards[2].MyValue)
            {
                handValue.Total = (int)cards[2].MyValue * 2;
                handValue.HighCard = (int)cards[6].MyValue;
                return true;
            }
            else if (cards[2].MyValue == cards[3].MyValue)
            {
                handValue.Total = (int)cards[3].MyValue * 2;
                handValue.HighCard = (int)cards[6].MyValue;
                return true;
            }
            else if (cards[3].MyValue == cards[4].MyValue)
            {
                handValue.Total = (int)cards[4].MyValue * 2;
                handValue.HighCard = (int)cards[6].MyValue;
                return true;
            }
            else if (cards[4].MyValue == cards[5].MyValue)
            {
                handValue.Total = (int)cards[5].MyValue * 2;
                handValue.HighCard = (int)cards[6].MyValue;
                return true;
            }
            else if (cards[5].MyValue == cards[6].MyValue)
            {
                handValue.Total = (int)cards[6].MyValue * 2;
                handValue.HighCard = (int)cards[4].MyValue;
                return true;
            }
            return false;
        }
    }
}

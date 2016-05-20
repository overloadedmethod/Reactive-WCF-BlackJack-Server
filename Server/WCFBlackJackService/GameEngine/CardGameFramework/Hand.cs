using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.Serialization;
using WCFBlackJackService.GameEngine.CardGameFramework;

namespace WCFBlackJackService.GameEngine.CardGameFramework
{
    public class Hand
    {
        // Creates a list of cards
        [DataMember]
        protected List<Card> cards = new List<Card>();
        [DataMember]
        public int NumCards { get { return cards.Count; } }
        [DataMember]
        public List<Card> Cards { get { return cards; } }

        /// <summary>
        /// Checks to see if the hand contains a card of a certain face value
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool ContainsCard(FaceValue item)
        {
            foreach (Card c in cards)
            {
                if (c.CardFaceValue == item)
                {
                    return true;
                }
            }
            return false;
        }
    }

    /// <summary>
    /// This class is game-specific.  Creates a BlackJack hand that inherits from class hand
    /// </summary>
    public class BlackJackHand : Hand
    {
        /// <summary>
        /// This method compares two BlackJack hands
        /// </summary>
        /// <param name="otherHand"></param>
        /// <returns></returns>
        public int CompareFaceValue(object otherHand)
        {
            BlackJackHand aHand = otherHand as BlackJackHand;
            if (aHand != null)
            {
                return this.GetSumOfHand().CompareTo(aHand.GetSumOfHand());
            }
            else
            {
                throw new ArgumentException("Argument is not a Hand");
            }
        }

        /// <summary>
        /// Gets the total value of a hand from BlackJack values
        /// </summary>
        /// <returns>int</returns>
        public int GetSumOfHand()
        {
            int val = 0;
            int numAces = 0;

            foreach (Card c in cards)
            {
                if (c.CardFaceValue == FaceValue.Ace)
                {
                    numAces++;
                    val += 11;
                }
                else if (c.CardFaceValue == FaceValue.Jack || c.CardFaceValue == FaceValue.Queen || c.CardFaceValue == FaceValue.King)
                {
                    val += 10;
                }
                else
                {
                    val += (int)c.CardFaceValue;
                }
            }

            while (val > 21 && numAces > 0)
            {
                val -= 10;
                numAces--;
            }

            return val;
        }

        public List<Card> toUnknown()
        {
            return Enumerable
                .Repeat(new Card { CardFaceValue = FaceValue.Unknown, CardSuit = Suit.Unknown }, cards.Count)
                .ToList();
        }
    }
}

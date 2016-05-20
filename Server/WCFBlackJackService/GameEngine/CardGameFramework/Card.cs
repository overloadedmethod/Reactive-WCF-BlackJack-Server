using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WCFBlackJackService.GameEngine.CardGameFramework
{
    public enum Suit
    {
        Diamonds, Spades, Clubs, Hearts, Unknown
    }

    /// <summary>
    /// Card face values
    /// </summary>
    public enum FaceValue
    {
        Two = 2, Three = 3, Four = 4, Five = 5, Six = 6, Seven = 7, Eight = 8,
        Nine = 9, Ten = 10, Jack = 11, Queen = 12, King = 13, Ace = 14, Unknown
    }
    public class Card
    {
        [DataMember]
        public Suit CardSuit { get; set; }
        [DataMember]
        public FaceValue CardFaceValue { get; set; }

        /// <summary>
        /// Return the card as a string (i.e. "The Ace of Spades")
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "The" + CardFaceValue.ToString() + "of" + CardSuit.ToString();
        }
    }
}

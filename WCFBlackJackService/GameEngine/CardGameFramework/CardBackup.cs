

using System.Runtime.Serialization;

namespace BlackJack_v2.CardGameFramework
{
    /// <summary>
    /// Card suit values
    /// </summary>
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

    public class CardBackup
    {
        // Objects for card information
        private readonly Suit suit;
        private readonly FaceValue faceVal;
        private bool isCardUp;

        [DataMember]
        public Suit Suit { get { return suit; } }
        [DataMember]
        public FaceValue FaceVal { get { return faceVal; } }
        [DataMember]
        public bool IsCardUp { get { return isCardUp; } set { isCardUp = value; } }

        /// <summary>
        /// Constructor for a new card.  Assign the card a suit, face value, and if the card is facing up or down
        /// </summary>
        /// <param name="suit"></param>
        /// <param name="faceVal"></param>
        /// <param name="isCardUp"></param>
        public CardBackup(Suit suit, FaceValue faceVal, bool isCardUp)
        {
            this.suit = suit;
            this.faceVal = faceVal;
            this.isCardUp = isCardUp;
        }

        /// <summary>
        /// Return the card as a string (i.e. "The Ace of Spades")
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "The" + faceVal.ToString() + "of" + suit.ToString();
        }
    }
}

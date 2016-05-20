using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Runtime.Serialization;
using WCFBlackJackService.GameEngine.CardGameFramework;

namespace WCFBlackJackService.GameEngine.CardGameFramework
{
    public class Deck
    {
        // Creates a list of cards
        protected List<Card> cards = new List<Card>();

        // Returns the card at the given position
        public Card this[int position] { get { return (Card)cards[position]; } }

        /// <summary>
        /// One complete deck with every face value and suit
        /// </summary>
        public Deck()
        {
            var Suits = (Enum.GetValues(typeof(Suit)) as IEnumerable<Suit>)
                .ToObservable().Where(suit => suit != Suit.Unknown);
            var Faces = (Enum.GetValues(typeof(FaceValue)) as IEnumerable<FaceValue>)
                .ToObservable().Where(face => face != FaceValue.Unknown);

            (from suit in Suits
             from face in Faces
             select new Card { CardSuit = suit, CardFaceValue = face })
            .Subscribe(card => cards.Add(card));
        }
        /// <summary>
        /// Shuffles the cards in the deck
        /// </summary>
        public void Shuffle()
        {
            Random random = new Random();
            for (int i = 0; i < cards.Count; i++)
            {
                int index1 = i;
                int index2 = random.Next(cards.Count);
                SwapCard(index1, index2);
            }
        }

        /// <summary>
        /// Draws one card and removes it from the deck
        /// </summary>
        /// <returns></returns>
        public Card Draw()
        {
            Card card = cards[0];
            cards.RemoveAt(0);
            return card;
        }

        /// <summary>
        /// Swaps the placement of two cards
        /// </summary>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        private void SwapCard(int index1, int index2)
        {
            Card card = cards[index1];
            cards[index1] = cards[index2];
            cards[index2] = card;
        }
    }
}

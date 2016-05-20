
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using WCFBlackJackService.GameEngine.CardGameFramework;
using WCFBlackJackService.WCF.WCFAuxiliary;

namespace WCFBlackJackService.GameEngine
{
    [DataContract]
    public class Player : INotifyPropertyChanged
    {
        [DataMember]
        public String Name { get; set; }
        [DataMember]
        public BlackJackHand Hand { get; set; }
        [DataMember]
        public decimal Bet { get; set; }
        [DataMember]
        public decimal AccountCredit { get; set; }
        public PlayerGameStateToken Token { get; set; }

        public decimal SessionID { get; set; }
        public decimal PlayerID { get; set; }

        public IBlackJackServiceCallback Callback { get; set; }
        public int Room { get; set; }
        public BlackJackHand NewHand()
        {
            Hand = new BlackJackHand();
            return Hand;
        }

        public void ClearBet()
        {
            Bet = 0;
        }

        public bool HasBlackJack()
        {
            if (Hand.GetSumOfHand() == 21)
                return true;
            else return false;
        }

        public bool HasBust()
        {
            if (Hand.GetSumOfHand() > 21)
                return true;
            else return false;
        }

        public void Hit()
        {
            //Card c = CurrentDeck.Draw();
           // Hand.Cards.Add(c);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

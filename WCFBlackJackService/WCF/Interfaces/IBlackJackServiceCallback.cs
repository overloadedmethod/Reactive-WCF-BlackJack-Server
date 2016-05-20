
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCFBlackJackService.GameEngine;
using WCFBlackJackService.GameEngine.CardGameFramework;

namespace WCFBlackJackService
{
    
    public interface IBlackJackServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void PlayerRecievedCards(Player player,List<Card> cards);

        [OperationContract(IsOneWay = true)]
        void PlayerDeal(Player Player, decimal amount);

        [OperationContract(IsOneWay = true)]
        void PlayerStand(Player Player);

        [OperationContract(IsOneWay = true)]
        void PlayerJoin(Player player);

        [OperationContract(IsOneWay = true)]
        void PlayerList(Player[] list);

        [OperationContract(IsOneWay = true)]
        void PlayerLeave(Player player);

        [OperationContract(IsOneWay = true)]
        void PlayerJoinsSession(Player player, int sessionID);

        [OperationContract(IsOneWay = true)]
        void PlayerStatus(Player player,string status);
    }
}

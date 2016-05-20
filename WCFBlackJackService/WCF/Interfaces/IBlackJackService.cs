using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WCFBlackJackService.GameEngine;


namespace WCFBlackJackService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IBlackJackService" in both code and config file together.
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IBlackJackServiceCallback))]
    public interface IBlackJackService
    {
        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
        void Hit(Player player);

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
        void Deal(Player player, decimal sum);

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
        void Stand(Player player);

        [OperationContract(IsOneWay = true, IsInitiating = true, IsTerminating = false)]
        void Join(Player name);

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = true)]
        void Leave(Player player);

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
        void WantsToPlay(Player player);
    }
}

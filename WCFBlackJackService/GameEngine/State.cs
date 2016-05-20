using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFBlackJackService.GameEngine
{
    /// <summary>
    /// EndResult maintains the game result state
    /// </summary>
    public enum GameState
    {
        DealerBlackJack, PlayerBlackJack, PlayerBust, DealerBust, Push, PlayerWin, DealerWin, PlayerEnter, PlayerLeave,PlayerHit,PlayerStand,PlayerDeal
    }
}

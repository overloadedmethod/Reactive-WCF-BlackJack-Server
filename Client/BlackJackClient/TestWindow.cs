using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel;
using BlackJackClient.BlackJackService;

namespace BlackJackClient
{
    public partial class TestWindow : Form, BlackJackService.IBlackJackServiceCallback
    {
        BlackJackServiceClient _client;
        Player _player;
        public TestWindow()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _client = new BlackJackService.BlackJackServiceClient(new InstanceContext(this),
                                    "NetTcpBinding_IBlackJackService");
        }

        private void btnJoin_Click(object sender, EventArgs e)
        {
            _player = new Player { Name = txtbxJoinName.Text };
            _client.Join(_player);    
        }

        public void SendResult(int sum)
        {
            //label1.Text = sum.ToString();
        }

        public void PlayerHit(Player sender, Hand hand)
        {
            rchtxtbxLog.Text += sender.Name + " Hit\n";
        }

        public void PlayerStand(Player sender, string message)
        {
            rchtxtbxLog.Text += (sender.Name + " Stand\n");
        }

        public void PlayerJoin(Player player)
        {
            rchtxtbxLog.Text += (player.Name + " Join\n");
        }

        public void PlayerLeave(Player player)
        {
            rchtxtbxLog.Text += (player.Name + " Leave\n");
        }

        private void btnDeal_Click(object sender, EventArgs e)
        {
            _client.Deal(_player, int.Parse(txtbxDeal.Text));
        }

        public void PlayerDeal(Player sender, decimal amount)
        {
            rchtxtbxLog.Text += (sender.Name + " deals "+amount+"\n");
        }

        public void PlayerList(Player[] list)
        {
            rchtxtbxLog.Text += "Current list of players:\n";
            foreach(Player player in list)
            {
                rchtxtbxLog.Text += player.Name + "\n";
            }
        }

        public void PlayerHit(string Player, string card)
        {
            rchtxtbxLog.Text += Player + " Hit and gets " + card +"\n";
        }

        //public void PlayerDeal(Player Player, decimal amount)
        //{
        //    rchtxtbxLog.Text += (Player.Name + " Deals amount " + amount + "\n");
        //}

        public void PlayerStand(string Player)
        {
            rchtxtbxLog.Text += (Player + " Stand\n");
        }

        private void btnHit_Click(object sender, EventArgs e)
        {
            _client.Hit(_player);
        }

        public void JoinSession(int sessionID)
        {
            throw new NotImplementedException();
        }

        public void PlayerRecievedCards(Player player, Card[] cards)
        {
            rchtxtbxLog.Text += (player.Name + " Recieved cards:\n");
            foreach (var card in cards)
                rchtxtbxLog.Text += card.CardFaceValue+"of"+card.CardSuit+"\n";
        }

        public void PlayerStand(Player Player)
        {
            rchtxtbxLog.Text += (Player.Name + " Stands\n");
        }

        private void btnStand_Click(object sender, EventArgs e)
        {
            _client.Stand(_player);
        }

        public void PlayerStatus(Player player, string status)
        {
            rchtxtbxLog.Text += (player.Name + " "+status);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _client.WantsToPlay(_player);
        }

        public void PlayerJoinsSession(Player player, int sessionID)
        {
            throw new NotImplementedException();
        }
    }
}

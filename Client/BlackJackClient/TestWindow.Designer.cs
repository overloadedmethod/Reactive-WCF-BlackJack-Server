namespace BlackJackClient
{
    partial class TestWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnJoin = new System.Windows.Forms.Button();
            this.btnHit = new System.Windows.Forms.Button();
            this.btnStand = new System.Windows.Forms.Button();
            this.txtbxJoinName = new System.Windows.Forms.TextBox();
            this.rchtxtbxLog = new System.Windows.Forms.RichTextBox();
            this.btnDeal = new System.Windows.Forms.Button();
            this.txtbxDeal = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnJoin
            // 
            this.btnJoin.Location = new System.Drawing.Point(12, 12);
            this.btnJoin.Name = "btnJoin";
            this.btnJoin.Size = new System.Drawing.Size(75, 23);
            this.btnJoin.TabIndex = 2;
            this.btnJoin.Text = "Enter As";
            this.btnJoin.UseVisualStyleBackColor = true;
            this.btnJoin.Click += new System.EventHandler(this.btnJoin_Click);
            // 
            // btnHit
            // 
            this.btnHit.Location = new System.Drawing.Point(13, 42);
            this.btnHit.Name = "btnHit";
            this.btnHit.Size = new System.Drawing.Size(75, 23);
            this.btnHit.TabIndex = 3;
            this.btnHit.Text = "Hit";
            this.btnHit.UseVisualStyleBackColor = true;
            this.btnHit.Click += new System.EventHandler(this.btnHit_Click);
            // 
            // btnStand
            // 
            this.btnStand.Location = new System.Drawing.Point(13, 72);
            this.btnStand.Name = "btnStand";
            this.btnStand.Size = new System.Drawing.Size(75, 23);
            this.btnStand.TabIndex = 4;
            this.btnStand.Text = "Stand";
            this.btnStand.UseVisualStyleBackColor = true;
            this.btnStand.Click += new System.EventHandler(this.btnStand_Click);
            // 
            // txtbxJoinName
            // 
            this.txtbxJoinName.Location = new System.Drawing.Point(94, 12);
            this.txtbxJoinName.Name = "txtbxJoinName";
            this.txtbxJoinName.Size = new System.Drawing.Size(100, 22);
            this.txtbxJoinName.TabIndex = 5;
            // 
            // rchtxtbxLog
            // 
            this.rchtxtbxLog.Location = new System.Drawing.Point(13, 101);
            this.rchtxtbxLog.Name = "rchtxtbxLog";
            this.rchtxtbxLog.ReadOnly = true;
            this.rchtxtbxLog.Size = new System.Drawing.Size(619, 306);
            this.rchtxtbxLog.TabIndex = 6;
            this.rchtxtbxLog.Text = "";
            // 
            // btnDeal
            // 
            this.btnDeal.Location = new System.Drawing.Point(201, 11);
            this.btnDeal.Name = "btnDeal";
            this.btnDeal.Size = new System.Drawing.Size(75, 23);
            this.btnDeal.TabIndex = 7;
            this.btnDeal.Text = "Deal";
            this.btnDeal.UseVisualStyleBackColor = true;
            this.btnDeal.Click += new System.EventHandler(this.btnDeal_Click);
            // 
            // txtbxDeal
            // 
            this.txtbxDeal.Location = new System.Drawing.Point(283, 12);
            this.txtbxDeal.Name = "txtbxDeal";
            this.txtbxDeal.Size = new System.Drawing.Size(100, 22);
            this.txtbxDeal.TabIndex = 8;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(94, 42);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Play!";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TestWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 419);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtbxDeal);
            this.Controls.Add(this.btnDeal);
            this.Controls.Add(this.rchtxtbxLog);
            this.Controls.Add(this.txtbxJoinName);
            this.Controls.Add(this.btnStand);
            this.Controls.Add(this.btnHit);
            this.Controls.Add(this.btnJoin);
            this.Name = "TestWindow";
            this.Text = "Test window";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnJoin;
        private System.Windows.Forms.Button btnStand;
        private System.Windows.Forms.TextBox txtbxJoinName;
        private System.Windows.Forms.Button btnHit;
        private System.Windows.Forms.RichTextBox rchtxtbxLog;
        private System.Windows.Forms.Button btnDeal;
        private System.Windows.Forms.TextBox txtbxDeal;
        private System.Windows.Forms.Button button1;
    }
}


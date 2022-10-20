using System;
using System.Collections.Generic;
using System.Text;

namespace ScrollingShooter
{
    class GUI
    {
        private int score;
        private int playerHP;
        private int lives;
        
        public void Initialize(int score, int HP, int lives)
        {
            this.score = score;
            this.lives = lives;
            playerHP = HP;
        }

        public int SCORE
        {
            get { return score; }
            set { this.score = value; }
        }

        public int PlayerHP
        {
            get { return playerHP; }
            set { this.playerHP = value; }
        }

        public int LIVES
        {
            get { return lives; }
            set { this.lives = value; }
        }
        



    }
}

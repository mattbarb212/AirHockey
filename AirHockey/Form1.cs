using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace AirHockey
{
    public partial class Form1 : Form
    {


        #region References
        SoundPlayer bounce = new SoundPlayer(Properties.Resources._456563__bumpelsnake__bounce1);
        SoundPlayer horn = new SoundPlayer(Properties.Resources._170825__santino_c__sirene_horn);

        int paddle1X = 145;
        int paddle1Y = 30;
        int player1Score = 0;
        int paddle1SideCollisionX = 145;
        int paddle1SideCollisionY = 30;

        int paddle2X = 145;
        int paddle2Y = 450;
        int player2Score = 0;
        int paddle2SideCollisionX = 145;
        int paddle2SideCollisionY = 450;


        int paddleWidth = 30;
        int paddleHeight = 30;
        int paddleSpeed = 4;


        int ballX = 150;
        int ballY = 250;
        int ballXSpeed = 6;
        int ballYSpeed = 6;
        int ballWidth = 15;
        int ballHeight = 15;
        bool wDown = false;
        bool sDown = false;
        bool aDown = false;
        bool dDown = false;
        bool upArrowDown = false;
        bool downArrowDown = false;
        bool leftArrowDown = false;
        bool rightArrowDown = false;

        bool spaceBarDown = false;
        #endregion

        #region Color Brushes
        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush darkRedBrush = new SolidBrush(Color.DarkRed);
        SolidBrush darkBlueBrush = new SolidBrush(Color.DarkBlue);
        Pen greenPen = new Pen(Color.LimeGreen,5);
        Pen redPen = new Pen(Color.Red, 3);
        Pen bluePen = new Pen(Color.Blue, 3);
        Pen clearPen = new Pen(Color.Transparent, 1);
        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        #region Key Bindings
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                case Keys.Space:
                    spaceBarDown = true;
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.Space:
                    spaceBarDown = true;
                    break;
            }
        }
        #endregion

        #region Drawings
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            
            Graphics g = this.CreateGraphics();
            e.Graphics.FillEllipse(whiteBrush, ballX, ballY, ballWidth, ballHeight);
            e.Graphics.FillEllipse(blueBrush, paddle1X, paddle1Y, paddleWidth, paddleHeight);
            e.Graphics.FillEllipse(redBrush, paddle2X, paddle2Y, paddleWidth, paddleHeight);
            e.Graphics.DrawLine(clearPen, paddle1SideCollisionX, paddle1SideCollisionY, paddle1SideCollisionX, paddle1SideCollisionY + paddleHeight);
            e.Graphics.DrawLine(clearPen, paddle1SideCollisionX + paddleWidth, paddle1SideCollisionY, paddle1SideCollisionX + 
                paddleWidth, paddle1SideCollisionY + paddleHeight);
            e.Graphics.DrawLine(clearPen, paddle2SideCollisionX, paddle2SideCollisionY, paddle2SideCollisionX, paddle2SideCollisionY + paddleHeight);
            e.Graphics.DrawLine(clearPen, paddle2SideCollisionX + paddleWidth, paddle2SideCollisionY, paddle2SideCollisionX +
                paddleWidth, paddle2SideCollisionY + paddleHeight);

            e.Graphics.DrawLine(greenPen, 10, 500, 10, 0);
            e.Graphics.DrawLine(greenPen, 310, 500, 310, 0);
            e.Graphics.DrawLine(greenPen, 10, 500, 115, 500);
            e.Graphics.DrawLine(greenPen, 205, 500, 310, 500);
            e.Graphics.DrawLine(greenPen, 10, 0, 115, 0);
            e.Graphics.DrawLine(greenPen, 205, 0, 310, 0);
            e.Graphics.DrawLine(redPen, 12, 250, 309, 250);
            e.Graphics.DrawLine(bluePen, 12, 125, 309, 125);
            e.Graphics.DrawLine(bluePen, 12, 375, 309, 375);
            #endregion

        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            #region Ball Movement
            int x = ballX;
            int y = ballY;

            if (spaceBarDown == true)
            {
                //move ball 
                ballX += ballXSpeed;
                ballY += ballYSpeed;
            }
            #endregion

            #region Paddle Movement
            //move player 1 
            if (wDown == true && paddle1Y > 0)
            {
                paddle1Y -= paddleSpeed;
                paddle1SideCollisionY -= paddleSpeed;
            }

            if (sDown == true && paddle1Y < this.Height - paddleHeight && paddle1Y < 250 - paddleHeight)
            {
                paddle1Y += paddleSpeed;
                paddle1SideCollisionY += paddleSpeed;
            }
            if (aDown == true && paddle1X > 10)
            {
                paddle1X -= paddleSpeed;
                paddle1SideCollisionX -= paddleSpeed;
            }
            if (dDown == true && paddle1X < 310 - paddleWidth)
            {
                paddle1X += paddleSpeed;
                paddle1SideCollisionX += paddleSpeed;
            }


            //move player 2 
            if (upArrowDown == true && paddle2Y > 0 && paddle2Y > 250)
            {
                paddle2Y -= paddleSpeed;
                paddle2SideCollisionY -= paddleSpeed;
            }

            if (downArrowDown == true && paddle2Y < this.Height - paddleHeight)
            {
                paddle2Y += paddleSpeed;
                paddle2SideCollisionY += paddleSpeed;
            }
            if (leftArrowDown == true && paddle2X > 10)
            {
                paddle2X -= paddleSpeed;
                paddle2SideCollisionX -= paddleSpeed;
            }
            if (rightArrowDown == true && paddle2X < 310 - paddleWidth)
            {
                paddle2X += paddleSpeed;
                paddle2SideCollisionX += paddleSpeed;
            }
            #endregion

            #region Ball Colisions
            //Check for ball collision with sides
            if (ballY < 0 && ballX < 115 || ballY < 0 && ballX > 205 || ballY > 480 && ballX < 115 || ballY > 480 && ballX > 205)
            {
                ballX = x;
                ballY = y;
                ballYSpeed *= -1;  // or: ballYSpeed = -ballYSpeed; 
                bounce.Play();
            }
            if (ballX > 310 - ballWidth)
            {
                ballXSpeed *= -1;
                ballX -= ballWidth;
                bounce.Play();
            }
            if (ballX < 10)
            {
                ballXSpeed *= -1;
                ballX += ballWidth;
                bounce.Play();
            }
            #endregion

            #region Paddle and Ball Drawings
            //create Rectangles of objects on screen to be used for collision detection 
            Rectangle player1Rec = new Rectangle(paddle1X, paddle1Y, paddleWidth, paddleHeight);
                Rectangle player2Rec = new Rectangle(paddle2X, paddle2Y, paddleWidth, paddleHeight);
                Rectangle ballRec = new Rectangle(ballX, ballY, ballWidth, ballHeight);
            Rectangle paddle1RightWall = new Rectangle(paddle1SideCollisionX + paddleWidth, paddle1SideCollisionY, 1, paddleHeight + 5);
            Rectangle paddle1LeftWall = new Rectangle(paddle1SideCollisionX, paddle1SideCollisionY, 1, paddleHeight + 5);
            Rectangle paddle2RightWall = new Rectangle(paddle2SideCollisionX + paddleWidth, paddle2SideCollisionY, 1, paddleHeight + 5);
            Rectangle paddle2LeftWall = new Rectangle(paddle2SideCollisionX, paddle2SideCollisionY, 1, paddleHeight + 5);


            //check if ball hits either paddle. If it does change the direction 
            //and place the ball in front of the paddle hit 
            #endregion

            #region Paddle Intersections
            if (player1Rec.IntersectsWith(ballRec))
            {

                ballX = x;
                ballY = y;
                ballYSpeed *= -1;
                
                               
                //ballY = paddle1Y + ballHeight + 1;
                bounce.Play();
            }
            if (paddle1RightWall.IntersectsWith(ballRec))
            {
                
                ballXSpeed *= -1;
                ballX += ballWidth;
            }
            if (paddle1LeftWall.IntersectsWith(ballRec))
            {
                
                ballXSpeed *= -1;
                ballX -= ballWidth;
            }

            if (player2Rec.IntersectsWith(ballRec))
            {
                ballX = x;
                ballY = y;
                ballYSpeed *= -1;
                
                bounce.Play();
                
            }
            if (paddle2RightWall.IntersectsWith(ballRec))
            {
                
                ballXSpeed *= -1;
                ballX += ballWidth;
            }
            if (paddle2LeftWall.IntersectsWith(ballRec))
            {
                
                ballXSpeed *= -1;
                ballX -= ballWidth;
            }
            #endregion

            #region Scoring
            //Check if either player scores a point
            if (ballY < 0 && ballX > 115 && ballX < 205)
            {
                player2Score++;

                p2ScoreLabel.Text = $"{player2Score}";



                ballX = 150;
                ballY = 250;

                paddle1X = 150;
                paddle2X = 150;
                paddle1Y = 20;
                paddle2Y = 450;
                paddle1SideCollisionX = 150;
                paddle1SideCollisionY = 20;
                paddle2SideCollisionX = 150;
                paddle2SideCollisionY = 450;

                horn.Play();
            }
            if (ballY > 500 - ballHeight && ballX > 115 && ballX < 205)
            {
                player1Score++;

                p1ScoreLabel.Text = $"{player1Score}";



                ballX = 150;
                ballY = 250;

                paddle1X = 150;
                paddle2X = 150;
                paddle1Y = 20;
                paddle2Y = 450;
                paddle1SideCollisionX = 150;
                paddle1SideCollisionY = 20;
                paddle2SideCollisionX = 150;
                paddle2SideCollisionY = 450;
                horn.Play();
            }


            //Check if either player won
            if (player1Score == 3 || player2Score == 3)
            {
                gameTimer.Enabled = false;
            }

            Refresh();
            #endregion
        }
    }
}

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
    // Matthew Barber
    // March 11, 2021
    // Air Hockey
    public partial class Form1 : Form
    {


        #region References
        SoundPlayer bounce = new SoundPlayer(Properties.Resources._456563__bumpelsnake__bounce1);
        SoundPlayer horn = new SoundPlayer(Properties.Resources._170825__santino_c__sirene_horn);

        // Paddles
        int paddle1X = 150;
        int paddle1Y = 20;
        int player1Score = 0;
        int paddle1SideCollisionX = 145;
        int paddle1SideCollisionY = 30;

        int paddle2X = 150;
        int paddle2Y = 450;
        int player2Score = 0;
        int paddle2SideCollisionX = 145;
        int paddle2SideCollisionY = 450;
        int sideCollisionWidth = 1;


        int paddleWidth = 30;
        int paddleHeight = 30;
        int paddleSpeed = 4;

        // Ball Sizes
        int ballX = 150;
        int ballY = 250 - 15 / 2;
        int ballXSpeed = 6;
        int ballYSpeed = 6;
        int ballWidth = 15;
        int ballHeight = 15;

        // Arena Lines
        int leftWallX = 10;
        int rightWallX = 310;
        int top = 0;
        int bottom = 500;
        int leftGoalLineX = 115;
        int rightGoalLineX = 205;
        int topBlueLineY = 125;
        int redLineY = 250;
        int bottomBlueLineY = 375;
        int goalLineY = 480;

        // Key Bindings
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
        Pen greenPen = new Pen(Color.LimeGreen, 5);
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
            e.Graphics.DrawLine(greenPen, leftWallX, bottom, leftGoalLineX, bottom);
            e.Graphics.DrawLine(greenPen, rightGoalLineX, bottom, rightWallX, bottom);
            e.Graphics.DrawLine(greenPen, leftWallX, top, leftGoalLineX, top);
            e.Graphics.DrawLine(greenPen, rightGoalLineX, top, rightWallX, top);
            e.Graphics.DrawLine(redPen, leftWallX, redLineY, rightWallX, redLineY);
            e.Graphics.DrawLine(bluePen, leftWallX, topBlueLineY, rightWallX, topBlueLineY);
            e.Graphics.DrawLine(bluePen, leftWallX, bottomBlueLineY, rightWallX, bottomBlueLineY);
            e.Graphics.DrawLine(greenPen, leftWallX, bottom, leftWallX, top);
            e.Graphics.DrawLine(greenPen, rightWallX, bottom, rightWallX, top);
            e.Graphics.FillEllipse(whiteBrush, ballX, ballY, ballWidth, ballHeight);
            e.Graphics.FillEllipse(blueBrush, paddle1X, paddle1Y, paddleWidth, paddleHeight);
            e.Graphics.FillEllipse(redBrush, paddle2X, paddle2Y, paddleWidth, paddleHeight);
            e.Graphics.DrawLine(clearPen, paddle1SideCollisionX, paddle1SideCollisionY, paddle1SideCollisionX, paddle1SideCollisionY + paddleHeight);
            e.Graphics.DrawLine(clearPen, paddle1SideCollisionX + paddleWidth, paddle1SideCollisionY, paddle1SideCollisionX +
                paddleWidth, paddle1SideCollisionY + paddleHeight);
            e.Graphics.DrawLine(clearPen, paddle2SideCollisionX, paddle2SideCollisionY, paddle2SideCollisionX, paddle2SideCollisionY + paddleHeight);
            e.Graphics.DrawLine(clearPen, paddle2SideCollisionX + paddleWidth, paddle2SideCollisionY, paddle2SideCollisionX +
                paddleWidth, paddle2SideCollisionY + paddleHeight);


            #endregion

        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            winnerLabel.Text = "Press SpaceBar To Begin";
            #region Ball Movement
            int x = ballX;
            int y = ballY;

            if (spaceBarDown == true)
            {
                winnerLabel.Text = "";
                //move ball 
                ballX += ballXSpeed;
                ballY += ballYSpeed;
            }
            #endregion

            #region Paddle Movement
            //move player 1 
            if (wDown == true && paddle1Y > top)
            {
                paddle1Y -= paddleSpeed;
                paddle1SideCollisionY -= paddleSpeed;
            }

            if (sDown == true && paddle1Y < this.Height - paddleHeight && paddle1Y < redLineY - paddleHeight)
            {
                paddle1Y += paddleSpeed;
                paddle1SideCollisionY += paddleSpeed;
            }
            if (aDown == true && paddle1X > leftWallX)
            {
                paddle1X -= paddleSpeed;
                paddle1SideCollisionX -= paddleSpeed;
            }
            if (dDown == true && paddle1X < rightWallX - paddleWidth)
            {
                paddle1X += paddleSpeed;
                paddle1SideCollisionX += paddleSpeed;
            }


            //move player 2 
            if (upArrowDown == true && paddle2Y > top && paddle2Y > redLineY)
            {
                paddle2Y -= paddleSpeed;
                paddle2SideCollisionY -= paddleSpeed;
            }

            if (downArrowDown == true && paddle2Y < this.Height - paddleHeight)
            {
                paddle2Y += paddleSpeed;
                paddle2SideCollisionY += paddleSpeed;
            }
            if (leftArrowDown == true && paddle2X > leftWallX)
            {
                paddle2X -= paddleSpeed;
                paddle2SideCollisionX -= paddleSpeed;
            }
            if (rightArrowDown == true && paddle2X < rightWallX - paddleWidth)
            {
                paddle2X += paddleSpeed;
                paddle2SideCollisionX += paddleSpeed;
            }
            #endregion

            #region Ball Colisions
            //Check for ball collision with sides
            if (ballY < top && ballX < leftGoalLineX || ballY < top && ballX > rightGoalLineX ||
                ballY > goalLineY && ballX < leftGoalLineX || ballY > goalLineY && ballX > rightGoalLineX)
            {
                ballX = x;
                ballY = y;
                ballYSpeed *= -1;  // or: ballYSpeed = -ballYSpeed; 
                bounce.Play();
            }
            if (ballX > rightWallX - ballWidth)
            {
                ballXSpeed *= -1;
                ballX -= ballWidth;
                bounce.Play();
            }
            if (ballX < leftWallX)
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
            Rectangle paddle1RightWall = new Rectangle(paddle1SideCollisionX + paddleWidth, paddle1SideCollisionY, sideCollisionWidth, paddleHeight);
            Rectangle paddle1LeftWall = new Rectangle(paddle1SideCollisionX, paddle1SideCollisionY, sideCollisionWidth, paddleHeight);
            Rectangle paddle2RightWall = new Rectangle(paddle2SideCollisionX + paddleWidth, paddle2SideCollisionY, sideCollisionWidth, paddleHeight);
            Rectangle paddle2LeftWall = new Rectangle(paddle2SideCollisionX, paddle2SideCollisionY, sideCollisionWidth, paddleHeight);


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
            // Player 2 Scoring
            if (ballY < top && ballX > leftGoalLineX && ballX < rightGoalLineX)
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

            // Player 1 Scoring
            if (ballY > bottom - ballHeight && ballX > leftGoalLineX && ballX < rightGoalLineX)
            {
                player1Score++;

                p1ScoreLabel.Text = $"{player1Score}";



                ballX = 150;
                ballY = 250 - 15 / 2;

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

                if (player1Score == 3)
                {
                    winnerLabel.Text = "Blue Team Wins";
                }
                if (player2Score == 3)
                {
                    winnerLabel.Text = "Red Team Wins";
                }
            }

            Refresh();
            #endregion
        }
    }
}

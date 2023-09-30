﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    /// <summary>
    /// The gameForm is just the UI that displays the game,
    /// which is the "game" object passed in with the constructor.
    /// The game can be interracted with to change turns, mark boxes,
    /// and display wins and losses.
    /// </summary>
    public partial class gameForm : Form
    {
        Game game;
        GameType gameType;

        public gameForm(Game newGame)
        {
            game = newGame;
            InitializeComponent();
        }

        private void gameFormLoad(object sender, EventArgs e) {}

        //
        // Click events
        //
        private void topLeft_Click(object sender, EventArgs e)
        {
            Tuple<int, int> position = new Tuple<int, int>(0, 0);
            makeMove(position, topLeft);
        }

        private void topTop_Click(object sender, EventArgs e)
        {
            Tuple<int, int> position = new Tuple<int, int>(0, 1);
            makeMove(position, topMiddle);
        }

        private void topRight_Click(object sender, EventArgs e)
        {
            Tuple<int, int> position = new Tuple<int, int>(0, 2);
            makeMove(position, topRight);
        }

        private void middleLeft_Click(object sender, EventArgs e)
        {
            Tuple<int, int> position = new Tuple<int, int>(1, 0);
            makeMove(position, middleLeft);
        }

        private void middleMiddle_Click(object sender, EventArgs e)
        {
            Tuple<int, int> position = new Tuple<int, int>(1, 1);
            makeMove(position, middleMiddle);
        }

        private void middleRight_Click(object sender, EventArgs e)
        {
            Tuple<int, int> position = new Tuple<int, int>(1, 2);
            makeMove(position, middleRight);
        }

        private void bottomLeft_Click(object sender, EventArgs e)
        {
            Tuple<int, int> position = new Tuple<int, int>(2, 0);
            makeMove(position, bottomLeft);
        }

        private void bottomMiddle_Click(object sender, EventArgs e)
        {
            Tuple<int, int> position = new Tuple<int, int>(2, 1);
            makeMove(position, bottomMiddle);
        }

        private void bottomRight_Click(object sender, EventArgs e)
        {
            Tuple<int, int> position = new Tuple<int, int>(2, 2);
            makeMove(position, bottomRight);
        }

        // Game mechancis
        private void makeMove(Tuple<int, int> position, PictureBox space, bool computer = false)
        {
            fillSpace(space, game.turn);
            game.makeMove(position, game.turn);
            if (checkForGameOver())
                return;

            // If it's a one player game, and the human just went, let the computer go...
            if (gameType != GameType.TWO_PLAYER && computer == false)
            {
                // If IMPOSSIBLE, do the algorithm >:)
                if (gameType == GameType.ONE_PLAYER_IMPOSSIBLE)
                {
                    game.changeTurn();
                    int pos = game.impossibleComputerMove();
                    computerMove(pos);
                }
                // Else place randomly.
                else
                {
                    game.changeTurn();
                    int pos = game.computerMove();
                    computerMove(pos);
                }
                if (checkForGameOver())
                    return;
            }

            game.changeTurn();
            turnLabelName.Text = game.turn.ToString();

            Console.WriteLine(game.turn);
        }

        private void fillSpace(PictureBox space, Letter letter)
        {
            // If the turn is O, set the letter to O
            if (letter == Letter.O)
            {
                space.Image = global::TicTacToe.Properties.Resources.O;
                space.Enabled = false;
            }
            // Else set it to X
            else
            {
                space.Image = global::TicTacToe.Properties.Resources.x;
                space.Enabled = false;
            }
        }

        private void computerMove(int space)
        {
            switch (space)
            {
                case 0:
                    fillSpace(topLeft, Letter.X);
                    break;
                case 1:
                    fillSpace(topMiddle, Letter.X);
                    break;
                case 2:
                    fillSpace(topRight, Letter.X);
                    break;
                case 3:
                    fillSpace(middleLeft, Letter.X);
                    break;
                case 4:
                    fillSpace(middleMiddle, Letter.X);
                    break;
                case 5:
                    fillSpace(middleRight, Letter.X);
                    break;
                case 6:
                    fillSpace(bottomLeft, Letter.X);
                    break;
                case 7:
                    fillSpace(bottomMiddle, Letter.X);
                    break;
                case 8:
                    fillSpace(bottomRight, Letter.X);
                    break;
            }
        }

        private bool checkForGameOver()
        {
            // Check if the move is a victory
            if (game.checkForWin(game.turn))
            {
                gameOver(game.turn);
                return true;
            }
            // If there's a CAT (no winner), pass in NONE to gameOver.
            else if (game.checkForCat())
            {
                gameOver(Letter.NONE);
                return true;
            }
            return false;
        }

        /// <summary>
        /// When the game ends, lock the board and display the ending message.
        /// Unhide the "New Game" button.
        /// </summary>
        private void gameOver(Letter victor)
        {
            // Lock the board.
            topLeft.Enabled = false;
            topMiddle.Enabled = false;
            topRight.Enabled = false;
            middleLeft.Enabled = false;
            middleMiddle.Enabled = false;
            middleRight.Enabled = false;
            bottomLeft.Enabled = false;
            bottomMiddle.Enabled = false;
            bottomRight.Enabled = false;

            turnLabel.Visible = false;
            turnLabelName.Visible = false;

            // Display message.
            if (victor != Letter.NONE)
                gameOverMessage.Text = victor.ToString() + " WINS!!!";
            else
                gameOverMessage.Text = "CAT!";
            gameOverMessage.Visible = true;

            // Display start game group box.
            startGameGroup.Visible = true;
        }

        /// <summary>
        /// Empty the board of all previous moves.
        /// </summary>
        private void clearBoard()
        {
            topLeft.Image = null;
            topMiddle.Image = null;
            topRight.Image = null;
            middleLeft.Image = null;
            middleMiddle.Image = null;
            middleRight.Image = null;
            bottomLeft.Image = null;
            bottomMiddle.Image = null;
            bottomRight.Image = null;
        }

        private void startGame()
        {
            // Enable ability to select picture boxes
            topLeft.Enabled = true;
            topMiddle.Enabled = true;
            topRight.Enabled = true;
            middleLeft.Enabled = true;
            middleMiddle.Enabled = true;
            middleRight.Enabled = true;
            bottomLeft.Enabled = true;
            bottomMiddle.Enabled = true;
            bottomRight.Enabled = true;

            // Unhide the picture boxes and grid
            topLeft.Visible = true;
            topMiddle.Visible = true;
            topRight.Visible = true;
            middleLeft.Visible = true;
            middleMiddle.Visible = true;
            middleRight.Visible = true;
            bottomLeft.Visible = true;
            bottomMiddle.Visible = true;
            bottomRight.Visible = true;
            turnLabel.Visible = true;
            turnLabelName.Visible = true;
            lineLeft.Visible = true;
            lineRight.Visible = true;
            lineTop.Visible = true;
            lineBottom.Visible = true;

            // Hide / display turn display
            turnLabel.Visible = (gameType == GameType.TWO_PLAYER);
            turnLabelName.Visible = (gameType == GameType.TWO_PLAYER);

            turnLabel.Visible = (gameType == GameType.TWO_PLAYER);
            turnLabelName.Visible = (gameType == GameType.TWO_PLAYER);

            startButton.Text = "New Game";
            startGameGroup.Location = new Point(300, 121);

            // Start the game.
            game.startGame(gameType);

            // If one player, make first computer move.
            if (gameType == GameType.ONE_PLAYER_IMPOSSIBLE)
            {
                fillSpace(topLeft, Letter.X);
                game.makeMove(new Tuple<int, int>(0, 0), Letter.X);
            }
            else if (gameType == GameType.ONE_PLAYER)
            {
                int pos = game.computerMove();
                computerMove(pos);
            }
            else
            {
                turnLabelName.Text = Letter.X.ToString();
            }
        }

        /// <summary>
        ///  Hide the start button and start the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startButton_Clicked(object sender, EventArgs e)
        {
            clearBoard();
            gameOverMessage.Visible = false;

            // Hide start button and start game
            startGameGroup.Visible = false;
            startGame();
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (onePlayerRadio.Checked)
                gameType = GameType.ONE_PLAYER;
            else if (onePlayerImpossibleRadio.Checked)
                gameType = GameType.ONE_PLAYER_IMPOSSIBLE;
            else if (twoPlayerRadio.Checked)
                gameType = GameType.TWO_PLAYER;
            startButton.Enabled = onePlayerRadio.Checked || onePlayerImpossibleRadio.Checked || twoPlayerRadio.Checked;
        }
    }
}

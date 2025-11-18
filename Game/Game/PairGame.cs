// <copyright file="PairGame.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

namespace PairGame;

using System;
using System.Drawing;
using System.Windows.Forms;

/// <summary>
/// Main form for the Memory Game (Find the Pair).
/// </summary>
public class PairGame : Form
{
    private static readonly Color ColorHidden = SystemColors.Control;
    private static readonly Color ColorRevealedTemp = Color.Yellow;
    private static readonly Color ColorRevealedPerm = Color.LightGreen;
    private static readonly Color ColorTextTemp = Color.Black;
    private static readonly Color ColorTextPerm = Color.DarkGreen;

    private readonly int n;
    private readonly Button[,] buttons;
    private readonly GameLogic gameLogic;
    private readonly System.Windows.Forms.Timer revealTimer;
    private readonly System.Windows.Forms.Timer gameTimer;
    private readonly DateTime startTime;

    private Button? firstButton;
    private Button? secondButton;
    private int pairsFound;
    private int failedAttempts;
    private Label? timerLabel;
    private Label? attemptsLabel;

    /// <summary>
    /// Initializes a new instance of the <see cref="PairGame"/> class.
    /// </summary>
    /// <param name="args">Command line arguments. First argument is grid size N (even).</param>
    public PairGame(string[] args)
    {
        if (args.Length == 0 || !int.TryParse(args[0], out this.n) || this.n % 2 != 0 || this.n < 2 || this.n > 12)
        {
            throw new ArgumentException(
                "Usage: Game.exe N\nN must be even number between 2 and 12",
                nameof(args));
        }

        this.gameLogic = new GameLogic(this.n);
        this.buttons = new Button[this.n, this.n];

        this.revealTimer = new System.Windows.Forms.Timer { Interval = 800 };
        this.revealTimer.Tick += this.RevealTimer_Tick;

        this.gameTimer = new System.Windows.Forms.Timer { Interval = 1000 };
        this.gameTimer.Tick += this.GameTimer_Tick;

        this.startTime = DateTime.Now;

        this.InitializeGame();
    }

    private void InitializeGame()
    {
        this.Text = $"Find the Pair ({this.n}x{this.n})";
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.StartPosition = FormStartPosition.CenterScreen;

        int buttonSize = Math.Min(500 / this.n, 80);
        int padding = 5;
        int gridWidth = (this.n * (buttonSize + padding)) + padding;
        int gridHeight = (this.n * (buttonSize + padding)) + padding;
        int panelHeight = 60;

        this.ClientSize = new Size(gridWidth, gridHeight + panelHeight);

        Panel statsPanel = new Panel
        {
            Dock = DockStyle.Bottom,
            Height = panelHeight,
            BackColor = Color.FromArgb(240, 240, 240),
        };

        this.timerLabel = new Label
        {
            Text = "Time: 0 sec",
            AutoSize = true,
            Location = new Point(15, 15),
            Font = new Font("Segoe UI", 10F, FontStyle.Regular),
        };

        this.attemptsLabel = new Label
        {
            Text = "Failed attempts: 0",
            AutoSize = true,
            Location = new Point(15, 35),
            Font = new Font("Segoe UI", 10F, FontStyle.Regular),
        };

        statsPanel.Controls.Add(this.timerLabel);
        statsPanel.Controls.Add(this.attemptsLabel);
        this.Controls.Add(statsPanel);
        int index = 0;
        for (int i = 0; i < this.n; i++)
        {
            for (int j = 0; j < this.n; j++)
            {
                var btn = new Button
                {
                    Width = buttonSize,
                    Height = buttonSize,
                    Left = padding + (j * (buttonSize + padding)),
                    Top = padding + (i * (buttonSize + padding)),
                    Font = new Font("Arial", Math.Min(buttonSize / 3f, 24f), FontStyle.Bold),
                    BackColor = ColorHidden,
                    ForeColor = Color.Navy,
                    Tag = new Point(i, j),
                    UseVisualStyleBackColor = false,
                };
                btn.Click += this.Button_Click;
                this.buttons[i, j] = btn;
                this.Controls.Add(btn);
                index++;
            }
        }

        this.gameTimer.Start();
        this.pairsFound = 0;
        this.failedAttempts = 0;
        this.firstButton = null;
        this.secondButton = null;
    }

    private void Button_Click(object? sender, EventArgs e)
    {
        if (sender is not Button btn || !btn.Enabled || this.revealTimer.Enabled)
        {
            return;
        }

        Point pos = (Point)btn.Tag!;
        int row = pos.X;
        int col = pos.Y;

        btn.Text = this.gameLogic.GetValue(row, col).ToString();
        btn.BackColor = ColorRevealedTemp;
        btn.ForeColor = ColorTextTemp;
        btn.Enabled = false;

        if (this.firstButton == null)
        {
            this.firstButton = btn;
        }
        else if (this.secondButton == null)
        {
            this.secondButton = btn;
            this.revealTimer.Start();
        }
    }

    private void GameTimer_Tick(object? sender, EventArgs e)
    {
        TimeSpan elapsed = DateTime.Now - this.startTime;
        this.timerLabel!.Text = $"Time: {(int)elapsed.TotalSeconds} sec";
    }

    private void RevealTimer_Tick(object? sender, EventArgs e)
    {
        this.revealTimer.Stop();

        Point p1 = (Point)this.firstButton!.Tag!;
        Point p2 = (Point)this.secondButton!.Tag!;

        if (this.gameLogic.IsPair(p1.X, p1.Y, p2.X, p2.Y))
        {
            this.firstButton.BackColor = ColorRevealedPerm;
            this.firstButton.ForeColor = ColorTextPerm;
            this.firstButton.Enabled = false;

            this.secondButton.BackColor = ColorRevealedPerm;
            this.secondButton.ForeColor = ColorTextPerm;
            this.secondButton.Enabled = false;

            this.pairsFound++;

            if (this.pairsFound == this.gameLogic.TotalPairs)
            {
                this.gameTimer.Stop();
                TimeSpan totalTime = DateTime.Now - this.startTime;
                int totalSeconds = (int)totalTime.TotalSeconds;

                MessageBox.Show(
                    $"Congratulations! You won!\n\n" +
                    $"Time: {totalSeconds} sec\n" +
                    $"Failed attempts: {this.failedAttempts}",
                    "Victory!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
        else
        {
            this.failedAttempts++;
            this.attemptsLabel!.Text = $"Failed attempts: {this.failedAttempts}";

            this.firstButton.Text = string.Empty;
            this.firstButton.BackColor = ColorHidden;
            this.firstButton.Enabled = true;

            this.secondButton.Text = string.Empty;
            this.secondButton.BackColor = ColorHidden;
            this.secondButton.Enabled = true;
        }

        this.firstButton = null;
        this.secondButton = null;
    }
}

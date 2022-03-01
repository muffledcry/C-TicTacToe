using System;
using System.Linq;

TicTacToe game = new TicTacToe(2, 0, 0);
game.Play();

class TicTacToe
{
    public int Rounds {get; set;}
    public Player[]? Players {get; }
    public Board? GameBoard {get;}
    public bool PlayGame {get;}

    public TicTacToe(int rounds, int playerOneScore, int playerTwoScore)
    {
        Rounds = rounds;
        Players = new Player[2];
        GameBoard = new Board();
        PlayGame = true;

        Players[0]  = new Player('X', playerOneScore);
        Players[1] = new Player('O', playerTwoScore);

        System.Random generator = new System.Random();
        int goesFirst = generator.Next(0,1);
        Players[goesFirst].HasTurn = true;
    }

    public bool CheckWin(char mark)
    {
        if (
            // check horizontals for X
            (GameBoard?.RowOne?[0] == mark && GameBoard.RowOne[1] == mark && GameBoard.RowOne[2] == mark) |
            (GameBoard?.RowTwo?[0] == mark && GameBoard.RowTwo[1] == mark && GameBoard.RowTwo[2] == mark) | 
            (GameBoard?.RowThree?[0] == mark && GameBoard.RowThree[1] == mark && GameBoard.RowThree[2] == mark) |
            // check verticals for X 
            (GameBoard?.RowOne?[0] == mark && GameBoard?.RowTwo?[0] == mark && GameBoard?.RowThree?[0] == mark) | 
            (GameBoard?.RowOne?[1] == mark && GameBoard?.RowTwo?[1] == mark && GameBoard?.RowThree?[1] == mark) | 
            (GameBoard?.RowOne?[2] == mark && GameBoard?.RowTwo?[2] == mark && GameBoard?.RowThree?[2] == mark) |
            // check diagonals for X
            (GameBoard?.RowOne?[0] == mark && GameBoard?.RowTwo?[1] == mark && GameBoard?.RowThree?[2] == mark) |
            (GameBoard?.RowOne?[2] == mark && GameBoard?.RowTwo?[1] == mark && GameBoard?.RowThree?[0] == mark)
            ) 
            return true;
        else 
            return false;
    }

    public char GetChoice(Player  player)
    {
        Console.WriteLine($"It's {player.Mark}'s turn.");
        Console.WriteLine("Where will you place your mark?");
        char choice = Convert.ToChar(Console.ReadLine());
        return choice;
    }

    public bool Available()
    {
        Console.WriteLine($"{GameBoard?.Redraws} redraws.");
        if (GameBoard?.Redraws < 9) return true;
        else return false;
    }

    public void Play()
    {
        bool play = true;
        while (play)
        {
            foreach (Player player in Players)
            {    
                bool win = CheckWin(player.Mark);
                if (win) 
                {
                    player.Score +=1;
                    Console.WriteLine("Tic Tac Toe: Three in a row!");
                    Console.WriteLine($"{player.Mark} has won!");
                    Console.WriteLine("\nPress enter to continue.");
                    Console.Clear();

                    Console.WriteLine("--------------");
                    Console.WriteLine("SCOREBOARD:");
                    Console.WriteLine("--------------");
                    Console.WriteLine($"{Players[0].Mark} has {Players[0].Score} points.");
                    Console.WriteLine($"{Players[1].Mark} has {Players[1].Score} points.");
                    Console.WriteLine("\n Press enter to start a new game.");
                    play = false;
                }
            }
            Console.Clear();
            bool canPlay = Available();
            if (canPlay)
            {
                GameBoard?.drawBoard();


                foreach (Player player in Players)
                {
                    if (player.HasTurn == true)
                    {
                        char choice = GetChoice(player);
                        GameBoard?.update(choice, player.Mark);
                        player.HasTurn = false;
                    }
                    else player.HasTurn = true;
                }
            }
            else 
            {
                Console.WriteLine("Draw.");
                play = false;
            }
        }
        Console.WriteLine("Would you like to play another round?");
        Console.WriteLine("Enter 1 for yes.");
        Console.WriteLine("Enter 2 for no.");
        string answer = Console.ReadLine();
        if (answer == "1")
        {
            Rounds +=1;
            TicTacToe game = new TicTacToe(Rounds, Players[0].Score, Players[1].Score);
            game.Play();
        }
    }
}

class Player
{
    public int Score {get; set;}
    public char Mark {get;}
    public bool HasTurn {get; set;}

    public Player(char mark, int score)
    {
        Score = score;
        Mark = mark;
        HasTurn = false;
    }
}

class Board
{
    public char[]? RowOne {get;}
    public char[]? RowTwo {get;}
    public char[]? RowThree {get;}

    public int Redraws {get; set;}

    public Board()
    {
        RowOne = new char[3];
        RowTwo = new char[3];
        RowThree = new char[3];
        Redraws = 0;

        RowOne[0] = '7';
        RowOne[1] = '8';
        RowOne[2] = '9';

        RowTwo[0] = '4';
        RowTwo[1] = '5';
        RowTwo[2] = '6';

        RowThree[0] = '1';
        RowThree[1] = '2';
        RowThree[2] = '3';
    }

    public void drawBoard()
    {
        Console.WriteLine($"{RowOne?[0]} | {RowOne?[1]} | {RowOne?[2]}");
        Console.WriteLine("---------");
        Console.WriteLine($"{RowTwo?[0]} | {RowTwo?[1]} | {RowTwo?[2]}");
        Console.WriteLine("---------");
        Console.WriteLine($"{RowThree?[0]} | {RowThree?[1]} | {RowThree?[2]}");
        Redraws += 1;
    }

    public void update(char choice, char mark)
    {
        if (choice == '7') {RowOne[0] = mark;}
        if (choice == '8') {RowOne[1] = mark;}
        if (choice == '9') {RowOne[2] = mark;}
        if (choice == '4') {RowTwo[0] = mark;}
        if (choice == '5') {RowTwo[1] = mark;}
        if (choice == '6') {RowTwo[2] = mark;}
        if (choice == '1') {RowThree[0] = mark;}
        if (choice == '2') {RowThree[1] = mark;}
        if (choice == '3') {RowThree[2] = mark;}
    }
}

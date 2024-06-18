using UnityEngine;
using System.Linq;

public class TicTacToeAI
{
    private readonly GameManager.Player[] board;

    public TicTacToeAI(GameManager.Player[] board)
    {
        this.board = board;
    }

    public int RandomMove()
    {
        int move = -1;

        int[] spaces = { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
        spaces = spaces.OrderBy(x => Random.value).ToArray();

        foreach (int index in spaces)
        {
            if (board[index] == GameManager.Player.None)
            {
                move = index;
                break;
            }
        }




        //for (int i = 0; i < board.Length; i++)
        //{
        //    if (board[i] == GameManager.Player.None)
        //    {
        //        move = i;
        //        break;
        //    }
        //}

        return move;


        /*
         * int[] corners = { 0, 2, 6, 8 };
        corners = corners.OrderBy(x => Random.value).ToArray();

        foreach (int index in corners)
        {
            if (board[index] == GameManager.Player.None)
            {
                return index;
            }
        }
        return -1;
         */
    }

    public int GetBestMove()
    {
        int move = -1;

        if (IsAIFirstMove())
        {
            int center = 4;
            if (board[center] == GameManager.Player.X)
            {
                move = GetCornerMove();
            }
            else
            {
                move = center;
            }
        }

        if (move == -1)
        {
            move = FindWinningMove(GameManager.Player.O);
        }

        if (move == -1)
        {
            move = FindWinningMove(GameManager.Player.X);
        }

        if (move == -1)
        {
            for (int i = 0; i < board.Length; i++)
            {
                if (board[i] == GameManager.Player.None)
                {
                    move = i;
                    break;
                }
            }
        }

        return move;
    }

    private bool IsAIFirstMove()
    {
        int filledCells = 0;
        foreach (var cell in board)
        {
            if (cell == GameManager.Player.O)
            {
                filledCells++;
            }
        }

        return filledCells == 0;
    }

    private int GetCornerMove()
    {
        int[] corners = { 0, 2, 6, 8 };
        corners = corners.OrderBy(x => Random.value).ToArray();

        foreach (int index in corners)
        {
            if (board[index] == GameManager.Player.None)
            {
                return index;
            }
        }
        return -1;
    }

    private int FindWinningMove(GameManager.Player player)
    {
        int[,] winConditions = new int[,]
        {
            {0, 1, 2}, {3, 4, 5}, {6, 7, 8}, // rows
            {0, 3, 6}, {1, 4, 7}, {2, 5, 8}, // columns
            {0, 4, 8}, {2, 4, 6}             // diagonals
        };

        for (int i = 0; i < winConditions.GetLength(0); i++)
        {
            int a = winConditions[i, 0];
            int b = winConditions[i, 1];
            int c = winConditions[i, 2];

            if (board[a] == player && board[b] == player && board[c] == GameManager.Player.None)
            {
                return c;
            }
            if (board[a] == player && board[b] == GameManager.Player.None && board[c] == player)
            {
                return b;
            }
            if (board[a] == GameManager.Player.None && board[b] == player && board[c] == player)
            {
                return a;
            }
        }

        return -1;
    }
}

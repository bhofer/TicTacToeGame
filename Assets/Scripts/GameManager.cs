using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public enum Player { None, X, O }
    public Player currentPlayer;
    public Player[] board = new Player[9];
    public Button[] buttons;
    public Text winnerText;
    public Text currentPlayerText;
    public GameObject endScreen;
    public GameObject startScreen;
    public GameObject gameScreen;
    public float aiDelay = 1f;

    private TicTacToeAI ai;
    private bool vsAI = false;
    private bool aiTurn = false;
    private bool rivalMode = false;

    private void Start()
    {
        ShowStartScreen();
    }

    public void StartGame(bool vsAI, bool rivalMode = false)
    {
        aiTurn = false;
        this.vsAI = vsAI;
        this.rivalMode = rivalMode;
        currentPlayer = Player.X;
        currentPlayerText.text = currentPlayer.ToString() + "'s TURN!";
        ClearBoard();
        ShowGameScreen();

        if (vsAI)
        {
            ai = new TicTacToeAI(board);
        }
    }

    public void CellClicked(int index)
    {
        if (aiTurn)
        {
            return;
        }

        if (board[index] == Player.None)
        {
            board[index] = currentPlayer;
            buttons[index].GetComponentInChildren<Text>().text = currentPlayer.ToString();

            if (CheckWin(currentPlayer))
            {
                EndGame(currentPlayer.ToString() + " WINS!");
            }
            else if (CheckDraw())
            {
                EndGame("DRAW PARTNER!");
            }
            else
            {
                SwitchPlayer();

                if (vsAI && currentPlayer == Player.O)
                {
                    aiTurn = true;
                    StartCoroutine(AIMoveWithDelay());
                }
            }
        }
    }

    private IEnumerator AIMoveWithDelay()
    {
        yield return new WaitForSeconds(aiDelay);
        AIMove();
    }

    private void AIMove()
    {
        aiTurn = false;
        int move;

        if (rivalMode)
        {
            move = ai.GetBestMove();
        }
        else
        {
            move = ai.RandomMove();
        }

        CellClicked(move);
    }

    private void SwitchPlayer()
    {
        currentPlayer = (currentPlayer == Player.X) ? Player.O : Player.X;
        currentPlayerText.text = currentPlayer.ToString() + "'s TURN!";
    }

    private bool CheckWin(Player player)
    {
        int[,] winConditions = new int[,]
        {
            {0, 1, 2}, {3, 4, 5}, {6, 7, 8}, // rows
            {0, 3, 6}, {1, 4, 7}, {2, 5, 8}, // columns
            {0, 4, 8}, {2, 4, 6}             // diagonals
        };

        for (int i = 0; i < winConditions.GetLength(0); i++)
        {
            if (board[winConditions[i, 0]] == player &&
                board[winConditions[i, 1]] == player &&
                board[winConditions[i, 2]] == player)
            {
                return true;
            }
        }

        return false;
    }

    private bool CheckDraw()
    {
        foreach (var cell in board)
        {
            if (cell == Player.None)
            {
                return false;
            }
        }

        return true;
    }

    private void EndGame(string result)
    {
        winnerText.text = result;
        gameScreen.SetActive(false);
        endScreen.SetActive(true);
    }

    public void GoHome()
    {
        ClearBoard();
        ShowStartScreen();
    }

    public void ReplayGame()
    {
        StartGame(vsAI, rivalMode);
    }

    private void ClearBoard()
    {
        StopAllCoroutines();

        for (int i = 0; i < board.Length; i++)
        {
            board[i] = Player.None;
            buttons[i].GetComponentInChildren<Text>().text = "";
        }
    }

    private void ShowStartScreen()
    {
        startScreen.SetActive(true);
        gameScreen.SetActive(false);
        endScreen.SetActive(false);
    }

    private void ShowGameScreen()
    {
        startScreen.SetActive(false);
        gameScreen.SetActive(true);
        endScreen.SetActive(false);
    }
}

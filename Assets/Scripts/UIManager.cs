using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;

    public void OnHaveABuddyClicked()
    {
        gameManager.StartGame(false);
    }

    public void OnNeedABuddyClicked()
    {
        gameManager.StartGame(true);
    }
    public void OnNeedARivalClicked()
    {
        gameManager.StartGame(true, true);
    }    

    public void OnHomeClicked()
    {
        gameManager.GoHome();
    }

    public void OnReplayClicked()
    {
        gameManager.ReplayGame();
    }

    public void OnCloseClicked()
    {
        Application.Quit();
    }
}

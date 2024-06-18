using UnityEngine;

public class Cell : MonoBehaviour
{
    public int index;
    public GameManager gameManager;

    public void OnCellClicked()
    {
        gameManager.CellClicked(index);
    }
}

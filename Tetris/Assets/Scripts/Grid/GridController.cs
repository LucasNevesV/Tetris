using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridController : MonoBehaviour
{

    public AudioController audioController;
    public GameObject gameOverCanvas;

    public Text scoreText;
    public Text linesText;

    int score = 0;
    int linesCleared = 0;

    Transform[,] grid = 
        new Transform[GameConfig.GridRowSize, GameConfig.GridHeight];

    public void UpdateGrid(Transform shape)
    {

        for (int y = 0; y < GameConfig.GridHeight; y++)
        {
            for (int x = 0; x < GameConfig.GridRowSize; x++)
            {
                if(grid[x, y]?.parent == shape)
                {
                    grid[x, y] = null;
                }
            }
        }

        foreach (Transform block in shape)
        {
            var position = block.position;
            grid[(int)position.x, (int)position.y] = block;
        }

    }

    void IncreaseScore()
    {
        score += 10;
        linesCleared++;

        scoreText.text = score.ToString();
        linesText.text = linesCleared.ToString();
    }

    void GameOver()
    {
        gameOverCanvas.SetActive(true);
    }

    void DeleteRow(int yPosition)
    {
        for (int x = 0; x < GameConfig.GridRowSize; x++)
        {
            Destroy(grid[x, yPosition].gameObject);
            grid[x, yPosition] = null;
        }

        audioController.RowCleared();
    }

    void JumpRow(int yPosition)
    {
        for (int y = yPosition + 1; y < GameConfig.GridHeight; y++)
        {
            for (int x = 0; x < GameConfig.GridRowSize; x++)
            {
                if(grid[x, y] != null)
                {
                    grid[x, y - 1] = grid[x, y];
                    grid[x, y] = null;
                    grid[x, y - 1].transform.position += Vector3.down;
                }
            }
        }
    }

    #region Validations

    public void CheckForTetris()
    {
        var rowsFull = new List<int>();

        for (int y = 0; y < GameConfig.GridHeight; y++)
        {
            if (HasFullRow(y))
            {
                DeleteRow(y);
                IncreaseScore();
                JumpRow(y);
            }
        }
    }

    public bool CheckForGameOver(Transform shape)
    {
        foreach (Transform block in shape)
        {
            if(block.position.y >= 15){
                GameOver();
                return true;
            }
        }
        return false;
    }
    public bool CheckCollision(Transform block)
    {
        var position = block.position;
        Transform cell = grid[(int)position.x, (int)position.y];

        if (cell != null && cell.parent != block.parent)
        {
            return true;
        }

        return false;
    }

    public bool CheckShapeCollision(Transform shape)
    {
        foreach (Transform block in shape)
        {
            var position = block.position;
            Transform cell = grid[(int)position.x, (int)position.y];

            if (cell != null && cell.parent != block.parent)
            {
                return true;
            }
        }
        return false;
    }

    public bool CheckIfPositionIsNull(Vector2 position)
    {

        if (grid[(int)position.x, (int)position.y] != null)
        {
            return true;
        }

        return false;
    }

    bool HasFullRow(int yPosition)
    {
        for (int x = 0; x < GameConfig.GridRowSize; x++)
        {
            if(grid[x, yPosition] == null)
            {
                return false;
            }
        }

        return true;
    }

    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{

    void Update()
    {
        KeyPressed();  
    }

    void KeyPressed()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadMenu();
        }
    }

    #region Actions

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    #endregion
}

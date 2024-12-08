using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverScreenScript : MonoBehaviour
{
    //public GameObject backgroundGameOver;
    
    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void MainMenuBotton()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }
}

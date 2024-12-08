using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WinBackgroundScript : MonoBehaviour
{
    public void MainMenuBotton()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

}

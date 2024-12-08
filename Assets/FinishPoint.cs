using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishPoint : MonoBehaviour
{
    public Image lastRound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

                if (EnemyManager.instance.AreAllEnemiesIsDefeated())
                {
                int sceneIndex = SceneManager.GetActiveScene().buildIndex;
                int totalScenes = SceneManager.sceneCountInBuildSettings;

                if(sceneIndex == totalScenes - 1)
                {
                    lastRound.gameObject.SetActive(true);
                    Time.timeScale = 0;
                }
                else
                {
                    SceneController.instance.NextLevel();
                }
                
                }
            else
            {
                Debug.Log("Cannot proceed. Enemies still alive: " + EnemyManager.instance.getEnemiesCount());
            }
        }
     
    }
}

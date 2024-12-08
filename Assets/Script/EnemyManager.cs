using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public int getEnemiesCount()
    {
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemies");
        return enemyList.Length;
    }
    public bool AreAllEnemiesIsDefeated()
    {
        return getEnemiesCount() == 0;
    }

}

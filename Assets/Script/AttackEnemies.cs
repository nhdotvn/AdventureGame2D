using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;

public class AttackEnemies : MonoBehaviour
{
   
    public int attackEnemiesDamage = 10;
    public Vector2 knockBack = Vector2.zero;

    public void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            attackEnemiesDamage = 10;
        }

        if ( SceneManager.GetActiveScene().buildIndex == 2)
        {
            attackEnemiesDamage = 20;
        }

        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            attackEnemiesDamage = 25;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        
        if (damageable != null)
        {

            Vector2 deliveridKnockBack = transform.parent.localScale.x > 0 ? knockBack : new Vector2(-knockBack.x, knockBack.y);
            bool gotHit = damageable.Hit(attackEnemiesDamage, deliveridKnockBack);
            if (gotHit)
            {
                Debug.Log(collision.name + "Hit for" + attackEnemiesDamage);
            }
            
        }
    }

}

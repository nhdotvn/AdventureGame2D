using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Attack : MonoBehaviour
{
   
    public int attackDamage = 10;
    public Vector2 knockBack = Vector2.zero;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        
        if (damageable != null)
        {

            Vector2 deliveridKnockBack = transform.parent.localScale.x > 0 ? knockBack : new Vector2(-knockBack.x, knockBack.y);
            bool gotHit = damageable.Hit(attackDamage, deliveridKnockBack);
            if (gotHit)
            {
                Debug.Log(collision.name + "Hit for" + attackDamage);
            }
            
        }
    }

}

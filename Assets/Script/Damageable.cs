using Assets.Script.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;
    public UnityEvent damageableDeath;
    public UnityEvent<int, int> healthChanged;

    public Image backgroundGameOver;

    Animator animator;



    [SerializeField]
    private int _maxHealth =100 ;

    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }
    [SerializeField]
    private int _health = 100;

    public int Health
    {
        get
        {
            return _health;

        }
        set
        {
            _health = value;
            healthChanged?.Invoke(_health, MaxHealth);

            if (_health <= 0)
            {
                IsAlive = false;
            }
        }

    }

    [SerializeField]
    private bool _isAlive = true;

    [SerializeField]
    private bool isInvincible;


    private float timeSinceHit =0;
    public float invincibilityTime = 0.25f;

    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        set {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
            Debug.Log("isAlive set" + value);

            if (this.CompareTag("Player") && value == false)
            {
                damageableDeath.Invoke();
                backgroundGameOver.gameObject.SetActive(true);
                Time.timeScale = 0;
                //ShowGameOverScreen();
            }
        }
    }

    //private void ShowGameOverScreen()
    //{
    //    if (backgroundGameOver != null)
    //    {
            
    //    }
    //    else
    //    {
    //        Debug.LogError("GameOverCanvas is not assigned!");
    //    }
    //}

    public bool LockVeloctiy
    {
        get
        {
            return animator.GetBool(AnimationStrings.lockVelocity);
        }
        set
        {
            animator.SetBool(AnimationStrings.lockVelocity, value);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {

        if (isInvincible)
        {
            if(timeSinceHit > invincibilityTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }
        
    }

    public bool Hit(int damage, Vector2 knockBack)
    {

        if(IsAlive && !isInvincible)
        {
           Health -= damage;
            isInvincible = true;

            animator.SetTrigger(AnimationStrings.hitTrigger);
            LockVeloctiy = true;
            damageableHit?.Invoke(damage, knockBack);
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);

            return true;
        }
        return false;
    }
    public bool Heal(int healthRestore)
    {
        if (IsAlive && Health < MaxHealth)
        {
            int maxHeal = Mathf.Max(MaxHealth - Health, 0);
            int actualHeal  = Mathf.Min(maxHeal, healthRestore);

            Health += actualHeal;

            CharacterEvents.characterHealed(gameObject, actualHeal);
            return true;
        }
        return false;
    }
}

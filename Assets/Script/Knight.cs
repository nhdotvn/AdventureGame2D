using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirection), typeof(Damageable))]
public class Knight : MonoBehaviour
{
    public float walkAcceleration = 3f;
    public float maxSpeed = 3f;

    public float walkStopRate = 0.6f;
    public DetectionZone attackZone;
    public DetectionZone cliffDetectionZone;



    Rigidbody2D rb;
    TouchingDirection touchingDirection;
    Animator animator;
    Damageable damageable;


    public enum WalkAbleDirection { Right, Left}

    private WalkAbleDirection _walkDirection;
   
    public WalkAbleDirection WalkDirection
    {
        get { return _walkDirection; }
        set
        {
            if(_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                if(value == WalkAbleDirection.Right)
                {
                    WalkDirectionVector = Vector2.right;

                }
                else if(value == WalkAbleDirection.Left)
                {
                    WalkDirectionVector = Vector2.left;
                }
            }
            _walkDirection = value;
        }
    }

    public bool _hasTarget = false;

    public bool HasTarget { get { return _hasTarget; } 
        private set 
        { 
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);    
        }
    }
    public bool canMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public float AttackCooldown
    {
        get
        {
            return animator.GetFloat(AnimationStrings.attackCooldown);
        }
        private set {
            animator.SetFloat(AnimationStrings.attackCooldown,MathF.Max(value,0));
        }
    }

    private Vector2 WalkDirectionVector = Vector2.right;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirection = GetComponent<TouchingDirection>();
        animator = GetComponent<Animator>();    
        damageable = GetComponent<Damageable>();
    }

    private void FixedUpdate()
    {
        if (touchingDirection.IsGrounded && touchingDirection.IsOnWall)
        {
            FlipDirection();
        }
        if (!damageable.LockVeloctiy)
        {
            if (canMove && touchingDirection.IsGrounded)
            {
                

                rb.velocity = new Vector2(
                    Mathf.Clamp(
                        rb.velocity.x + (walkAcceleration * WalkDirectionVector.x * Time.fixedDeltaTime), 
                        -maxSpeed, maxSpeed), rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
            }
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;

        if(AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }
        
    }
    private void FlipDirection()
    {
        if (WalkDirection == WalkAbleDirection.Right)
        {
            WalkDirection = WalkAbleDirection.Left;
        }
        else if (WalkDirection == WalkAbleDirection.Left)
        {
            WalkDirection = WalkAbleDirection.Right;
        }
        else {
            Debug.LogError("Current walkable direction is not set to legal values of left or right");
        }
    }

    public void onHit(int damage, Vector2 knockBack)
    {    
        rb.velocity = new Vector2(knockBack.x, rb.velocity.y + knockBack.y);
    }

    public void onCliffDetected()
    {
        if (touchingDirection.IsGrounded)
        {
            FlipDirection();
        }
    }
    
}

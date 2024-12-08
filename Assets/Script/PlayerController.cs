using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirection), typeof(Damageable))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;

    public float runSpeed = 8f;
    
    public float jumpImpulse = 10f;
    
    public float airWalkSpeed = 3f;
    
    TouchingDirection touchingDirections;

    Damageable damageale;
    
    
    Vector2 moveInput;

    Rigidbody2D rb;
    Animator animator;

    public float currentMoveSpeed
    {
        get
        {
            if (CanMove)
            {
                if (IsMoving && !touchingDirections.IsOnWall)
                {
                    if (touchingDirections.IsGrounded)
                    {
                        if (IsRunning)
                        {
                            return runSpeed;
                        }
                        else return walkSpeed;
                    }
                    else
                    {
                        return airWalkSpeed;
                    }

                }
                else
                {
                    return 0;
                }
            }
            else {
                return 0;
            }
            
            
        }

    }
    [SerializeField]
    private bool _isMoving = false;

    
    public bool IsMoving { get 
        { 
            return _isMoving;
        }
        private set 
        { 
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        } 
    }
    [SerializeField]
    private bool _isRunning = false;

    public bool IsRunning
    {
        get
        {
            return _isRunning;
        }
        set
        {
            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }
    }
    
    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public bool IsAlive
    {
        get
        {
            return animator.GetBool(AnimationStrings.isAlive);
        }
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirection>();
        damageale = GetComponent<Damageable>();
    }

    public bool _isFacingRight = true;
    

    public bool IsFacingRight { get { return _isFacingRight; } 
        private set { 
        if(_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
        _isFacingRight = value;
        } 
    
    }

    private void FixedUpdate()
    {
        if (!damageale.LockVeloctiy)
        {
            rb.velocity = new Vector2(moveInput.x * currentMoveSpeed, rb.velocity.y);
        }
        

        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
    }
    public void onMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (IsAlive)
        {
            IsMoving = moveInput != Vector2.zero;

            SetFacingDirection(moveInput);
        }
        else
        {
            IsMoving = false;
        }
       
    }
    private void SetFacingDirection(Vector2 moveInput)
    {
        if(moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if(moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }
    public void onRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;
        }
        else if (context.canceled)
        {
            IsRunning = false;
        }
    }
    public void onJump(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.IsGrounded && CanMove)
        {
            animator.SetTrigger(AnimationStrings.jumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }
    public void onAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.attackTrigger);
        }
    }

    public void onRangedAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.rangedAttackTrigger);
        }
    }

    public void onHit(int damage, Vector2 knockBack)
    {
        rb.velocity= new Vector2(knockBack.x, rb.velocity.y + knockBack.y);
    }
}


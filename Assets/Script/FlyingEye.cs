using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEye : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    Damageable damageable;  


    public float flightSpeed = 2f;
    public DetectionZone biteDetectionZone;
    public List<Transform> waypoints;
    public Collider2D deathCollider;
    private float wayPointReachedDistance = 0.1f;
    

    Transform nextWayPoint;
    int waypointnum = 0;

    
    public bool _hasTarget = false;
    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

    public bool canMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    private void Start()
    {
        nextWayPoint = waypoints[waypointnum];
    }
   

    // Update is called once per frame
    void Update()
    {
        HasTarget = biteDetectionZone.detectedColliders.Count > 0;
    }

    private void FixedUpdate()
    {
        if (damageable.IsAlive)
        {
            if (canMove)
            {
                FLight();
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }
        
    }

    private void FLight()
    {
        Vector2 directionToWayPoint = (nextWayPoint.position - transform.position).normalized;

        float distance = Vector2.Distance(nextWayPoint.position, transform.position);

        rb.velocity = directionToWayPoint * flightSpeed;
        updateDirection();

        if(distance < wayPointReachedDistance)
        {
            waypointnum++;
            if(waypointnum >= waypoints.Count)
            {
                waypointnum = 0;
            }

            nextWayPoint = waypoints[waypointnum];
        }


    }

    private void updateDirection()
    {

        Vector3 locScale = transform.localScale;
        if(transform.localScale.x > 0)
        {
            if(rb.velocity.x < 0)
            {
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);    
            }
        }
        else
        {
            if (rb.velocity.x > 0)
            {
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
            }
        }
    }
    public void onDeath()
    {
        rb.gravityScale = 2f;
        rb.velocity = new Vector2(0, rb.velocity.y);
        deathCollider.enabled = true;
    }
}

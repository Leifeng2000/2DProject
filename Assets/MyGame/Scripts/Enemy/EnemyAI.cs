using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.EventTrigger;
[AddComponentMenu("LeiFeng/EnemyAI")]

public class EnemyAI : MonoBehaviour
{
    #region public properties
    public Transform pointA;
    public Transform pointB;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 3.5f;
    public float chaseRange = 7f;
    #endregion

    #region private properties
    private Transform target;
    private bool isChasing = false;
    private Transform player;
    private Animator animator;
    private Rigidbody2D rb;
    private bool facingRight; //facing direction
    #endregion

    void Start()
    {
        target = pointA;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (player != null && Vector2.Distance(transform.position, player.position) <= chaseRange)
        {
            if (!isChasing) Debug.Log("Started Chasing");
            isChasing = true;
        }
        else
        {
            if (isChasing) Debug.Log("Stopped Chasing");
            isChasing = false;
        }

        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        MoveTowards(target.position, patrolSpeed);

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            target = target == pointA ? pointB : pointA;
        }
    }
    void ChasePlayer()
    {
        MoveTowards(player.position, chaseSpeed);
    }
    void MoveTowards(Vector2 targetPosition, float speed)
    {
        Vector2 newPosition = Vector2.MoveTowards(rb.position, targetPosition, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);
        if ((targetPosition.x > rb.position.x && !facingRight) || (targetPosition.x < rb.position.x && facingRight))
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        Debug.Log("Flipped: Now facing " + (facingRight ? "Right" : "Left"));
    }
}

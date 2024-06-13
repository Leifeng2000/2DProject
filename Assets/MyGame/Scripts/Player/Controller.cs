using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("LeiFeng/Controller")]

public class Controller : MonoBehaviour
{
    #region Public
    public LayerMask groundLayer;
    #endregion
    #region Private
    [SerializeField] Transform groundCheck; // Assign this in the Inspector
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 5f;
    private Rigidbody2D rb;
    private SpriteRenderer spriteCharacter;
    private bool isGround = false;
    private bool facingRight = true;
    private int isWalkAnimationId = Animator.StringToHash("isWalk");
    private int isJumpAnimationId = Animator.StringToHash("isJump");
    private Animator anim;
    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteCharacter = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Move();
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            NotJump();
        }
    }

    private void NotJump()
    {
        anim.SetBool(isJumpAnimationId, false);
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        anim.SetBool(isJumpAnimationId, true);
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
        if ((horizontal > 0 && !facingRight) || (horizontal < 0 && facingRight))
        {
            Flip();
        }
        if (Mathf.Abs(horizontal) > 0)
        {
            anim.SetBool(isWalkAnimationId, true);
        }
        else
        {
            anim.SetBool(isWalkAnimationId, false);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
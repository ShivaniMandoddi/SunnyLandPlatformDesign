using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnotherMovement : MonoBehaviour
{
    public float playerSpeed;
    public float jumpSpeed;
    Rigidbody2D rb;
    Vector2 movement;
    SpriteRenderer render;
    Animator animator;
    bool IsGrounded = true;
    ScoreCalculator calculator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

       animator.SetFloat("IsRunning", movement.sqrMagnitude);
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded == true)
        {
            Jump();
        }
        if (movement.x < 0)
            render.flipX = true;
        else if (movement.x > 0)
            render.flipX = false;

    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(movement.x*playerSpeed, rb.velocity.y);
  
    }

    private void Jump()
    {
        rb.AddForce(new Vector2(rb.velocity.x, jumpSpeed), ForceMode2D.Impulse);
        animator.SetBool("IsJump", true);
        IsGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            animator.SetBool("IsJump", false);
            IsGrounded = true;
        }
        if(collision.gameObject.tag=="Coins")
        {
            Destroy(collision.gameObject);
            calculator = GameObject.Find("ScoreManager").GetComponent<ScoreCalculator>();
            calculator.Score(5);
        }
    }
}

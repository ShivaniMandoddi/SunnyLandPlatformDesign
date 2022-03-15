using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float playerSpeed=2f;
    public float playerjumpspeed;
    Rigidbody2D rb;
    Vector2 movement;
    Animator animator;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        animator.SetFloat("IsRunning", movement.sqrMagnitude);
        
           
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position+movement * playerSpeed * Time.fixedDeltaTime);
        if (Input.GetKeyDown(KeyCode.Space))
        {

            rb.AddForce(new Vector2(0f,movement.y * playerjumpspeed),ForceMode2D.Impulse );
            animator.SetBool("IsJump", true);
        }
       else 
           animator.SetBool("IsJump", false);
        
    }
}

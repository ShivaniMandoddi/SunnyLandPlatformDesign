using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerAnotherMovement : MonoBehaviour
{
    public float playerSpeed;   //PlayerSpeed
    public float jumpSpeed;
    Rigidbody2D rb;
    Vector2 movement;
    SpriteRenderer render;
    Animator animator;
    public GameObject winpage; // Make Active the Game Win Page
    public Button quit;          
    public Button restart;
    public Text condition;
    bool IsGrounded = true;
    bool IsJump=false;
    ScoreCalculator calculator;
    bool IsGameOver = false;
    public Text Score;
    GameObject collide;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        quit.onClick.AddListener(Quit);
        restart.onClick.AddListener(Restart);
        calculator = GameObject.Find("ScoreManager").GetComponent<ScoreCalculator>();
    }

    
    // Update is called once per frame
    void Update()
    {
        if (IsGameOver == false)
        {
            movement.x = Input.GetAxis("Horizontal");
            movement.y = Input.GetAxis("Vertical");

            animator.SetFloat("IsRunning", movement.sqrMagnitude);
            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded == true)
            {
                Jump();
            }
            if (movement.x < 0)                     //FLipping the player based on movement
                render.flipX = true;
            else if (movement.x > 0)
                render.flipX = false;
        }
        if(calculator.score==100)
        {
            winpage.SetActive(true);
            condition.text = "Won the Game!!";
            IsGameOver = true;
        }

    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(movement.x*playerSpeed, rb.velocity.y);   //Adding horizontal movement to player
  
    }

    private void Jump()
    {
        rb.AddForce(new Vector2(rb.velocity.x, jumpSpeed), ForceMode2D.Impulse);    //Adding some force to jump 
        animator.SetBool("IsJump", true);
        IsJump = true;
        IsGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")                          
        {
            animator.SetBool("IsJump", false);
            IsGrounded = true;
            IsJump = false;
        }
        if(collision.gameObject.tag=="Coins")                        //When player Collects Gems and cherries updating the score  
        {
            Destroy(collision.gameObject);
            calculator = GameObject.Find("ScoreManager").GetComponent<ScoreCalculator>();
            calculator.Score(5);
            Score.text = "Score: " + calculator.score;
        }
        if(collision.gameObject.tag=="Enemy" && IsJump==true)       // When player collide  enemy with jump, updating the score and killing the enemy
        {

            collide = collision.gameObject;
            Animator expo=collision.gameObject.GetComponent<Animator>();
            expo.SetBool("IsDead", true);
            calculator.Score(10);
            Score.text = "Score: " + calculator.score;
            IsJump = false;
            StartCoroutine("Dead");

        }
        else if (collision.gameObject.tag == "Enemy") // When player collide enemy without jump, then game ends
        {
            IsGameOver = true;
            animator.SetBool("IsHurt",true);
            winpage.SetActive(true);
            condition.text = "Lost the Game!!";
           
        }

    }
    IEnumerator Dead()           // Destroying enemy after 1s
    {
        yield return (new WaitForSeconds(1)); 
        Destroy(collide);
    }
    private void Restart()// Restart Same PAge 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Quit()// Going to Home Page
    {
        SceneManager.LoadScene(0);
    }

}

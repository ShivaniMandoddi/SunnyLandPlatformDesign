using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpossumMovement : MonoBehaviour
{
    

    public float speed;
    Rigidbody2D rb;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(rb.velocity.x * -speed, rb.velocity.y);
    }
    
        
    
}

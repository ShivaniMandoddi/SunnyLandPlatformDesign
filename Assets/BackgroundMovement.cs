using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    // Start is called before the first frame update
    float speed = 2f;
   

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed*Time.deltaTime);
        if (transform.position.x < -25.5)
            transform.position = new Vector2(30f, transform.position.y);
    }
}

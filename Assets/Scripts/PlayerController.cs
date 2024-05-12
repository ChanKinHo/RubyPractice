using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 3f;

    float inputX;
    float inputY;

    Rigidbody2D rigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        
    }

    private void FixedUpdate()
    {
        Vector2 position = rigidbody2D.position;

        position.x = position.x + inputX * speed * Time.deltaTime;
        position.y = position.y + inputY * speed * Time.deltaTime;

        rigidbody2D.position = position;
    }
}

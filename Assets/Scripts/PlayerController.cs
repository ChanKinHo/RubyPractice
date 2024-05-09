using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 3f;

    float inputX;
    float inputY;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        Vector2 position = transform.position;

        position.x = position.x + inputX * speed * Time.deltaTime;
        position.y = position.y + inputY * speed * Time.deltaTime;

        transform.position = position;
    }
}

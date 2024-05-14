using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public int maxHealth = 5;
    int currentHealth;
    public int Health {  get { return currentHealth; } }

    public float timeInvincible = 2.0f;

    bool isInvincible;
    float invincibleTimer;

    public float speed = 3f;

    float inputX;
    float inputY;

    Rigidbody2D rigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");

        if (isInvincible) 
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
            {
                isInvincible = false;
            }
        }
        
    }

    private void FixedUpdate()
    {
        Vector2 position = rigidbody2D.position;

        position.x = position.x + inputX * speed * Time.deltaTime;
        position.y = position.y + inputY * speed * Time.deltaTime;

        //rigidbody2D.position = position;
        rigidbody2D.MovePosition(position);
    }

    public void ChangeHealth(int amount) 
    {
        if (amount < 0)
        {
            if (isInvincible)
            {
                return;
            }
            invincibleTimer = timeInvincible;
            isInvincible = true;
        }
        currentHealth = Mathf.Clamp(currentHealth+amount,0,maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }
}

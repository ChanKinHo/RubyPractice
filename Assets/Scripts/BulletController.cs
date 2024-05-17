using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody2D rb;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void launch(Vector2 direction, float force)
    {
        rb.AddForce(direction*force);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.Fixed();
        }
        
        Destroy(gameObject);
    }

    

}

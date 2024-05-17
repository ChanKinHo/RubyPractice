using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public AudioClip healthClip;
    public ParticleSystem eatEffect;
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null) 
        {
            if (player.Health < player.maxHealth) 
            {
                player.ChangeHealth(1);
                Instantiate(eatEffect,transform.position,Quaternion.identity);

                player.PlaySound(healthClip);

                Destroy(gameObject);
            }
            
        }
        //Debug.Log("some object touch: " + other);
    }
}

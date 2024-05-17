using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public int maxHealth = 5;

    public GameObject bulletPrefab;
    int currentHealth;
    public int Health {  get { return currentHealth; } }

    public float timeInvincible = 2.0f;

    bool isInvincible;
    float invincibleTimer;

    public float speed = 3f;

    float inputX;
    float inputY;

    float flashTime = 0f;

    Rigidbody2D rigidbody2D;

    Animator animator;

    Vector2 lookDirection = new Vector2(1, 0);

    private Renderer myRender;

    AudioSource myAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        myRender = GetComponent<Renderer>();
        myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");

        Vector2 move = new Vector2(inputX, inputY);

        if (!Mathf.Approximately(move.x,0.0f) || !Mathf.Approximately(move.y,0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("lookX",lookDirection.x);
        animator.SetFloat("lookY",lookDirection.y);
        animator.SetFloat("speed", move.magnitude);

        //Debug.Log("magnitude: " + move.magnitude);

        if (Input.GetKeyDown(KeyCode.J))
        {
            Launch();
        }

        if (Input.GetKeyDown(KeyCode.Z)) 
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2D.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NonPlayerController nonPlayer = hit.collider.GetComponent<NonPlayerController>();
                if (nonPlayer != null)
                {
                    nonPlayer.DisplayDialog();
                }
                
                //Debug.Log("Raycast has hit the object " + hit.collider.gameObject);
            }
        }

        if (isInvincible) 
        {
            invincibleTimer -= Time.deltaTime;

            if (flashTime > 0)
            {
                flashTime -= Time.deltaTime;
            }
            else 
            {
                myRender.enabled = !myRender.enabled;
                flashTime = 0.3f;
            }
            
            
            if (invincibleTimer < 0)
            {
                isInvincible = false;
                myRender.enabled = true;
                flashTime = 0f;
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
            animator.SetTrigger("hit");
        }
        currentHealth = Mathf.Clamp(currentHealth+amount,0,maxHealth);
        
        //Debug.Log(currentHealth + "/" + maxHealth);
        HealthBarController.instance.SetValue(currentHealth/(float)maxHealth);
    }

    void Launch()
    {
        GameObject bulletObject = Instantiate(bulletPrefab, rigidbody2D.position + Vector2.up * 0.5f, Quaternion.identity);
        BulletController bulletController = bulletObject.GetComponent<BulletController>();
        bulletController.launch(lookDirection, 300);

        animator.SetTrigger("launch");

    }

    public void PlaySound(AudioClip clip)
    {
        myAudioSource.PlayOneShot(clip);
    }
}

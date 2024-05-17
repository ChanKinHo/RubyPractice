using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float speed = 3f;

    Rigidbody2D rigidbody2D = null;

    int direction;

    public bool isVertical;

    public float changeTime = 3.0f;

    Animator animator;

    public ParticleSystem smokeEffect;

    bool isBroken = true;

    //计时器
    float timer;

    //float detalDistance;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        direction = -1;
        //detalDistance = 0f;
        timer = changeTime;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!isBroken)
        {
            return;
        }
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }

    private void FixedUpdate()
    {

        if (!isBroken)
        {
            return;
        }

        Vector2 position = rigidbody2D.position;

        if (isVertical)
        {
            position.y += direction * speed * Time.deltaTime;

            rigidbody2D.MovePosition(position);
            animator.SetFloat("moveX", 0);
            animator.SetFloat("moveY", direction);
        }else
        {
            position.x += direction * speed * Time.deltaTime;
            rigidbody2D.MovePosition(position);
            animator.SetFloat("moveX", direction);
            animator.SetFloat("moveY", 0);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController controller = other.gameObject.GetComponent<PlayerController>();
        if (controller != null)
        {
            controller.ChangeHealth(-1);
        }
    }

    public void Fixed()
    {
        isBroken = false;

        //取消物理效果，即不会碰撞
        rigidbody2D.simulated = false;

        animator.SetTrigger("fixed");

        //不用destroy而用stop是因为当粒子系统被销毁时，也会销毁当前正在处理的所有粒子，即使是刚刚创建的粒子
        //Stop 只会阻止粒子系统创建粒子，已经存在的粒子可以正常结束自己的生命周期。这比所有粒子突然消失要看起来自然得多
        smokeEffect.Stop();
    }


    //使用距离判断来实现来回
    //private void FixedUpdate()
    //{
    //    Vector2 position = rigidbody2D.position;

    //    if (detalDistance > 8f)
    //    {
    //        detalDistance = 0f;
    //        verticalDirection = verticalDirection * -1;
    //    }

    //    detalDistance += Mathf.Abs(verticalDirection * speed * Time.deltaTime);
    //    position.y += verticalDirection * speed * Time.deltaTime;

    //    rigidbody2D.MovePosition(position);
    //}
}

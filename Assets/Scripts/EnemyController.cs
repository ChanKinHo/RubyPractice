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

    //��ʱ��
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

        //ȡ������Ч������������ײ
        rigidbody2D.simulated = false;

        animator.SetTrigger("fixed");

        //����destroy����stop����Ϊ������ϵͳ������ʱ��Ҳ�����ٵ�ǰ���ڴ�����������ӣ���ʹ�Ǹոմ���������
        //Stop ֻ����ֹ����ϵͳ�������ӣ��Ѿ����ڵ����ӿ������������Լ����������ڡ������������ͻȻ��ʧҪ��������Ȼ�ö�
        smokeEffect.Stop();
    }


    //ʹ�þ����ж���ʵ������
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

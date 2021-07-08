using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlyMonster : Mob_Monster
{


    [SerializeField] private Vector2 startPos;
    [SerializeField] private float boundary;
    [SerializeField] private float posY;

    bool temp;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        Initiallized();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        monsterData = GetComponent<MonsterData>();

        spriteScale = sr.transform.localScale.x;
        Debug.Log(spriteScale);
        death = false;
        isTracing = true;
        isAttacking = true;
        temp = false;
        Invoke("Think", 1f);

    }
    // Update is called once per frame
    void Update()
    {

        if (death)
            return;

        if (HP < 0)
        {
            Debug.Log("die");
            //anim.PlayAnimation(2);
            rb.velocity = Vector2.zero;

            CancelInvoke();
            this.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            this.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            //InstantinateBlood();
            death = true;
            sr.DOFade(0, 1f);
        }

    }

    private void FixedUpdate()
    {
        if (death)
        {
            CancelInvoke();
            return;
        }

        Around();

    }


    protected override void Initiallized()
    {
        HP = monsterData.HP;
        speed = monsterData.Speed;
        attRange = monsterData.AttRange;
        att = monsterData.Att;
        attSpeed = monsterData.AttSpeed;
        blood = (int)monsterData.BloodCnt;
    }
    protected override void Around()
    {

        Move();

        if (distance < attRange)
        {

            if (isAttacking)
            {
               // Attack();
            }
            else
            {
                return;
            }
        }
        else
        {
            animator.SetBool("Walk", false);
        }

    }

    protected override void Move()
    {

        if (death)
            return;

        if (!isAttacking)
            return;


        float round = Vector2.Distance((Vector2)transform.position , (Vector2)startPos);

        if (boundary < round && !temp)
        {
            transform.DOMove(startPos,2f);
        }
        else
        {
            rb.velocity = new Vector2(nextDiretion, posY);
            temp = false;
        }

    }

    protected override void Think()
    {
        temp = true;

        nextDiretion = Random.Range(-1, 2); // -1 왼쪽 , 0 정지 , 1 오른쪽
        posY = Random.Range(-1, 1);
        float time = Random.Range(2f, 5f);
         
        Debug.Log(nextDiretion);
        if (nextDiretion == 0)
        {
            animator.SetBool("Walk", false);
            Invoke("Think", time);
            return;

        }

        if (nextDiretion == -1)
        {
            transform.localScale = new Vector3(spriteScale, spriteScale, 1);

        }
        else if (nextDiretion == 1)
        {
            transform.localScale = new Vector3(-spriteScale, spriteScale, 1);

        }

        animator.SetBool("Walk", true);
        Invoke("Think", time);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
 
        if (collision.CompareTag("Player"))
        {
            Debug.Log("into Player");
            traceTarget = collision.gameObject;
            CancelInvoke();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            Debug.Log("trace Player");
            isTracing = true;
            animator.SetBool("Walk", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
      
        if (collision.CompareTag("Player"))
        {
            isTracing = false;
            Stop();
            traceTarget = null;
            Invoke("Think", 2f);
        }
    }
}

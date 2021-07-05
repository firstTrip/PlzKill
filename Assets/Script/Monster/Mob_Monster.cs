using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_Monster : MonoBehaviour
{
    [SerializeField] protected float HP;
    [SerializeField] protected int blood;
    [SerializeField] protected float speed;
    [SerializeField] public float att;
    [SerializeField] protected float attSpeed;

    [SerializeField] protected float attRange;
    [SerializeField] protected GameObject bloodPiece;
    [SerializeField] protected Animator animator;


    protected float distance;
    protected float spriteScale;
    protected Rigidbody2D rb;
    [SerializeField] protected int nextDiretion;

    protected bool death;
    protected bool isTracing;
    protected bool isAttacking;

    [SerializeField] protected GameObject traceTarget;
    [SerializeField] protected GameObject textObj;
    [SerializeField] protected Transform textPos;

    protected SpriteRenderer sr;
    protected enum MonsterType
    {
        AggressiveMonster, // ¼±°ø¸÷
        NonAggressiveMonster, // ÈÄ°ø ¸÷
        NavMonster, // Æ®·¹ÀÌ½Ì ¸÷
        FixedMonster, // °íÁ¤Çü ¸÷
        AroundMonster , // ÀÌµ¿Çü ¸÷

    }

    [SerializeField] protected MonsterType monsterType;
    // Start is called before the first frame update
    void Start()
    {
        HP = 1000;
        blood = 1;
        attRange = 1f;
        att = 10f;
        attSpeed = 2f;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        spriteScale = sr.transform.localScale.x;
        Debug.Log(spriteScale);
        death = false;
        isTracing = false;
        isAttacking = true;

        if(monsterType != MonsterType.FixedMonster)
        {
            Invoke("Think", 1f);

        }
    }

    // Update is called once per frame
    void  Update()
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
            //this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            this.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            InstantinateBlood();
            death = true;
        }
    }

    private void FixedUpdate()
    {
        if (death)
        {
            CancelInvoke();
            return;
        }

        if(monsterType == MonsterType.NavMonster)
        {
            Trace();

        }else if(monsterType == MonsterType.AroundMonster)
        {
            Around();
        }else if (monsterType == MonsterType.FixedMonster)
        {
            FixedMob();
        }


    }

    protected void InstantinateBlood()
    {
        for (int n = blood; n > 0; n--)
        {
            Instantiate(bloodPiece, this.transform.position, Quaternion.identity);

        }

    }

    // Æ®·¹ÀÌ½Ì ¸÷
    protected void Trace()
    {

        if (isTracing)
        {
            Vector2 playerPos = traceTarget.transform.position;
            distance = Vector2.Distance(playerPos, transform.position);

            //Debug
            if (playerPos.x > transform.position.x)
            {
                transform.localScale = new Vector3(-spriteScale, spriteScale, 1);
                nextDiretion = 1;
            }
            else if (playerPos.x < transform.position.x)
            {
                transform.localScale = new Vector3(spriteScale, spriteScale, 1);
                nextDiretion = -1;
            }

            if (distance < attRange)
            {
                Debug.Log("attack");

                if (isAttacking)
                {
                    Attack();
                }
                else
                {
                    return;
                }
            }
        }

        Move();
    }


    // ÀÌµ¿Çü ¸÷
    protected virtual void Around()
    {

        if (isTracing)
        {
             Move();

            if (distance < attRange)
            {
                Debug.Log("attack");

                if (isAttacking)
                {
                    Attack();
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
   
    }
    protected virtual void FixedMob()
    {
        if (isTracing)
        {
            Vector2 playerPos = traceTarget.transform.position;
            distance = Vector2.Distance(playerPos, transform.position);

            //Debug
            if (playerPos.x > transform.position.x)
            {
                transform.localScale = new Vector3(-spriteScale, spriteScale, 1);
            }
            else if (playerPos.x < transform.position.x)
            {
                transform.localScale = new Vector3(spriteScale, spriteScale, 1);
            }

            if (distance < attRange)
            {
                Debug.Log("attack");

                if (isAttacking)
                {
                    Attack();
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
        
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, attRange);
    }

    protected virtual void Attack()
    {
        //°ø°Ý ÇÏ±â ÅõÃ´ ±ÙÁ¢ÀÌµç ¹¹µç
        isAttacking = false;
        animator.SetTrigger("Attack");
        traceTarget.gameObject.GetComponent<Player>().GetDamage(att, transform);
        Invoke("CancleAttack", attSpeed);
    }

    protected void CancleAttack()
    {
        isAttacking = true;
    }


    protected virtual void Move()
    {

        if (death)
            return;

        if (!isAttacking)
            return;

        rb.velocity = new Vector2(nextDiretion, rb.velocity.y);

        Vector2 frontvec = new Vector2(rb.position.x + nextDiretion * 0.4f, rb.position.y);

        Debug.DrawRay(frontvec, Vector3.down, new Color(0, 1, 0));

        RaycastHit2D raycast = Physics2D.Raycast(frontvec, Vector3.down, 1, LayerMask.GetMask("Ground"));


        if (raycast.collider == null)
            Turn();
    }

    protected void Turn()
    {
        nextDiretion = nextDiretion * (-1);

        CancelInvoke();
        Invoke("Think", 2f);
    }

    protected virtual void Think()
    {
        nextDiretion = Random.Range(-1, 2); // -1 ¿ÞÂÊ , 0 Á¤Áö , 1 ¿À¸¥ÂÊ
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

    protected virtual void Skill()
    {
        Debug.Log("nope");
    }


    public  void GetDamage(float Damage)
    {

        GameObject DamageText = Instantiate(textObj);
        DamageText.GetComponent<DamageText>().damage = Damage;

        DamageText.transform.position = textPos.position;
        HP -= Damage;
        Debug.Log(HP);
    }


    protected void Stop()
    {
        nextDiretion = 0;
        rb.velocity = Vector2.zero;
        animator.SetBool("Walk", false);
        CancelInvoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (monsterType != MonsterType.NavMonster)
            return;

        if (collision.CompareTag("Player"))
        {
            Debug.Log("into Player");
            traceTarget = collision.gameObject;
            CancelInvoke();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (monsterType != MonsterType.NavMonster)
            return;

        if (collision.CompareTag("Player"))
        {
            Debug.Log("trace Player");
            isTracing = true;
            animator.SetBool("Walk", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (monsterType != MonsterType.NavMonster)
            return;

        if (collision.CompareTag("Player"))
        {
            isTracing = false;
            Stop();
            Invoke("Think", 2f);
        }
    }
}

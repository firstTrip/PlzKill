using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Tentacle : Mob_Monster
{

    private Rigidbody2D playerRb;
    [SerializeField] private float pushForce;
    // Start is called before the first frame update
    void Start()
    {
        HP = 50;
        blood = 1;
        attRange = 2f;
        att = 10f;
        attSpeed = 2f;
        pushForce = 3;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        spriteScale = sr.transform.localScale.x;
        Debug.Log(spriteScale);
        death = false;
        isTracing = false;
        isAttacking = true;

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
            this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            //this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
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
        FixedMob();
    }

    protected override void FixedMob()
    {
        base.FixedMob();
    }
 
    protected override void Attack()
    {
        //공격 하기 투척 근접이든 뭐든
        isAttacking = false;
        animator.SetTrigger("Attack");
        traceTarget.gameObject.GetComponent<Player>().GetDamage(att, transform);
        Skill();
        Invoke("CancleAttack", attSpeed);
    }
    protected override void Skill()
    {
        int dir = traceTarget.gameObject.transform.position.x - transform.position.x > 0 ? 1 : -1;
        playerRb.AddForce(new Vector2(dir, 1) * pushForce, ForceMode2D.Impulse);
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
            Invoke("Think", 2f);
        }
    }
}

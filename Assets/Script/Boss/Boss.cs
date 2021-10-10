using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Boss : MonoBehaviour
{
    public int patternIndex;
    public int curCountPatterns;
    public int[] maxPatternsCount;

    public float dashPower;
    [Space]
    [Header("HP")]
    public float HP;
    public float MaxHP;

    [Space]
    public GameObject Player;
    public GameObject nextStage;
    [Space]
    public Transform[] bulletPos;
    [Space]
    [SerializeField] protected GameObject smashBullet;

    public SpriteRenderer sr;
    public Animator Anim;
    Vector2 spriteSize;
    public Rigidbody2D rb;

    public float bAtt;

    [Space]

    [Header("offSet")]
    public float leftXoffset;
    public float rightXoffset;

    [Space]

    [Header(" ???? ????")]
    public float BulletSpeed;

    [Space]

    public Transform handPos;
    public int bulletCnt;
    public float DashCoolTime;

    public float radius;

    public bool onWall;
    public bool isActive = false;

    public int nextDiretion;
    public enum BossState 
    {
        Death,
        Idle,
        Walk,
        Dash,
        Stun,
        Berserk
    }

    public BossState bossState;
    // 질량에 따라 대쉬 속도 달라지는 거 고쳐야 하고 부디치면 밀리는거 사라져야함
    // Start is called before the first frame update
    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        Anim = GetComponent<Animator>();
        spriteSize = sr.transform.localScale;
        Debug.Log("boss Size :"+spriteSize);
        bossState = BossState.Idle;
        nextStage.SetActive(false);
        HP = 1000;
        MaxHP = HP;

        bAtt = 30f;
        patternIndex = 0;
       
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(this.transform.position + new Vector3(rightXoffset, 0, 0), radius);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(this.transform.position + new Vector3(leftXoffset, 0, 0), radius);
    }

    // Update is called once per frame
    void Update()
    {
        if (bossState == BossState.Death || GameManager.Instance.gameMode == GameManager.GameMode.nomal)
            return;

        onWall = Physics2D.OverlapCircle(this.transform.position + new Vector3(rightXoffset, 0, 0), radius, LayerMask.GetMask("Ground"))
                            || Physics2D.OverlapCircle(this.transform.position + new Vector3(leftXoffset, 0, 0), radius, LayerMask.GetMask("Ground"));

        if (bossState == BossState.Idle)
            Flip();


        if (HP < 0)
        {
            nextStage.SetActive(true);
        }
            Debug.Log("dead");
    }

    void Think()
    {
        bossState = BossState.Idle;

        patternIndex = patternIndex == 2 ? 0 : patternIndex +1;
        curCountPatterns = 0;

        switch (patternIndex)
        {
            case 0:
                DashToPlayer();
                break;

            case 1:
                Walk();
                break;

            case 2:
                SmashToPlayer();
                break;
        }
    }

    void Walk()
    {

        Debug.Log("Walk");
        bossState = BossState.Walk;

        Flip();

        rb.velocity = new Vector2(nextDiretion *5, rb.velocity.y);

        curCountPatterns++;

        if (curCountPatterns < maxPatternsCount[patternIndex])
            Invoke("Walk", DashCoolTime);
        else
            Invoke("Think", 2);
    }

    void Flip()
    {
        if (Player.transform.position.x > gameObject.transform.position.x)
        {
            nextDiretion = 1;
        }
        else
        {
            nextDiretion = -1;

        }

        if (nextDiretion == -1)
        {
            transform.localScale = new Vector3(-spriteSize.x, spriteSize.y, 1);

        }
        else if (nextDiretion == 1)
        {
            transform.localScale = new Vector3(spriteSize.x, spriteSize.y, 1);

        }
    }

    void DashToPlayer()
    {
        Debug.Log("DashToPlayer");
        DashCoolTime = 2f;
        bossState = BossState.Dash;
        Flip();


        StartCoroutine("WaitDash");

    }


    IEnumerator WaitDash()
    {
        Anim.SetTrigger("Test2");
        yield return new WaitForSeconds(2f);

        bool isRight = Player.transform.position.x - gameObject.transform.position.x > 0 ? true : false;

        if (isRight)
        {
            rb.AddForce(Vector2.right * dashPower, ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(Vector2.left * dashPower, ForceMode2D.Impulse);
        }

        if (onWall)
        {
            Stun();
        }

        curCountPatterns++;

        if (curCountPatterns < maxPatternsCount[patternIndex])
            Invoke("DashToPlayer", DashCoolTime);
        else
            Invoke("Think", 2);

    }
    void SmashToPlayer()
    {
        Debug.Log("SmashToPlayer");
        Anim.SetTrigger("Test");

        for (int i=0; i< bulletCnt; i++)
        {
            GameObject Bullet = Instantiate(smashBullet , bulletPos[i].position + new Vector3((i) * -0.6f , (i) * -0.3f, 0),Quaternion.identity);
            Rigidbody2D rbB=  Bullet.GetComponent<Rigidbody2D>();

            //Bullet.transform.rotation = Quaternion.Euler(0, 0, 90);

            Vector2 temp = Player.transform.position - bulletPos[i].position;

            float z = Mathf.Atan2(temp.x,temp.y) * Mathf.Rad2Deg;
            Bullet.transform.rotation = Quaternion.Euler(0, 0, z -100);

            rbB.AddForce(new Vector2(temp.x,temp.y) * BulletSpeed, ForceMode2D.Impulse);

            Destroy(Bullet, 1.5f);
        }
        curCountPatterns++;

        if (curCountPatterns < maxPatternsCount[patternIndex])
            Invoke("SmashToPlayer", 2);
        else
            Invoke("Think", 2);
    }

    public virtual void GetDamage(float Damage)
    {
        HP -= Damage;

        Debug.Log(HP);
    }

    void Stun()
    {
        bossState = BossState.Stun;

        DashCoolTime = 3f;
        Debug.Log(bossState);

    }

    public virtual float setHp()
    {
        return HP;
    }

    public virtual float setMaxHp()
    {
        return MaxHP;
    }

    // 보스 호출
    public virtual void StartThink()
    {
        Think();
    }

    void ChangeIdle()
    {
        bossState = BossState.Idle;

    }

    public virtual string GetBossState()
    {
        return bossState.ToString();
    }
}

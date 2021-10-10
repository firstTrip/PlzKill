using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centaur : Boss
{
    /*
    public int patternIndex;
    public int curCountPatterns;
    public int[] maxPatternsCount;

    public float dashPower;
    [Space]
    [Header("HP")]
    public float HP;

    [Space]
    public GameObject Player;
    [Space]
    public Transform[] bulletPos;
    

    public SpriteRenderer sr;
    public Animator Anim;

    public float bAtt;

    [Space]

    [Header("offSet")]
    public float leftXoffset;
    public float rightXoffset;

    [Space]

    [Header(" ???? ????")]
    public float BulletSpeed;

    [Space]

    public int bulletCnt;
    public float DashCoolTime;

    public float radius;

    public bool onWall;
    */
    [Space]
    //public GameObject Arrow;

    private Vector2 SpriteSize;
    //private Rigidbody2D rb;
    /*
    private int nextDiretion;
    private enum BossState
    {
        Death,
        Idle,
        Walk,
        Dash,
        Stun,
        Berserk
    }

    private BossState bossState;
    */

    public GameObject hand;
    public GameObject Arrow;

    // Start is called before the first frame update
    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        Anim = GetComponent<Animator>();
        SpriteSize = sr.transform.localScale;
        bossState = BossState.Idle;

        bAtt = 30f;
        patternIndex = 0;
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
    }



    void Think()
    {
        bossState = BossState.Idle;

        patternIndex = patternIndex == 2 ? 0 : patternIndex + 1;
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
                SmashToPlayer2();
                break;
        }
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
            transform.localScale = new Vector3(-SpriteSize.x, SpriteSize.y, 1);

        }
        else if (nextDiretion == 1)
        {
            transform.localScale = new Vector3(SpriteSize.x, SpriteSize.y, 1);

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
        Anim.SetTrigger("Dash");

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

        yield return new WaitForSeconds(DashCoolTime);

        curCountPatterns++;

        if (curCountPatterns < maxPatternsCount[patternIndex])
            Invoke("DashToPlayer", DashCoolTime);
        else
            Invoke("Think", 2);

    }
    void SmashToPlayer2()
    {
        Debug.Log("ShotArrow");
        StartCoroutine("ShotArrow");

    }

    IEnumerator ShotArrow()
    {

        while (bulletCnt < 5)
        {
            Anim.SetTrigger("Attack");

            yield return new WaitForSeconds(1f);

            bulletCnt++;

        }

        yield return null;

        bulletCnt = 0;
        curCountPatterns++;

        if (curCountPatterns < maxPatternsCount[patternIndex])
            Invoke("SmashToPlayer2", 2);
        else
            Invoke("Think", 2);
    }

    public void Shot()
    {
        Debug.Log("count shot");
        Flip();

        GameObject go = Instantiate(Arrow);
        go.transform.position = hand.transform.position;
        go.transform.rotation = Quaternion.identity;


        Rigidbody2D rbB = go.GetComponent<Rigidbody2D>();

        //Bullet.transform.rotation = Quaternion.Euler(0, 0, 90);

        Vector2 temp = Player.transform.position - hand.transform.position;

        float z = Mathf.Atan2(temp.x, temp.y) * Mathf.Rad2Deg;

        go.transform.rotation = Quaternion.Euler(0, 0, z - 100);

        rbB.AddForce(new Vector2(temp.x, temp.y) * BulletSpeed, ForceMode2D.Impulse);

        Destroy(go, 1.5f);
    }


    void Walk()
    {

        Debug.Log("Walk");
        bossState = BossState.Walk;

        Flip();

        rb.velocity = new Vector2(nextDiretion * 5, rb.velocity.y);

        curCountPatterns++;

        if (curCountPatterns < maxPatternsCount[patternIndex])
            Invoke("Walk", DashCoolTime);
        else
            Invoke("Think", 2);
    }

    
    public override void GetDamage(float Damage)
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

    void ChangeIdle()
    {
        bossState = BossState.Idle;

    }
    
    public override float setHp()
    {
        return HP;
    }

    public override  float setMaxHp()
    {
        return MaxHP;
    }

    public override void StartThink()
    {
        Think();
    }

    public override string GetBossState()
    {
        return bossState.ToString();
    }
    
}

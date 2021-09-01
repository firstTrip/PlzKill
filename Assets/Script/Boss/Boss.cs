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


    public GameObject Player;
    public GameObject smashBullet;

    public SpriteRenderer sr;
    float spriteSize;
    private Rigidbody2D rb;
    [Space]

    [Header("offSet")]
    public float leftXoffset;
    public float rightXoffset;

    [Space]

    [Header(" ź�� �ӵ�")]
    public int BulletSpeed;
    [Space]

    public Transform handPos;
    public int bulletCnt;
    public float DashCoolTime;

    public float radius;

    public bool onWall;

    private enum BossState 
    {
        Death,
        Idle,
        Dash,
        Stun,
        Berserk
    }

    private BossState bossState;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        spriteSize = sr.size.x;
        Debug.Log(spriteSize);
        bossState = BossState.Idle;
        patternIndex = 0;
        Think();
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
        if (bossState == BossState.Death)
            return;

        onWall = Physics2D.OverlapCircle(this.transform.position + new Vector3(rightXoffset, 0, 0), radius, LayerMask.GetMask("Ground"))
                            || Physics2D.OverlapCircle(this.transform.position + new Vector3(leftXoffset, 0, 0), radius, LayerMask.GetMask("Ground"));

    }

    void Think()
    {
        bossState = BossState.Idle;

        patternIndex = patternIndex == 2 ? 0 : patternIndex +1;
        curCountPatterns = 0;

        switch (patternIndex)
        {
            case 0:
                Walk();
                break;

            case 1:
                DashToPlayer();
                break;

            case 2:
                SmashToPlayer();
                break;
        }
    }

    void Walk()
    {

        Debug.Log("Walk");


        if (curCountPatterns < maxPatternsCount[patternIndex])
            Invoke("DashToPlayer", DashCoolTime);
        else
            Invoke("Think", 2);
    }
    void DashToPlayer()
    {
        Debug.Log("DashToPlayer");

        DashCoolTime = 2f;
        bossState = BossState.Dash;
        bool isRight = Player.transform.position.x - gameObject.transform.position.x > 0 ? true : false;

        Debug.Log(isRight);


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

        for(int i=0; i< bulletCnt; i++)
        {
            GameObject Bullet = Instantiate(smashBullet , handPos.position + new Vector3((i) * -0.6f , (i) * -0.3f, 0),Quaternion.identity);
            Rigidbody2D rb=  Bullet.GetComponent<Rigidbody2D>();

            //Bullet.transform.rotation = 

            Vector2 temp = Player.transform.position - transform.position;

            float z = Mathf.Atan2(temp.x,temp.y) * Mathf.Rad2Deg;
            Bullet.transform.rotation = Quaternion.Euler(0, 0, z);

            rb.AddForce(new Vector2(temp.x,temp.y) * BulletSpeed, ForceMode2D.Impulse);

            Destroy(Bullet, 1.5f);
        }
        curCountPatterns++;

        if (curCountPatterns < maxPatternsCount[patternIndex])
            Invoke("SmashToPlayer", 2);
        else
            Invoke("Think", 2);
    }

    void Stun()
    {
        // �ִ� ���
        // ���� x  
        // 3�� �ִٰ� �ٽ� �뽬 

        bossState = BossState.Stun;

        DashCoolTime = 3f;
        Debug.Log(bossState);

    }

    public string GetBossState()
    {
        return bossState.ToString();
    }
}

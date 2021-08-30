using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int patternIndex;
    public int curCountPatterns;
    public int[] maxPatternsCount;

    public float dashPower;


    public GameObject Player;
    private Rigidbody2D rb;
    [Space]

    [Header("offSet")]
    public float leftXoffset;
    public float rightXoffset;

    [Space]

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
        bossState = BossState.Idle;

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

        patternIndex = patternIndex == 1 ? 0 : patternIndex +1;
        curCountPatterns = 0;

        switch (patternIndex)
        {
            case 0:
                DashToPlayer();
                break;

            case 1:
                SmashToPlayer();
                break;
        }
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
            Stop();

        curCountPatterns++;

        if (curCountPatterns < maxPatternsCount[patternIndex])
            Invoke("DashToPlayer", DashCoolTime);
        else
            Invoke("Think", 2);

    }

    void SmashToPlayer()
    {
        Debug.Log("SmashToPlayer");

        curCountPatterns++;

        if (curCountPatterns < maxPatternsCount[patternIndex])
            Invoke("SmashToPlayer", 2);
        else
            Invoke("Think", 2);
    }

    void Stop()
    {
        // 애님 재생
        // 동작 x  
        // 3초 있다가 다시 대쉬 

        bossState = BossState.Stun;

        DashCoolTime = 3f;
        Debug.Log(bossState);

    }
}

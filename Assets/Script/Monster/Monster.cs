using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using BansheeGz.BGDatabase;

public class Monster : MonoBehaviour
{

    [SerializeField] private float HP;
    [SerializeField] private int blood;
    [SerializeField] private float speed;
    [SerializeField] public float att;
    [SerializeField] private float attSpeed;

    [SerializeField] private float attRange;
    [SerializeField] private GameObject bloodPiece;
    [SerializeField] private SPUM_Prefabs anim;


    private float distance;
    private float setSize;
    private Rigidbody2D rb;
    [SerializeField] private int nextDiretion;

    private bool death;
    private bool isTracing;
    private bool isAttacking;
    private bool isStun;

    [SerializeField] private GameObject traceTarget;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject textObj;
    [SerializeField] private Transform textPos;

    bool asd;

    public MonsterData monsterData;

    public enum MonsterType
    {
        AggressiveMonster, // 선공몹
        NonAggressiveMonster, // 후공 몹
        NavMonster // 트레이싱 몹

    }

    public MonsterType monsterType;
    // Start is called before the first frame update
    void Start()
    {
        Initiallized();
        asd = true;
        setSize = transform.localScale.x;
        Debug.Log(setSize);
        rb = GetComponent<Rigidbody2D>();
        monsterData = GetComponent<MonsterData>();
        Player = GameObject.FindGameObjectWithTag("Player");
        death = false;
        isTracing = false;
        isAttacking = true;
        isStun = false;
        Invoke("Think", 1f);
    }


    protected virtual void Initiallized()
    {
        HP = monsterData.HP;
        speed = monsterData.Speed;
        attRange = monsterData.AttRange;
        att = monsterData.Att;
        attSpeed = monsterData.AttSpeed;
        blood = (int)monsterData.BloodCnt;
    }
    // Update is called once per frame
    void Update()
    {
        if (death || isStun)
            return;

        if(HP < 0 )
        {
            CancelInvoke();
            nextDiretion = 2;
            rb.velocity = Vector2.zero;
            this.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            //this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            this.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            InstantinateBlood();
            death = true;
            Destroy(gameObject, 1f);
        }
    }

    private void FixedUpdate()
    {
        if (death && asd)
        {
            asd = false;
            nextDiretion = 2;
            Think();
            return;
        }

        Trace();
    }

    private void InstantinateBlood()
    {
        for(int n = blood; n>0; n--)
        {
            Instantiate(bloodPiece, this.transform.position, Quaternion.identity);
            Debug.Log(this.transform.position);
        }

    }

    private void Trace()
    {
        if (death)
            return;

        if (isTracing)
        {
            Vector2 playerPos = traceTarget.transform.position;
            distance = Vector2.Distance(playerPos, transform.position);

            if (playerPos.x > transform.position.x)
            {
                transform.localScale = new Vector3(-setSize, setSize, 1);
                nextDiretion = 1;
            }
            else if(playerPos.x < transform.position.x)
            {
                transform.localScale = new Vector3(setSize, setSize, 1);
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, attRange);
    }

    private void Attack()
    {
        //공격 하기 투척 근접이든 뭐든
        isAttacking = false;
        anim.PlayAnimation(4);
        traceTarget.gameObject.GetComponent<Player>().GetDamage(att,transform);
        Invoke("CancleAttack", attSpeed);
    }

   private void CancleAttack()
    {
        isAttacking = true;
    }

    private void Move()
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

    private void Turn()
    {
        nextDiretion = nextDiretion * (-1);

        CancelInvoke();
        Invoke("Think", 2f);
    }

    private void Think()
    {

        if (death)
        {
            anim.PlayAnimation(2);
            return;

        }

        nextDiretion = Random.Range(-1, 2); // -1 왼쪽 , 0 정지 , 1 오른쪽
        float time = Random.Range(2f, 5f);


        if (nextDiretion == 2)
            return;
        if (nextDiretion == 0)
        {
            anim.PlayAnimation(0);
            Invoke("Think", time);
            return;

        }

        if (nextDiretion == -1)
        {
            transform.localScale = new Vector3(setSize, setSize, 1);

        }
        else if(nextDiretion == 1)
        {
            transform.localScale = new Vector3(-setSize, setSize, 1);

        }

        anim.PlayAnimation(1);
        Invoke("Think", time);

    }

   
    public void GetDamage(float Damage)
    {
        Debug.Log(Damage);
        GameObject DamageText = Instantiate(textObj);
        DamageText.GetComponent<DamageText>().damage = Damage;

        DamageText.transform.position = textPos.position;
        SoundManager.Instance.PlaySound("Attack1");

        StartCoroutine("Stun");
        HP -= Damage;
        //Debug.Log(dir);
    }


    IEnumerator Stun()
    {
        CancelInvoke();

        anim.PlayAnimation(3);
        rb.velocity = Vector2.zero;
        isStun = true;

        yield return new WaitForSeconds(0.2f);
        isStun = false;

    }

    private void Stop()
    {
        nextDiretion = 0;
        rb.velocity = Vector2.zero;
        anim.PlayAnimation(0);
        CancelInvoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (monsterType != MonsterType.NavMonster ||isStun)
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
        if (monsterType != MonsterType.NavMonster || isStun)
            return;

        if (collision.CompareTag("Player"))
        {
            Debug.Log("trace Player");
            isTracing = true;
            anim.PlayAnimation(1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (monsterType != MonsterType.NavMonster || isStun)
            return;

        if (collision.CompareTag("Player"))
        {
            isTracing = false;
            Stop();
            Invoke("Think", 2f);
        }
    }

}

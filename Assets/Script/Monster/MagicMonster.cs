using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMonster : MonoBehaviour
{
    [SerializeField] private GameObject bullet;


    //private Rigidbody rgB;
    float BulletSpeed = 3f;


    [SerializeField] private float HP= 10;
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

    [SerializeField] private GameObject traceTarget;
    [SerializeField] private GameObject textObj;
    [SerializeField] private Transform textPos;

    bool asd;

    public MonsterData monsterData;

    public enum MonsterType
    {
        AggressiveMonster, // ¼±°ø¸÷
        NonAggressiveMonster, // ÈÄ°ø ¸÷
        NavMonster // Æ®·¹ÀÌ½Ì ¸÷

    }

    public MonsterType monsterType;
    // Start is called before the first frame update
    void Start()
    {
        //Initiallized();
        asd = true;
        setSize = transform.localScale.x;
        Debug.Log(setSize);
        rb = GetComponent<Rigidbody2D>();
        //monsterData = GetComponent<MonsterData>();
        death = false;
        isTracing = false;
        isAttacking = true;
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
        if (death)
            return;

        if (HP < 0)
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
        for (int n = blood; n > 0; n--)
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
            else if (playerPos.x < transform.position.x)
            {
                transform.localScale = new Vector3(setSize, setSize, 1);
                nextDiretion = -1;
            }

            if (distance < attRange)
            {
                Debug.Log("attack");

                if (isAttacking)
                {
                    Attack2();
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

    /*
    private void Attack()
    {
        //°ø°Ý ÇÏ±â ÅõÃ´ ±ÙÁ¢ÀÌµç ¹¹µç
        isAttacking = false;
        anim.PlayAnimation(4);
        traceTarget.gameObject.GetComponent<Player>().GetDamage(att, transform);
        Invoke("CancleAttack", attSpeed);
    }
    */

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

        nextDiretion = Random.Range(-1, 2); // -1 ¿ÞÂÊ , 0 Á¤Áö , 1 ¿À¸¥ÂÊ
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
        else if (nextDiretion == 1)
        {
            transform.localScale = new Vector3(-setSize, setSize, 1);

        }

        anim.PlayAnimation(1);
        Invoke("Think", time);

    }


    public void GetDamage(float Damage)
    {

        GameObject DamageText = Instantiate(textObj);
        DamageText.GetComponent<DamageText>().damage = Damage;

        DamageText.transform.position = textPos.position;
        HP -= Damage;
    }


    private void Stop()
    {
        nextDiretion = 0;
        rb.velocity = Vector2.zero;
        anim.PlayAnimation(0);
        CancelInvoke();
    }

    private void Attack()
    {

        isAttacking = false;
        anim.PlayAnimation(6);

        Vector2 temp = traceTarget.transform.position - transform.position + new Vector3(0, -1, 0);

        float z = Mathf.Atan2(temp.x, temp.y) * Mathf.Rad2Deg;

        GameObject Bullet = Instantiate(bullet, this.transform.position + new Vector3(0,1,0), Quaternion.identity);
        Rigidbody2D rbB = Bullet.GetComponent<Rigidbody2D>();

        rbB.AddForce(new Vector2(temp.x, temp.y) * BulletSpeed, ForceMode2D.Impulse);

        Debug.Log(new Vector2(temp.x, temp.y));
        //traceTarget.gameObject.GetComponent<Player>().GetDamage(att, transform);
        Destroy(Bullet, 1.5f);
        //Invoke("CancleAttack", attSpeed);
        StartCoroutine("cancleAttack");
    }

    private void Attack2()
    {
        int roundBullt = 12;
        isAttacking = false;
        anim.PlayAnimation(6);


        for (int index=0; index < roundBullt; index++)
        {
            GameObject Bullet = Instantiate(bullet, this.transform.position + new Vector3(0,1,0), Quaternion.identity);
         
            Rigidbody2D rbB2 = Bullet.GetComponent<Rigidbody2D>();

            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * index / roundBullt)
                                                ,Mathf.Sin(Mathf.PI * 2 * index / roundBullt));

            rbB2.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);

            Debug.Log(dirVec);
            Destroy(Bullet, 5f);

        }

        //traceTarget.gameObject.GetComponent<Player>().GetDamage(att, transform);
        //Invoke("CancleAttack", attSpeed);
        StartCoroutine("cancleAttack");
    }

    IEnumerator cancleAttack()
    {
        isAttacking = false;
        yield return new WaitForSeconds(attSpeed);
        isAttacking = true;
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
            //anim.PlayAnimation(1);
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

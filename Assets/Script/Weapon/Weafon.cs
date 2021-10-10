using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weafon : MonoBehaviour
{

    private GameObject player;
    private BoxCollider2D coll;

    public float att;

    public float PlayerAtt;

    private float startPos;
    private float endPos;

    private float attackTime;
    float z;
    float attackPos;
    Vector2 attackByVec;

    public WeaponId weaponId;

    int cnt;

    [Space]
    [Header("아이탬 이펙트")]
    public GameObject WeafonEffect;
    public GameObject skill;
    //private Animator anim;

    [Space]

    private bool isAttack;
    private bool isRight;
    private bool isUp;

    private bool flag;
    private bool DirectFlag;

    private void Start()
    {
        player = GetComponentInParent<Player>().gameObject;
        PlayerAtt = GetComponentInParent<Player>().setAtt();
        coll = GetComponent<BoxCollider2D>();
        weaponId = this.gameObject.GetComponentInChildren<WeaponId>();
        flag = true;
        DirectFlag = true;

        WeafonEffect.GetComponent<Effect>().setDamage(att);
        Debug.Log( "weafon : "+(att +PlayerAtt));
        //anim = GetComponent<Animator>();

        attackTime = 0.5f;
        cnt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttack)
            TrackingMonuse();
    }

    private void TrackingMonuse()
    {
        int correctionValue =0;

        Vector2 Mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;



        if (Mouse.y > 0)
            isUp = true;
        else
            isUp = false;

        Debug.Log("위 아래 : "+ isUp);


        if (Mouse.x >0)
        {
            isRight = true;
            Debug.Log("isRight : " + isRight);
            correctionValue = 60;

        }
        else
        {
            isRight = false;
            Debug.Log("isRight : " + isRight);
            correctionValue = -60;
        }



        z = Mathf.Atan2(Mouse.y, Mouse.x) * Mathf.Rad2Deg -90 + correctionValue;
        attackPos = Mathf.Atan2(Mouse.y, Mouse.x) * Mathf.Rad2Deg;

        attackByVec = Mouse.normalized;
                      //new Vector2(Mathf.Cos(Mathf.Abs(attackPos)), Mathf.Sin(Mathf.Abs(attackPos)));

        transform.rotation = Quaternion.Euler(0, 0, z);
        startPos = this.transform.rotation.z;
    }

    IEnumerator AttackMotion()
    {
        float changePos = 60;
        Debug.Log("into AttackMotion");

        while(changePos > 0)
        {
            Debug.Log("into courutine");
            Debug.Log(changePos);
            transform.rotation = Quaternion.Euler(0, 0, startPos + changePos);//올라갈때는 -
            changePos -= 1f;
            yield return new WaitForSeconds(0.01f);

        }
        flag = true;
        changePos = 60;

    }

    IEnumerator attackTest()
    {
        float angle = z;
        float ectAngle =0;

        Debug.Log("오른쪽 : " + isRight);
        Debug.Log("위 : " + isUp);


        if (!isRight && isUp) // 왼쪽 위 공격
        {
            Debug.Log("왼쪽 위 공격");


            while (angle < (120 - angle))
            {
                
                isAttack = false;
                gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);

                yield return new WaitForSeconds(0.01f);

                angle += 10;

                ectAngle = 120 - angle;
            }

            Debug.Log(ectAngle);

            if(ectAngle > 0)
            {
                while (ectAngle > 0)
                {
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);

                    yield return new WaitForSeconds(0.01f);

                    angle += 10;

                    ectAngle = 120 - angle;

                }


            }

            Debug.Log(angle +" : "+ (150 - z));

        }
        else if (!isRight && !isUp) // 왼쪽 아래 공격
        {
            Debug.Log("angle : " + angle);

            Debug.Log("왼쪽 아래 공격");


            // -228     -108
            while (angle < (120 + angle))
            {

                Debug.Log("wht dksemfdjdha");

                isAttack = false;
                gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);

                yield return new WaitForSeconds(0.01f);

                angle -= 10;

                //ectAngle = 120 - angle;



                if (angle < -120)
                    break;
            }

            /*
            if (ectAngle > 0)
            {
                while (ectAngle > 0)
                {
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);

                    yield return new WaitForSeconds(0.01f);

                    angle -= 10;

                    ectAngle = 120 + angle;

                }


            }*/

            Debug.Log(angle + " : " + (150 - z));

        }
        else if(isRight && isUp) // 오른쪽 위 공격 
        {
            Debug.Log("오른쪽 위 공격");

            while (angle > (-180 - angle))
            {

                isAttack = false;
                gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);

                yield return new WaitForSeconds(0.01f);

                angle -= 10;


            }

            Debug.Log(angle + " : " + (-150 + angle));

        }
        else if(isRight && !isUp) // 오른쪽 아래 공격
        {
            while (angle < (180 + angle))
            {

                Debug.Log("오른쪽 아래 공격");


                isAttack = false;
                gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);

                yield return new WaitForSeconds(0.01f);

                angle += 10;


            }

            Debug.Log(angle + " : " + (-150 + z));

        }

        gameObject.transform.rotation = Quaternion.Euler(0,0,z);
        isAttack = true;

        yield return null;
    }

    public void Attack()
    {

        int x = 0;
        int y = 0;

        if (isRight)
            x = 1;
        else
            x = -1;

        if (isUp)
            y = 1;
        else
            y = -1;

        // go 의 위치를 어택 포지션 노말라이즈드 한것의 좌표값 으로 전환 
        //GameObject go = Instantiate(WeafonEffect, transform.parent.position + new Vector3(1 * x,1,0), Quaternion.identity);

        if(cnt >2)
        {

            Debug.Log("is Skill");
            GameObject go = Instantiate(skill, (Vector2)transform.parent.position, Quaternion.identity);

            // go.transform.localScale = new Vector2(transform.localScale.x * x, transform.localScale.y * y);
            SoundManager.Instance.PlaySound("BasicSwSkill");
            Destroy(go, 0.5f);

            cnt = 0;

        }
        else
        {
            GameObject go = Instantiate(WeafonEffect, (Vector2)transform.parent.position + attackByVec, Quaternion.identity);
            go.GetComponent<Effect>().setDamage(att+PlayerAtt);
            go.transform.localScale = new Vector2(transform.localScale.x * x, transform.localScale.y * y);
            Destroy(go, 0.5f);

        }

        ShakeCamera.Instance.OnShakeCamera();
        StartCoroutine(attackTest());
        /*
        DirectFlag = !DirectFlag;
        flag = false;
        float ChangePos = player.transform.localScale.x > 0 ? -270 : 270;

        att = (int)Random.Range(weaponId.att - 2, weaponId.att + 3);//  무기 마다 데이터 화 해서 넣기 
        coll.enabled = true;

        if(!DirectFlag)
            transform.rotation = Quaternion.Euler(0, 0, startPos + ChangePos);
        else
            transform.rotation = Quaternion.Euler(0, 0, startPos);

        Invoke("DosenAttack", attackTime);
        */

        cnt++;
    }


    private void DosenAttack()
    {
        coll.enabled = false;
        flag = true;

    }

    public void ChangeWeafon(GameObject swapWeapon)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = swapWeapon.GetComponent<WeaponId>().sprite;
        att = swapWeapon.GetComponent<WeaponId>().att;

    }

    private void MakeSound()
    {
        int AttackSound = Random.Range(1, 4);
        SoundManager.Instance.PlaySound("Attack" + AttackSound.ToString());
        Debug.Log("Attack" + AttackSound.ToString());
    }

    public void getAtt()
    {

    }
 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            Debug.Log("Attack Monster");
            MakeSound();
            collision.GetComponent<Monster>().GetDamage(att+PlayerAtt);
        }
        else if (collision.CompareTag("Mob_Monster"))
        {
            Debug.Log("Attack Monster");
            MakeSound();
            collision.GetComponent<Mob_Monster>().GetDamage(att + PlayerAtt);
        }
    }
}

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

    public WeaponId weaponId;

    private Animator anim;

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
        //anim = GetComponent<Animator>();

        attackTime = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttack)
            TrackingMonuse();
    }

    private void TrackingMonuse()
    {
        Vector2 Mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        if (Mouse.y > 0)
            isUp = true;
        else
            isUp = false;


        if (Mouse.x >0)
        {
            isRight = true;
            Debug.Log("isRight : " + isRight);

        }
        else
        {
            isRight = false;
            Debug.Log("isRight : " + isRight);

        }



        z = Mathf.Atan2(Mouse.y, Mouse.x) * Mathf.Rad2Deg -90;
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

        Debug.Log(isUp);

        if (!isRight && isUp) // 왼쪽 위 공격
        {

            while (angle < (150 - z))
            {
                
                isAttack = false;
                gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);

                yield return new WaitForSeconds(0.01f);

                angle += 10;

            }

            Debug.Log(angle +" : "+ (150 - z));

        }
        else if (!isRight && !isUp) // 왼쪽 아래 공격
        {
            while (angle < (150 - z))
            {

                isAttack = false;
                gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);

                yield return new WaitForSeconds(0.01f);

                angle -= 10;

            }

            Debug.Log(angle + " : " + (150 - z));

        }
        else if(isRight && isUp) // 오른쪽 아래 공격 
        {

            while (angle > (-150 + z))
            {

                isAttack = false;
                gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);

                yield return new WaitForSeconds(0.01f);

                angle -= 10;

            }

            Debug.Log(angle + " : " + (-150 + z));

        }
        else if(isRight && !isUp) // 오른쪽 아래 공격
        {
            while (angle < (150 - z))
            {

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

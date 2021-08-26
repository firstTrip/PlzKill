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

    public WeaponId weaponId;

    private Animator anim;

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
        if (flag)
            TrackingMonuse();
    }

    private void TrackingMonuse()
    {
        Vector2 Mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float z = Mathf.Atan2(Mouse.y, Mouse.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, z -90);
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

    public void Attack()
    {
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

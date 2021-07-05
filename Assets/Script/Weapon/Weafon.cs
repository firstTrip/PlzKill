using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weafon : MonoBehaviour
{

    private BoxCollider2D coll;

    public float att;

    public float PlayerAtt;

    public WeaponId weaponId;
    private void Start()
    {
        PlayerAtt = GetComponentInParent<Player>().setAtt();
        coll = GetComponent<BoxCollider2D>();
        weaponId = this.gameObject.GetComponentInChildren<WeaponId>();
    }
    // Update is called once per frame
    void Update()
    {
        /*
        Vector2 Mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float z = Mathf.Atan2(Mouse.y, Mouse.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, z);
        */
    }


    public void Attack()
    {
        att = (int)Random.Range(weaponId.att - 2, weaponId.att + 3);//  무기 마다 데이터 화 해서 넣기 
        coll.enabled = true;

        
        Invoke("DosenAttack", 0.1f);
    }


    private void DosenAttack()
    {
        Debug.Log("dosneAttack");
        coll.enabled = false;
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

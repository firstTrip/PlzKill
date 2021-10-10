using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSWSkill : MonoBehaviour
{
    private float damage;
    public int DamageCnt;

  
    public void setDamage(float wDamage)
    {
        damage = (int)Random.Range(wDamage / 2 - 5, wDamage / 2 + 5);
        Debug.Log("Weafon Damage :" + damage);
    }

    void DamageTo(GameObject obj)
    {
        for(int i=0;i< DamageCnt;i++)
        {
            obj.GetComponent<Boss>().GetDamage(damage);
            Debug.Log("HE Got damge");
        }
    }

    IEnumerator CDamageToMonster(GameObject obj)
    {

        for (int i = 0; i < DamageCnt; i++)
        {
            obj.GetComponent<Monster>().GetDamage(damage);
            Debug.Log("HE Got damge");
            yield return new WaitForSeconds(0.2f);

        }

        yield return null;

    }

    IEnumerator CDamageToBoss(GameObject obj)
    {

        for (int i = 0; i < DamageCnt; i++)
        {
            obj.GetComponent<Boss>().GetDamage(damage);
            Debug.Log("HE Got damge");
            yield return new WaitForSeconds(0.2f);

        }
        yield return null;

    }
    void DamageToMonster()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            Debug.Log(damage);
            StartCoroutine(CDamageToMonster(collision.gameObject));
            Debug.Log("HE Got skill damge");
        }

        else if (collision.CompareTag("Boss"))
        {
            Debug.Log(damage);
            StartCoroutine(CDamageToBoss(collision.gameObject));
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSWSkill : MonoBehaviour
{
    private float damage = 10;
    public int DamageCnt;

    // Start is called before the first frame update
    void Start()
    {
        damage = (int)Random.Range(damage/2 - 2, damage/2 + 3);
    }

    void DamageTo(GameObject obj)
    {
        for(int i=0;i< DamageCnt;i++)
        {
            obj.GetComponent<Boss>().GetDamage(damage);
            Debug.Log("HE Got damge");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            Debug.Log(damage);
            DamageTo(collision.gameObject);
            Debug.Log("HE Got skill damge");
        }

        else if (collision.CompareTag("Boss"))
        {
            Debug.Log(damage);
            DamageTo(collision.gameObject);
        }
    }
}

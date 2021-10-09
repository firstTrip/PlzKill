using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{

    private float damage=10;

    public void setDamage(float wDamage)
    {
        damage = wDamage;
        Debug.Log("Weafon Damage :" + damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Monster"))
        {
            Debug.Log(damage);
            collision.GetComponent<Monster>().GetDamage(damage);
            Debug.Log("HE Got damge");
        }
    }
}

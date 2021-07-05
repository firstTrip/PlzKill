using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBlock : MonoBehaviour
{
    public float Damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().GetDamage(Damage,this.gameObject.transform);
        }
    }
}

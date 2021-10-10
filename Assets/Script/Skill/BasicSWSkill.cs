using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSWSkill : MonoBehaviour
{
    private float damage = 10;


    // Start is called before the first frame update
    void Start()
    {
        damage = (int)Random.Range(damage - 2, damage + 3);
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            Debug.Log(damage);
            collision.GetComponent<Monster>().GetDamage(damage);
            Debug.Log("HE Got skill damge");
        }
    }
}

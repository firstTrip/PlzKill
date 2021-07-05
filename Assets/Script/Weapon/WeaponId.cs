using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponId : MonoBehaviour
{
    public int ID;
    public float att;
    public float attSpeed;
    public Sprite sprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }

   
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{


    [SerializeField] private float sizeOfBlood;

    private Rigidbody2D rb;

    public float blood = 10;

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sizeOfBlood = 10;
        rb.AddForce(Vector2.up *3, ForceMode2D.Impulse);
    }



    private void Update()
    {
        RaycastHit2D ray = Physics2D.Raycast(this.transform.position, Vector2.up, 1f, LayerMask.GetMask("Player"));
        RaycastHit2D ray2 = Physics2D.Raycast(this.transform.position, Vector2.down, 0.2f, LayerMask.GetMask("Ground"));

        if (ray2)
        {
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        if (ray)
        {
            Debug.Log("into blood");
            ray.collider.GetComponent<Player>().getBlood(blood);
            setBlood();
        }
    }
    public float setBlood()
    {
        Destroy(this.gameObject);
        return sizeOfBlood;
    }


}

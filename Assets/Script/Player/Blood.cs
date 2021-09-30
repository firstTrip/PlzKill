using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{


    [SerializeField] private float sizeOfBlood;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sizeOfBlood = 10;
        rb.AddForce(Vector2.up*10, ForceMode2D.Impulse);
        Destroy(gameObject, 6f);
    }

  
    public float setBlood()
    {
        Destroy(this.gameObject);
        return sizeOfBlood;
    }


}

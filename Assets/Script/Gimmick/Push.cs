using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Push : MonoBehaviour
{

    private BoxCollider2D coll;
    private Rigidbody2D rb;


    [Tooltip("발사각 :")][SerializeField] private int x, y;
    public float pushPower;
    [SerializeField] private bool isStart;
    [SerializeField] private bool isActive;

    [Tooltip("true 일경우 제자리로 돌아옴 ")][SerializeField] private bool reStart;



    private void Start()
    {
        coll =GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        //pushPower = 500;
        isStart = false;
        isActive = false;
    }

    private void Update()
    {
        if (isStart)
        {
            rb.AddForce(new Vector2(x,y)* pushPower, ForceMode2D.Impulse);
            isStart = false;

            if(reStart)
                Invoke("RePlace",0.1f);
        }
    }
    private void Action()
    {
        if (!isActive)
        {
            isStart = true;
            isActive = true;
        }

    }

    private void RePlace()
    {
        rb.AddForce(new Vector2(-x, y) * pushPower, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            int dir = collision.gameObject.transform.position.x - transform.position.x > 0 ? 1 : -1;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(dir , 0) * pushPower, ForceMode2D.Impulse);
            Debug.Log("들왔냐?");
        }
    }
}

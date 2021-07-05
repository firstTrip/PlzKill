using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DropBlock : MonoBehaviour
{

    private bool isStart;

    private Rigidbody2D rb;
    RaycastHit2D raycast;


    public Vector3 offset;
    public Vector3 boxScale;
    private bool onPlayer;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isStart = true;
        offset = Vector3.zero;
        onPlayer = false;
    }

    IEnumerator Drop()
    {
        if (isStart)
        {
            Debug.Log("into");

            Tween myTween = transform.DOShakePosition(1f,0.1f,2,0);

            yield return myTween.WaitForCompletion();

            this.gameObject.transform.rotation = Quaternion.identity;

            rb.bodyType = RigidbodyType2D.Dynamic;

            if (gameObject.GetComponent<MoveToXBlock>())
            {
                gameObject.GetComponent<MoveToXBlock>().PauseBlock();
            }
            isStart = false;

            Destroy(gameObject, 2f);

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.CompareTag("Wave"))
        {
            StartCoroutine(Drop());
        }
        else if (collision.CompareTag("Player"))
        {
            StartCoroutine(Drop());
        }


    }

}

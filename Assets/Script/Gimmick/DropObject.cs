using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropObject : MonoBehaviour
{

    private Rigidbody2D rb;
    private float wave;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void MakeEarthquake(float wave)
    {
       Debug.Log(wave);

        GameObject go = new GameObject();
        go.tag = "Wave";
        go.transform.position = new Vector2(gameObject.transform.position.x,gameObject.transform.position.y - gameObject.transform.localScale.x/2);
        BoxCollider2D goCollider = go.AddComponent<BoxCollider2D>();
        goCollider.isTrigger= true;
        goCollider.transform.localScale = new Vector3(wave, gameObject.transform.localScale.x/2,0.1f);

        Destroy(go,1f);
        Destroy(gameObject, 3f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider != null)
            Debug.Log(collision.collider.name);

        if (collision.collider.CompareTag("Ground"))
        {
            Debug.Log("why?");
            wave = collision.relativeVelocity.magnitude * rb.mass/2;
            MakeEarthquake(wave);
        }
    }
}

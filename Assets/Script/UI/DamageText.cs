using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    public float moveSpeed;
    public float alphaSpeed;
    public float destroySpeed;

    TextMeshPro text;
    Color alpha;

    public float damage;

    private void Start()
    {
        text = GetComponent<TextMeshPro>();
        text.text = damage.ToString();
        alpha = text.color;

        Invoke("DestroyObject", destroySpeed);
    }

    private void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed);

        text.color = alpha;
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}

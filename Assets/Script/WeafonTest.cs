using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeafonTest : MonoBehaviour
{
    private bool isAttack;
    // Start is called before the first frame update
    void Start()
    {
        isAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && isAttack)
        {
            StartCoroutine(attack());
        }
    }

    IEnumerator attack()
    {
        int angle =0;
        while(angle<150)
        {
            isAttack = false;
            gameObject.transform.rotation =  Quaternion.Euler(0,0,angle);

            Debug.Log(gameObject.transform.rotation);
            yield return new WaitForSeconds(0.025f);

            angle += 10;
        }

        gameObject.transform.rotation = Quaternion.identity;
        isAttack = true;

        yield return null;
    }
}

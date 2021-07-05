using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTypeAction : MonoBehaviour
{
    [SerializeField] private GameObject obstacle;

    [SerializeField] private Rigidbody2D rb;

    private bool isActive = false;

    private void Action()
    {
        Debug.Log("intoasd");

        if (obstacle.gameObject == null)
            return;

        if (obstacle.GetComponent<Rigidbody2D>() && !isActive)
        {
            rb = obstacle.GetComponent<Rigidbody2D>();

            if (rb.bodyType == RigidbodyType2D.Kinematic)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;

            }
            else if (rb.bodyType == RigidbodyType2D.Dynamic)
            {
                rb.bodyType = RigidbodyType2D.Kinematic;
            }

            isActive = true;
        }  
        
       
    }
}

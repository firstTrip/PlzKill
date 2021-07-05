using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTrigger : MonoBehaviour
{

    public string tag = "Player";
    public string functionEnter = "ChildTriggerEnter";
    public string functionExit = "ChildTriggerExit";

    public GameObject[] SendMessageTarget;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (string.IsNullOrEmpty(functionEnter))
            return;

        if (collision.CompareTag(tag))
        {
            if(SendMessageTarget.Length > 0)
            {
                for(int i = 0; i < SendMessageTarget.Length; i++)
                {
                    SendMessageTarget[i].SendMessage(functionEnter, collision);
                }
            }
            else
            {
                SendMessageUpwards(functionEnter, collision);
            } 

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (string.IsNullOrEmpty(functionExit))
        {
            return;
        }

        if (collision.CompareTag(tag))
        {
            if (SendMessageTarget.Length > 0)
            {
                for (int i = 0; i < SendMessageTarget.Length; i++)
                {
                    SendMessageTarget[i].SendMessage(functionExit, collision);
                }
            }
            else
            {
                SendMessageUpwards(functionExit, collision);
            }
        }
    }
}



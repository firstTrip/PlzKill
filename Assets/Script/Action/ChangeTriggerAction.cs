 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTriggerAction : MonoBehaviour
{
    bool isActive = true;

    // Start is called before the first frame update
    private void Action()
    {
        if (isActive)
        {
            isActive = false;

            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;

        }


    }
}

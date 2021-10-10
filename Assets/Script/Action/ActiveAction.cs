using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAction : MonoBehaviour
{

    bool isActive = true;

    private void Awake()
    {
        this.gameObject.SetActive(false);

    }

    // Start is called before the first frame update
    private void Action()
    {
        if (isActive)
        {
            isActive = false;
            this.gameObject.SetActive(true);

        }


    }
}

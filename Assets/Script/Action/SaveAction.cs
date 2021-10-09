using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAction : MonoBehaviour
{

    private float PlayerX;
    private float PlayerY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

   private void Action()
    {
        DataManager.Instance.Save();
    }
}

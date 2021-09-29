using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{


    [SerializeField] private float sizeOfBlood;

    // Start is called before the first frame update
    private void Awake()
    {
        sizeOfBlood = 10;
        Destroy(gameObject, 6f);
    }

  
    public float setBlood()
    {
        Destroy(this.gameObject);
        return sizeOfBlood;
    }


}

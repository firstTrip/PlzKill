using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{


    [SerializeField] private float sizeOfBlood;
    [SerializeField] private ParticleSystem ps;

    // Start is called before the first frame update
    private void Awake()
    {
        sizeOfBlood = 10;
        ps = GetComponent<ParticleSystem>();

        Destroy(gameObject, 6f);
    }

  
    public float setBlood()
    {
        Destroy(this.gameObject);
        return sizeOfBlood;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("INTOASD");
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterSprite : MonoBehaviour
{

    private Image image;
    private Sprite sprite;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        sprite = GetComponent<SpriteRenderer>().sprite;

       
    }

    // Update is called once per frame
    void Update()
    {

        if(image.name != sprite.name)
        {
            sprite = image.sprite;
        }
       
    }
}

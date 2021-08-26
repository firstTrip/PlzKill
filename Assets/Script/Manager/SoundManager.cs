using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip BGM;

    [Header("Swish Sound")]
    public AudioClip Swish1;
    public AudioClip Swish2;
    public AudioClip Swish3;

    [Space]

    [Header("Attack Sound")]
    public AudioClip Attack1;
    public AudioClip Attack2;
    public AudioClip Attack3;



    #region SingleTon
    /* SingleTon */
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType(typeof(SoundManager)) as SoundManager;
                if (!instance)
                {
                    GameObject container = new GameObject();
                    container.name = "SoundManager";
                    instance = container.AddComponent(typeof(SoundManager)) as SoundManager;
                }
            }

            return instance;
        }
    }

    #endregion

    private void Awake()
    {
        #region SingleTon
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(this);
        }
        #endregion
    }

    public void PlaySound(string soundName)
    {
        if (soundName == "BGM")
        {
            //audioSource.PlayOneShot(BGM);
        }
        else if (soundName == "Swish1")
        {
            audioSource.PlayOneShot(Swish1);

        }
        else if (soundName == "Swish2")
        {
            audioSource.PlayOneShot(Swish2);

        }
        else if (soundName == "Swish3")
        {
            audioSource.PlayOneShot(Swish3);

        }
        else if (soundName == "Attack1")
        {
            audioSource.PlayOneShot(Attack1);

        }
        else if (soundName == "Attack2")
        {
            audioSource.PlayOneShot(Attack2);

        }
        else if (soundName == "Attack3")
        {
            audioSource.PlayOneShot(Attack3);

        }
        else if (soundName == "Run")
        {
            //audioSource.clip = Run;
            audioSource.loop = false;
            audioSource.Play();

        }
        else if (soundName == "Jump")
        {
            //audioSource.PlayOneShot(Jump);

        }

    }
}

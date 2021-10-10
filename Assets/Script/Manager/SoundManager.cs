using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;


    [Header("Swish Sound")]
    public AudioClip Swish1;
    public AudioClip Swish2;
    public AudioClip Swish3;

    [Space]

    [Header("Attack Sound")]
    public AudioClip Attack1;
    public AudioClip Attack2;
    public AudioClip Attack3;


    [Space]
    [Header("skill")]
    public AudioClip BasicSwSkill;

    [Space]

    [Header("BGM")]
    public AudioClip[] NomalBGM;
    
    [Space]
    public AudioClip[] BossBgm;


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
        PlayLoopSound("NomalLoopSound");

    }

    public void PlayLoopSound(string loopSound)
    {
        if (!audioSource.isPlaying)
            audioSource.Stop();
        StartCoroutine(loopSound);
    }

    

    IEnumerator NomalLoopSound()
    {
        audioSource.volume = 1;
        audioSource.clip = NomalBGM[0];
        audioSource.Play();

        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            if (!audioSource.isPlaying)
            {
                audioSource.clip = NomalBGM[1];
                audioSource.Play();
                audioSource.loop = true;
            }
        }

    }

    IEnumerator BossLoopSound()
    {
        audioSource.volume = 1;
        audioSource.clip = BossBgm[0];
        audioSource.Play();

        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            if (!audioSource.isPlaying)
            {
                audioSource.clip = BossBgm[1];
                audioSource.Play();
                audioSource.loop = true;
            }
        }

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
        else if (soundName == "BasicSwSkill")
        {
            audioSource.PlayOneShot(BasicSwSkill);

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

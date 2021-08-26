using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UIManager : MonoBehaviour
{

    #region SingleTon
    /* SingleTon */
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType(typeof(UIManager)) as UIManager;
                if (!instance)
                {
                    GameObject container = new GameObject();
                    container.name = "UIManager";
                    instance = container.AddComponent(typeof(UIManager)) as UIManager;
                }
            }

            return instance;
        }
    }

    #endregion

    public Image textBox;
    public Image textNpcBox;
    public Image FadeImg;

    public TextMeshProUGUI text;
    public TextMeshProUGUI npcText;

    public int talkIndex;
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
    
    public void SetNotice(bool isActive ,bool isNpc )
    {

        if (isActive)
        {
            if (isNpc)
                textNpcBox.gameObject.SetActive(true);
            else
                textBox.gameObject.SetActive(true);
            //GameManager.Instance.GamePause(true);
        }
        else
        {
            if (isNpc)
                textNpcBox.gameObject.SetActive(false);
            else
                textBox.gameObject.SetActive(false);
            // GameManager.Instance.GamePause(false);
        }

    }

    public void SetText(int id,bool isNpc , float Duration)
    {
        string talkData = TalkManager.Instance.GetTalk(id, talkIndex);

        if (talkData == null)
            return;

        if (isNpc)
        {
            Debug.Log(talkData);
            npcText.text = talkData;
            TMProUGUIDoText.DoText(npcText, Duration);

        }
        else
        {
            text.text = talkData;
            Debug.Log(talkData);
            TMProUGUIDoText.DoText(text, Duration);

        }
        
    }

    public void FadeOut(float dulation)
    {
        //FadeImg.DOFade(1.0f, dulation);
    }
}

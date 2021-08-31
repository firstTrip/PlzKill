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

    public Button[] npcButton;
    public Button[] RewardButton;

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
        talkIndex = 0;

        for (int i = 0; i < RewardButton.Length; i++)
        {
            RewardButton[i].gameObject.SetActive(false);
        }
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
        int QuestInt = QuestManager.Instance.GetQuestIndex();
        string talkData = TalkManager.Instance.GetTalk(id + QuestInt, talkIndex);
        Debug.Log(talkData+talkIndex);

        if (talkData == null) 
        {
            if (isNpc)
            {
                for (int i = 0; i < npcButton.Length; i++)
                    npcButton[i].gameObject.SetActive(true);
                return;
            }

            IntializeText();
            return;

        }

        if (isNpc)
        {
            for (int i = 0; i < npcButton.Length; i++)
                npcButton[i].gameObject.SetActive(false);

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
        GameManager.Instance.isAction = true;
        talkIndex++;
    }

    public void IntializeText()
    {
        GameManager.Instance.isAction = false;
        talkIndex = 0;
        QuestManager.Instance.QuestIndex = 10;
    }

    public void ActiveReward()
    {
        for(int i=0;i < RewardButton.Length; i++)
        {
            RewardButton[i].gameObject.SetActive(true);
        }
    }

    public void DisActiveReward()
    {
        for (int i = 0; i < RewardButton.Length; i++)
        {
            RewardButton[i].gameObject.SetActive(false);
        }
    }
    public void FadeOut(float dulation)
    {
        //FadeImg.DOFade(1.0f, dulation);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{

    #region SingleTon
    /* SingleTon */
    private static TalkManager instance;
    public static TalkManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType(typeof(TalkManager)) as TalkManager;
                if (!instance)
                {
                    GameObject container = new GameObject();
                    container.name = "TalkManager";
                    instance = container.AddComponent(typeof(TalkManager)) as TalkManager;
                }
            }

            return instance;
        }
    }

    #endregion

    Dictionary<int, string[]> talkData;

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

        talkData = new Dictionary<int, string[]>();
        GenerateData();

    }


    void GenerateData()
    {
        // 9 = NoSelect 를 선택시
        // 11 = YesSelect 를 선택시
        talkData.Add(10 +1000,new string[]{"안녕!!","넌 무엇을 원하니?\n선택해" });
        talkData.Add(11+1000, new string[] { "고맙군 자네 자네가 은인이야" });

        talkData.Add(9+1000, new string[] { "자네는 재능이 있어" });

    }


    public string GetTalk(int id,int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex]; 
    }
    
}

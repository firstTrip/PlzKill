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
        // 9 = NoSelect �� ���ý�
        // 11 = YesSelect �� ���ý�
        talkData.Add(10 +1000,new string[]{"�ȳ�!!","�� ������ ���ϴ�?\n������" });
        talkData.Add(11+1000, new string[] { "���� �ڳ� �ڳװ� �����̾�" });

        talkData.Add(9+1000, new string[] { "�ڳ״� ����� �־�" });

    }


    public string GetTalk(int id,int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex]; 
    }
    
}

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
        talkData.Add(10 +1000,new string[]{"�ȳ��Ͻʴϱ�!\n�������� ����� ������\n���ô��� ����ϼ̽��ϴ�\n�ֱ� �������� ������ ����鿡��\n���и� �θ��� �ٶ���\n�����Ⱑ ���� �ʽ��ϴٸ�..\n�ٵ� �������� �����Ϸ� ���̽��ϱ�?" }); 

        //Yes
        talkData.Add(11+1000, new string[] { "��°��....\n\n - ���� �밡�� �ɷ��� �����մϴ�" });

        //No
        talkData.Add(9+1000, new string[] { "�������� ������ óġ�Ͻðڴٰ��..?\n��.. �׷� ���� ���ڽ��ϴ�!\n������ â���� ������ �����Դϴ�\n�ϳ� �������\n\n - �ֹ����� ���� ������ ���� �� �ϳ��� �޽��ϴ�" });

    }


    public string GetTalk(int id,int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex]; 
    }
    
}

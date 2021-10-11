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
        talkData.Add(10 +1000,new string[]{"안녕하십니까!\n지하층에 사람이 많은데\n오시느라 고생하셨습니다\n최근 지하층의 수장이 사람들에게\n행패를 부리는 바람에\n분위기가 좋진 않습니다만..\n근데 지하층엔 무슨일로 오셨습니까?" }); 

        //Yes
        talkData.Add(11+1000, new string[] { "어째서....\n\n - 피의 대가로 능력이 증가합니다" });

        //No
        talkData.Add(9+1000, new string[] { "지하층의 수장을 처치하시겠다고요..?\n그.. 그럼 저희도 돕겠습니다!\n병사의 창고에서 빼돌린 무기입니다\n하나 들고가세요\n\n - 주민으로 부터 숨겨진 무기 중 하나를 받습니다" });

    }


    public string GetTalk(int id,int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex]; 
    }
    
}

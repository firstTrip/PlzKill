using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    #region SingleTon
    /* SingleTon */
    private static QuestManager instance;
    public static QuestManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType(typeof(QuestManager)) as QuestManager;
                if (!instance)
                {
                    GameObject container = new GameObject();
                    container.name = "QuestManager";
                    instance = container.AddComponent(typeof(QuestManager)) as QuestManager;
                }
            }

            return instance;
        }
    }

    #endregion


    public int QuestIndex;

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


        QuestIndex = 10;
    }

    public int GetQuestIndex()
    {
        return QuestIndex;
    }

    public void DownQuestInDex() => QuestIndex--;

    public void UpQuestInDex() => QuestIndex++;

}

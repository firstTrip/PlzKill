using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    #region SingleTon
    /* SingleTon */
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
                if (!instance)
                {
                    GameObject container = new GameObject();
                    container.name = "GameManager";
                    instance = container.AddComponent(typeof(GameManager)) as GameManager;
                }
            }

            return instance;
        }
    }

    #endregion

    public EnumGameState NowState;

    private GameObject scanObject;

    private GameObject BossObject;
    public bool isAction;

    private int NpcCount;
    public float Duration;

    public List<Reward> RewardList = new List<Reward>();

    public List<Reward> RewardResult = new List<Reward>();

    private int num = 0;
    public int total;

    public enum GameMode
    {
        nomal,
        boss
    }

    public GameMode gameMode; 

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

        Duration = 1f;
        NpcCount = 0;
        SetStatList();
        BossObject = GameObject.FindGameObjectWithTag("Boss");
        gameMode = GameMode.nomal;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            DataManager.Instance.Save();

        if (Input.GetKeyDown(KeyCode.R))
            DataManager.Instance.Load();


    } 
    private bool isPause;

    public void GamePause(bool TrueIsPause)
    {
        isPause = TrueIsPause;

        if (TrueIsPause)
        {
            ChangeGameState(EnumGameState.Stop);
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
            ChangeGameState(EnumGameState.Action);
        }
    }

    public void BossActive()
    {
        BossObject.GetComponent<Boss>().StartThink();
    }

    public void ChangeGameState(EnumGameState state)
    {
        NowState = state;
    }

    public void TalkAction(GameObject scanObj)
    {

        Debug.Log("Talk Action");
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();

        UIManager.Instance.SetText(objData.ID, objData.IsNpc, Duration);
        UIManager.Instance.SetNotice(isAction, objData.IsNpc);
    }

    public void UpNpcCnt()
    {
        NpcCount++;
        if(NpcCount == 3)
        {
            NpcCount = 0;
            //무기 보상 주기
        }
    }

    public Reward RandomReward()
    {
        int weight =0;
        int selectNum = 0;

        selectNum = Mathf.RoundToInt(total * Random.Range(0.0f, 1.0f));

        for(int i=0;i< RewardList.Count; i++)
        {
            weight += RewardList[i].weight;
            if(selectNum <= weight)
            {
                Reward temp = new Reward(RewardList[i]);
                return temp;
            }
            
        }

        return null;
    }

    public void MakeReward()
    {

        for (int i = 0; i < 10; i++)
        {
            RewardResult.Add(RandomReward());
        }

        /*
        if (RewardResult != null)
        {
            RewardList.Clear();
            RewardResult.Clear();
        }
        else
        {
            
        }
        */
      
    }

    public void SetStatList()
    {
        for(int i=0;i< RewardList.Count; i++)
        {
            total += RewardList[i].weight;
        }
    }

    
    public Reward SetReward()
    {
        do
        {
            num++;
        } while (RewardResult[num - 1].ReName == RewardResult[num].ReName);

        return RewardResult[num];
    }
    
}

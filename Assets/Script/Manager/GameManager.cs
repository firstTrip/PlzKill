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

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeGameState(EnumGameState state)
    {
        NowState = state;
    }
}

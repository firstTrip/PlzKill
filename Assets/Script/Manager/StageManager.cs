using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{

    private static StageManager instance;

    #region ΩÃ±€≈Ê
    public static StageManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType(typeof(StageManager)) as StageManager;
                if (!instance)
                {
                    GameObject container = new GameObject();
                    container.name = "StageManager";
                    instance = container.AddComponent(typeof(StageManager)) as StageManager;
                }
            }

            return instance;
        }
    }
    #endregion

    public List<GameObject> Stage;

    private int star;
    private int redStar;
    // Start is called before the first frame update
    void Start()
    {
        #region
        if (instance == null)
        {
            instance = this;
        }else if(instance != null)
        {
            Destroy(this);
        }
        #endregion
        Initaillized();
    }

    private void Initaillized()
    {
        star = 0;
        redStar = 0;

        Stage = new List<GameObject>();

    }


    public void CallStage()
    {

    }
}

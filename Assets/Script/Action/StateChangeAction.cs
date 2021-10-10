using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChangeAction : MonoBehaviour
{
    bool isActive = true;

    private void Action()
    {
        if (isActive)
        {
            isActive = false;
             
            if (GameManager.Instance.gameMode == GameManager.GameMode.nomal)
                GameManager.Instance.gameMode = GameManager.GameMode.boss;

            else if (GameManager.Instance.gameMode == GameManager.GameMode.boss)
                GameManager.Instance.gameMode = GameManager.GameMode.nomal;

        }
       

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseAction : MonoBehaviour
{

    [Header("몬수터 수 :")]
    [SerializeField] private int NumOfMonster;
    [Space]

    [Header("몬수터 종류 :")]
    [SerializeField] private GameObject[] TypeOfMonsters;

    private int cnt;

   private void Action()
    {
        do
        {
            if (TypeOfMonsters == null)
            {
                Debug.Log("into finish");
                return;
            }
            int n = Random.Range(0, TypeOfMonsters.Length);
            Instantiate(TypeOfMonsters[n], transform.position, Quaternion.identity);
            cnt++;

        }while (cnt < NumOfMonster) ;
       
    }
}
